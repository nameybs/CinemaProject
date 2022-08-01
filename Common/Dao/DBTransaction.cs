using Npgsql;
///-----------------------------------------------------------------
///   Namespace     : CinemaProject.Common.Dao
///   Class         : DBTransaction
///   Description   : DBTransactionProcess
///   Author        : YOON             
///   Update Date   : 2022-07-26
///-----------------------------------------------------------------
namespace CinemaProject.Common.Dao;

public class DBTransaction : IDisposable
{
    // DB Connection Object
    private NpgsqlConnection conn;
    // Oracle DB Transaction Object
    private NpgsqlTransaction tran;

    /// <summary>
    /// Create Object
    /// </summary>
    public DBTransaction() 
    {
        if (conn == null) conn = new NpgsqlConnection(Const.INIT_CONFIG?[Const.DB_CONFIG]);
        conn.Open();
        tran = conn.BeginTransaction();
    }

    /// <summary>
    /// Transaction Commit
    /// </summary>
    public void Commit()
    {
        if (tran != null) tran.Commit();
    }

    /// <summary>
    /// Transaction Rollback
    /// </summary>
    public void RollBack()
    {
        if (tran != null) tran.Rollback();
    }

    /// <summary>
    /// Dispose Object
    /// </summary>
    public void Dispose()
    {
        if (tran != null) tran.Dispose();
        if (conn != null) conn.Close();
    }

    /// <summary>
    ///  connection object
    /// </summary>
    /// <returns>connection object</returns>
    internal NpgsqlConnection GetConnection()
    {
        return conn;
    }
}
