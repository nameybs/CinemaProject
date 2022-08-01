using CinemaProject.Common.Dao;
using CinemaProject.Models.Home;
///-----------------------------------------------------------------
///   Namespace     : CinemaProject.Service.HomeService
///   Class         : HomeService
///   Description   : 
///   Author        : YOON        
///   Update Date   : 2022-07-27
///-----------------------------------------------------------------
namespace CinemaProject.Service.Home;

public class HomeService : IHomeService
{
    public Test selectTest(Test test)
    {
        IList<Test>? result = null;
            using(DBCommander cmd = new DBCommander())
            {
                try
                {
                    string sql = @"SELECT ID
                                    , NAME
                                    , AGE
                                    , BIRTH
                                    , REGDT
                                    , UPTDT
                                    FROM TEST_TABLE
                                    WHERE id = :id
                                    ORDER BY ID";
                    
                    cmd.AddParameter("id", test.id);
                    result = cmd.ExecuteReader<Test>(sql);
                }
                catch(Exception) 
                {
                    throw;
                }
            }
        return result[0];
    }

    public IList<Test> multipleSelectTest()
    {
        IList<Test>? result = null;

        using(DBCommander cmd = new DBCommander())
        {
                try
                {
                    string sql = @"SELECT ID
                                    , NAME
                                    , AGE
                                    , BIRTH
                                    , REGDT
                                    , UPTDT
                                    FROM TEST_TABLE
                                    ORDER BY ID";
                    
                    result = cmd.ExecuteReader<Test>(sql);
                }
                catch(Exception) 
                {
                    throw;
                }
        }
        return result;
    }

    public int insertTest(Test test)
    {

        int result = 0;
        using(DBCommander cmd = new DBCommander())
        {
            try
            {
                string sql = @"INSERT INTO TEST_TABLE 
                                VALUES (
                                    nextval('test_table_seq') ,
                                    create_user_id() ,
                                    :name ,
                                    :age ,
                                    :birth ,
                                    CURRENT_DATE ,
                                    NULL
                                )";
                cmd.AddParameter("name", test.name);
                cmd.AddParameter("age", test.age);
                cmd.AddParameter("birth", test.birth);
                result = cmd.ExecuteNonQuery(sql);
            }
            catch (Exception)
            {
                throw;
            }
        }
        return result;
    }

    public int multipleInsertTest(IList<Test> list)
    {

        int result = 0;
        using (DBTransaction tran = new DBTransaction())
        {
            try
            {
                foreach (var item in list)
                {
                    using(DBCommander cmd = new DBCommander(tran))
                    {
                        string sql = @"INSERT INTO TEST_TABLE 
                                        VALUES (
                                            nextval('test_table_seq') ,
                                            create_user_id() ,
                                            :name ,
                                            :age ,
                                            :birth ,
                                            CURRENT_DATE ,
                                            NULL
                                        )";
                        cmd.AddParameter("name", item.name);
                        cmd.AddParameter("age", item.age);
                        cmd.AddParameter("birth", item.birth);
                        result = cmd.ExecuteNonQuery(sql);
                    }
                }
                tran.Commit();
            }
            catch (Exception)
            {
                tran.RollBack();
                throw;
            }
        }

        return result;
    }


    public int updateTest(Test test)
    {
        int result = 0;
        using(DBCommander cmd = new DBCommander())
        {
            try
            {
                string sql = @"UPDATE TEST_TABLE
                                SET NAME = :name,
                                    UPTDT = CURRENT_DATE
                                WHERE ID = :id";

                cmd.AddParameter("name", test.name);
                cmd.AddParameter("id", test.id);
                
                result = cmd.ExecuteNonQuery(sql);
            }
            catch (Exception)
            {
                throw;
            }
        }
        return result;
    }

    public int deleteTest(Test test)
    {
        int result = 0;
        using(DBCommander cmd = new DBCommander())
        {
            try
            {
                string sql = @"DELETE FROM TEST_TABLE WHERE ID = :id";
                cmd.AddParameter("id", test.id);

                sql = string.Format(sql, test.name);
                result = cmd.ExecuteNonQuery(sql);
            }
            catch (Exception)
            {
                throw;
            }
        }
        return result;
    }

    public int multipleDeleteTest(IList<Test> list)
    {
        int result = 0;
        using (DBTransaction tran = new DBTransaction())
        {
            try
            {
                foreach (var item in list)
                {
                    using(DBCommander cmd = new DBCommander(tran))
                    {
                        string sql = @"DELETE FROM TEST_TABLE WHERE ID = :id";

                        cmd.AddParameter("id", item.id);
                        result = cmd.ExecuteNonQuery(sql);
                    }
                }
                tran.Commit();
            }
            catch (Exception)
            {
                tran.RollBack();
                throw;
            }
        }
        return result;
    }
}