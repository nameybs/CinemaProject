using CinemaProject.Service.Common;
using CinemaProject.Service.Home;
using CinemaProject.Service.User;
using CinemaProject.Service.Test;
///-----------------------------------------------------------------
///   Namespace     : CinemaProject.Service
///   Class         : BaseService
///   Description   : 
///   Author        : YOON                
///   Update Date   : 2022-07-27
///-----------------------------------------------------------------
namespace CinemaProject.Service;

public class BaseService
{
    private HomeService homeService;
    private CommonService commonService;
    private UserService userService;
    private TestService testService;

    public T GetService<T>()
    {
        dynamic resultService;
        
        if (typeof(T).Equals(typeof(IHomeService))) resultService = GetHomeService();
        else if (typeof(T).Equals(typeof(ICommonService))) resultService = GetCommonService();
        else if (typeof(T).Equals(typeof(IUserService))) resultService = GetUserService();
        else if (typeof(T).Equals(typeof(ITestService))) resultService = GetTestService();
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
    
    private ITestService GetTestService()
    {
        if (testService == null) testService = new TestService();
        return testService;
    }
}
