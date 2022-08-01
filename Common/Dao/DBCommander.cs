using System.Reflection;
using System.Data;
using System.Collections;
using Npgsql;
///-----------------------------------------------------------------
///   Namespace     : CinemaProject.Common.Dao
///   Class         : DBCommander
///   Description   : DBConnect・SQLCommand
///   Author        : YOON                    
///   Update Date   : 2022-07-26
///-----------------------------------------------------------------
namespace CinemaProject.Common.Dao;

public class DBCommander : IDisposable
{
    // Postgres DB Connection Object
    private NpgsqlConnection? conn;
    // Postgres DB Command Object           
    private NpgsqlCommand cmd;
    // External Transaction           
    private DBTransaction? externalTran; 
    
    /// <summary>
    /// Create Object
    /// </summary>
    public DBCommander()
    {
        if (conn == null) conn = new NpgsqlConnection(Const.INIT_CONFIG?[Const.DB_CONFIG]);
        if (cmd == null) cmd = new NpgsqlCommand();
    }

    /// <summary>
    /// Create Object(Transaction)
    /// </summary>
    /// <param name="externalTran">Transaction</param>
    public DBCommander(DBTransaction externalTran)
    {
        this.externalTran = externalTran;
        conn = externalTran.GetConnection();
        if (cmd == null) cmd = new NpgsqlCommand();
    }
    
    /// <summary>
    /// Add Parameter(key , value)
    /// </summary>
    /// <param name="key">キー</param>
    /// <param name="value">値</param>
    public void AddParameter(string key, object value)
    {
        cmd.Parameters.Add(new NpgsqlParameter(key, value));
    }
    
    /// <summary>
    /// Add Parameter(Object<T>)
    /// </summary>
    /// <param name="key">キー</param>
    /// <param name="value">値</param>
    public void AddParameter<T>(T param)
    {
        Type resultObjectType = typeof(T);

        Object? obj = resultObjectType.GetConstructor(new Type[]{ })?.Invoke(new Object[] { });

        if(obj != null)
        {
            foreach (PropertyInfo prop in obj.GetType().GetProperties())
            {
                if (typeof(ArrayList) == prop.PropertyType) continue;
                cmd.Parameters.Add(new NpgsqlParameter(prop.Name, prop.GetValue(param)));
            }
        }
    }
    
    /// <summary>
    /// SELECT Process(Key, Value)
    /// </summary>
    /// <param name="sql">sql</param>
    /// <returns>Execute Result</returns>
    public IList<Dictionary<string, object>> ExecuteReader(string sql)
    {
        return ExecuteReader<Dictionary<string, object>>(sql);
    }
    
    /// <summary>
    /// SELECT Process(Object<T>)
    /// </summary>
    /// <typeparam name="T">Object<T></typeparam>
    /// <param name="sql">sql</param>
    /// <returns>Execute Result</returns>
    public IList<T> ExecuteReader<T>(string sql)
    {
        Type resultObjectType = typeof(T);

        cmd.CommandText = sql;

        NpgsqlDataReader? dr = null;
        IList<T> list = new List<T>();

        try
        {
            cmd.Connection = conn;                
            if(conn?.State == ConnectionState.Closed) conn.Open();

            dr = cmd.ExecuteReader();

            if (CheckObjectPrimitiveDataType(resultObjectType))
            {
                while (dr.Read())
                {
                    list.Add((T)dr[0]);
                }
            }
            else if(resultObjectType == typeof(Dictionary<string, object>))
            {
                while (dr.Read())
                {
                    Dictionary<string, object> obj = new Dictionary<string, object>();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        obj.Add(dr.GetName(i), dr[dr.GetName(i)]);
                    }
                    list.Add((T)(object)obj);
                }
            }
            else
            {
                while (dr.Read())
                {
                    Object? obj = resultObjectType.GetConstructor(new Type[] { })?.Invoke(new Object[] { });
                    if(obj != null)
                    {
                        foreach (PropertyInfo prop in obj.GetType().GetProperties())
                        {
                            bool hasColumnName = false;
                            for (int i = 0; i < dr.FieldCount; i++)
                            {
                                if (dr.GetName(i).Equals(prop.Name))
                                {
                                    hasColumnName = true;
                                    break;
                                }
                            }

                            if (hasColumnName && !object.Equals(dr[prop.Name], DBNull.Value))
                            {
                                if(typeof(Nullable<decimal>) == prop.PropertyType)
                                {
                                    prop.SetValue(obj, null, null);
                                }
                                else
                                {
                                    prop.SetValue(obj, dr[prop.Name], null);
                                }
                                
                            }
                        }
                        list.Add((T)obj);
                    }
                }
            }                
        }
        catch (NpgsqlException) {  throw; }
        catch (Exception) { throw; }
        finally
        {
            if (dr != null) dr.Dispose();
            if (cmd != null) cmd.Dispose();
            if (conn != null && externalTran == null) conn.Close();
        }

        return list;
    }
    
    /// <summary>
    /// CheckObjectPrimitiveDataType
    /// </summary>
    /// <param name="resultObjectType">PrimitiveDataType</param>
    /// <returns>Check Result</returns>
    private bool CheckObjectPrimitiveDataType(Type resultObjectType)
    {
        if (resultObjectType == typeof(bool)) return true;
        else if (resultObjectType == typeof(byte)) return true;
        else if (resultObjectType == typeof(char)) return true;
        else if (resultObjectType == typeof(int)) return true;
        else if (resultObjectType == typeof(decimal)) return true;
        else if (resultObjectType == typeof(double)) return true;
        else if (resultObjectType == typeof(sbyte)) return true;
        else if (resultObjectType == typeof(string)) return true;
        else if (resultObjectType == typeof(Int16)) return true;
        else if (resultObjectType == typeof(Int32)) return true;
        else if (resultObjectType == typeof(Int64)) return true;
        else if (resultObjectType == typeof(Single)) return true;
        else if (resultObjectType == typeof(UInt16)) return true;
        else if (resultObjectType == typeof(UInt32)) return true;
        else if (resultObjectType == typeof(UInt64)) return true;
        else return false;
    }
    
    /// <summary>
    /// 「Insert, Update, Delete」
    /// </summary>
    /// <param name="sql">sql</param>
    /// <returns>Process Count</returns>
    public int ExecuteNonQuery(string sql)
    {
        int result = -1;
        NpgsqlTransaction? localTran = null;
        try
        {
            cmd.CommandText = sql;
            cmd.Connection = conn;
            
            if (externalTran == null)
            {
                if(conn?.State == ConnectionState.Closed) conn.Open();
                localTran = conn?.BeginTransaction();
            }

            result = cmd.ExecuteNonQuery();

            if(localTran != null) localTran.Commit();
        }
        catch (NpgsqlException)
        {
            if (localTran != null) localTran.Rollback();
            throw;
        }
        catch (Exception) {
            if (localTran != null) localTran.Rollback();
            throw; 
        }
        finally
        {
            if (cmd != null) cmd.Dispose();
            if (conn != null && externalTran == null) conn.Close();
        }

        return result;
    }

    /// <summary>
    /// 「Insert, Update, Delete」
    /// </summary>
    /// <typeparam name="T">Object<T></typeparam>
    /// <param name="sql">sql</param>
    /// <param name="param">param</param>
    /// <returns>Process Count</returns>
    public int ExecuteNonQuery<T>(string sql, T param)
    {
        Type resultObjectType = typeof(T);

        Object? obj = resultObjectType.GetConstructor(new Type[] { })?.Invoke(new Object[] { });

        if(obj != null)
        {
            foreach (PropertyInfo prop in obj.GetType().GetProperties())
            {
                cmd.Parameters.Add(new NpgsqlParameter(prop.Name, prop.GetValue(param)));
            }
        }

        return ExecuteNonQuery(sql);
    }
    
    /// <summary>
    /// SELECT(Scalar)
    /// </summary>
    /// <param name="sql">sql</param>
    /// <returns>first Row</returns>
    public Object? ExecuteScalar(string sql)
    {
        Object? result;
        try
        {
            cmd.CommandText = sql;
            cmd.Connection = conn;
            if (conn?.State == ConnectionState.Closed) conn.Open();

            result = cmd.ExecuteScalar();
        }
        catch (NpgsqlException) { throw; }
        catch (Exception) { throw; }
        finally
        {
            if (cmd != null) cmd.Dispose();
            if (conn != null && externalTran == null) conn.Close();
        }

        return result;
    }

    /// <summary>
    /// SELECT(Scalar) 
    /// </summary>
    /// <typeparam name="T">Object<T></typeparam>
    /// <param name="sql">sql</param>
    /// <param name="param">param</param>
    /// <returns></returns>
    public Object? ExecuteScalar<T>(string sql, T param)
    {
        Type resultObjectType = typeof(T);

        Object? obj = resultObjectType.GetConstructor(new Type[] { })?.Invoke(new Object[] { });

        if(obj != null)
        {
            foreach (PropertyInfo prop in obj.GetType().GetProperties())
            {
                cmd.Parameters.Add(new NpgsqlParameter(prop.Name, prop.GetValue(param)));
            }
        }

        return ExecuteScalar(sql);
    }

    /// <summary>
    /// Dispose Object
    /// </summary>
    public void Dispose()
    {
        if (cmd != null) cmd.Dispose();
        if (conn != null && externalTran == null) conn.Close();
    }
}