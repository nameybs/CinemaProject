using CinemaProject.Common.Dao;
using CinemaProject.Models.User;
using MailKit.Net.Smtp;
using MimeKit;
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
        IList<UserInfo> result = null;
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

    public int getEmailCount(string email)
    {
        int result = 0;
        using(DBCommander cmd = new DBCommander())
        {
            try
            {
                string sql = @"SELECT 
                                COUNT(email)
                            FROM U_USERS
                            WHERE email = :email";

                cmd.AddParameter("email", email);
                result = Convert.ToInt32(cmd.ExecuteScalar(sql));
            }
            catch(Exception) 
            {
                throw;
            }
        }

        return result;
    }

    public int emailVerify(string email)
    {
        int value = 0;
        try
        {
            Random random = new Random();
            value = random.Next(1000, 9999);

            using(MimeMessage mimeMsg = new MimeMessage())
            {
                mimeMsg.From.Add(MailboxAddress.Parse(CinemaProject.Common.Config.GetConfigValue("MAIL_SERVER_ADDR")));
                mimeMsg.To.Add(MailboxAddress.Parse(email));
                mimeMsg.Subject = "[CinemaProject]메일 인증";
                mimeMsg.Body = new TextPart(MimeKit.Text.TextFormat.Html) 
                {
                    Text = string.Format(@"<p>아래의 인증번호를 확인 해 주세요</p>
                            <B>{0}</B>
                            <p>인증코드는 5분 이내로 입력 해 주세요.</p>
                            ", value)
                };

                using(SmtpClient smtp = new SmtpClient())
                {
                    try
                    {
                        smtp.Connect(CinemaProject.Common.Config.GetConfigValue("MAIL_SMTP_GMAIL")
                                    , Convert.ToInt32(CinemaProject.Common.Config.GetConfigValue("MAIL_PORT_GMAIL"))
                                    , false);
                        smtp.Authenticate(CinemaProject.Common.Config.GetConfigValue("MAIL_SERVER_ADDR")
                                        , CinemaProject.Common.Config.GetConfigValue("MAIL_SERVER_PASS"));
                        smtp.Send(mimeMsg);
                    }
                    finally
                    {
                        smtp.Disconnect(true);
                    }
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
        return value;
    }
}