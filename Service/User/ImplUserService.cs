using CinemaProject.Common.Dao;
using CinemaProject.Models.User;
//-----------------------------------------------------------------
///   Namespace     : CinemaProject.Service.UserService
///   Class         : ImplUserService
///   Description   : 
///   Author        : YOON             
///   Update Date   : 2022-08-01
///-----------------------------------------------------------------
namespace CinemaProject.Service.User;

public class UserService : IUserService
{
    public IList<UserInfo> getUser(UserInfo userInfo)
    {
        IList<UserInfo>? result = null;
        using(DBCommander cmd = new DBCommander())
        {
            try
            {
                string sql = @"SELECT 
                                user_num,
                                user_id,
                                password,
                                user_name,
                                birth,
                                gender,
                                email,
                                phone_number,
                                post_code,
                                address,
                                status,
                                reg_date,
                                upt_date
                            FROM U_USERS
                            WHERE user_id = :user_id
                              AND password = :password";

                cmd.AddParameter("user_id", userInfo.user_id);
                cmd.AddParameter("password", userInfo.password);
                result = cmd.ExecuteReader<UserInfo>(sql);
            }
            catch(Exception) 
            {
                throw;
            }
        }
        return result;
    }
}