using CinemaProject.Models.User;
//-----------------------------------------------------------------
///   Namespace     : CinemaProject.Service.UserService
///   Class         : IUserService
///   Description   : 
///   Author        : YOON             
///   Update Date   : 2022-08-01
///-----------------------------------------------------------------
namespace CinemaProject.Service.User;

public interface IUserService
{
    public IList<UserInfo> getUser(UserInfo userInfo);
}
