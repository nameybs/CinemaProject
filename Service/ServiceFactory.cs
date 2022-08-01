using CinemaProject.Service.Common;
using CinemaProject.Service.Home;
using CinemaProject.Service.User;
///-----------------------------------------------------------------
///   Namespace     : CinemaProject.Service
///   Class         : ServiceFactory
///   Description   : 
///   Author        : YOON                
///   Update Date   : 2022-07-27
///-----------------------------------------------------------------
namespace CinemaProject.Service;

public class ServiceFactory
{
    private HomeService? homeService;
    private CommonService? commonService;
    private UserService? userService;

    public T GetService<T>()
    {
        dynamic resultService;
        
        if (typeof(T).Equals(typeof(IHomeService))) resultService = GetHomeService();
        else if (typeof(T).Equals(typeof(ICommonService))) resultService = GetCommonService();
        else if (typeof(T).Equals(typeof(IUserService))) resultService = GetUserService();
        else throw new System.Exception("Service Interface Error");

        return resultService;
    }

    private IHomeService GetHomeService()
    {
        if (homeService == null) homeService = new HomeService();
        return homeService;
    }

    private ICommonService GetCommonService()
    {
        if (commonService == null) commonService = new CommonService();
        return commonService;
    }

        private IUserService GetUserService()
    {
        if (userService == null) userService = new UserService();
        return userService;
    }
}
