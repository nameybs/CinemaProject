///-----------------------------------------------------------------
///   Namespace     : CinemaProject.Controller
///   Class         : BaseController
///   Description   : 공통 모듈 컨트롤러
///   Author        : YOON                
///   Update Date   : 2022-07-27
///-----------------------------------------------------------------
using CinemaProject.Service;
using Microsoft.AspNetCore.Mvc;
using log4net;
using CinemaProject.Models.User;

namespace CinemaProject.Controllers;

public class BaseController : Controller
{
    private BaseService? baseService;
    private static readonly log4net.ILog logger = LogManager.GetLogger(typeof(BaseController));

    protected BaseController()
    {
    }

    protected internal T GetService<T>()
    {
        if (baseService == null) baseService = new BaseService();
        return baseService.GetService<T>();
    }

    protected internal UserInfo CurrentUser
    {
        get 
        {
            if(HttpContext.Session.GetString("user_num") == null)
            {
                return null;
            }

            UserInfo userInfo = new UserInfo();
            userInfo.user_num = HttpContext.Session.GetString("user_num");
            userInfo.user_name = HttpContext.Session.GetString("user_name");
            userInfo.birth = DateTime.Parse(HttpContext.Session.GetString("birth"));
            userInfo.gender = HttpContext.Session.GetString("gender");
            userInfo.email = HttpContext.Session.GetString("email");
            userInfo.phone_number = HttpContext.Session.GetString("phone_number");
            userInfo.post_code = HttpContext.Session.GetString("post_code");
            userInfo.address = HttpContext.Session.GetString("address");
            return userInfo;
        }
        set 
        {
            if(value != null)
            {
                HttpContext.Session.SetString("user_num", value.user_num);
                HttpContext.Session.SetString("user_name", value.user_name);
                HttpContext.Session.SetString("birth", value.birth.ToString());
                HttpContext.Session.SetString("gender", value.gender);
                HttpContext.Session.SetString("email", value.email);
                HttpContext.Session.SetString("phone_number", value.phone_number);
                HttpContext.Session.SetString("post_code", value.post_code);
                HttpContext.Session.SetString("address", value.address);
            }
            else
            {
                HttpContext.Session.Remove("user_num");
                HttpContext.Session.Remove("user_name");
                HttpContext.Session.Remove("birth");
                HttpContext.Session.Remove("gender");
                HttpContext.Session.Remove("email");
                HttpContext.Session.Remove("phone_number");
                HttpContext.Session.Remove("post_code");
                HttpContext.Session.Remove("address");
            }
        }
    }
}