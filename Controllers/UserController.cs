///-----------------------------------------------------------------
///   Namespace     : CinemaProject.Controllers
///   Class         : UserController
///   Description   : 유저 컨트롤러
///   Author        : YOON                    
///   Update Date   : 2024-01-06
///-----------------------------------------------------------------
using Microsoft.AspNetCore.Mvc;
using CinemaProject.Models.User;
using CinemaProject.Service.User;

namespace CinemaProject.Controllers;

public class UserController : BaseController
{
    /// <summary>
    /// Login
    /// </summary>
    /// <returns></returns>
    public IActionResult Login()
    {
        return View();
    }

    /// <summary>
    /// Logout
    /// </summary>
    /// <returns></returns>
    public RedirectResult Logout()
    {
        // 로그인 세션 삭제
        this.CurrentUser = null;

        return Redirect("/Home");
    }

    /// <summary>
    /// LoginCheck
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public JsonResult LoginCheck(UserInfo userInfo) 
    {
        // 입력정보 체크
        if(userInfo.user_id.Trim() == String.Empty 
            || userInfo.password.Trim() == String.Empty) return Json(1);

        // 유저 정보 취득
        IUserService userService = GetService<IUserService>();
        IList<UserInfo> resultInfo = userService.getUser(userInfo);
        
        // 취득결과 체크
        if(resultInfo.Count == 0 || resultInfo.Count > 1) return Json(2);
        
        // 세션에 유저 정보 추가
        this.CurrentUser = resultInfo[0];

        return Json(0);
    }

    /// <summary>
    /// ConfirmEmail
    /// </summary>
    /// <returns></returns>
    public IActionResult ConfirmEmail()
    {
        return View();
    }

    /// <summary>
    /// Terms Conditions
    /// </summary>
    /// <returns></returns>
    public IActionResult TermsConditions()
    {
        return View();
    }

    /// <summary>
    /// Sign Up
    /// </summary>
    /// <returns></returns>
    public IActionResult SignUp()
    {
        return View();
    }

    /// <summary>
    /// EmailVerify
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public JsonResult EmailCheck(String email)
    {
        if(email == null || email == String.Empty) return Json(1);

        IUserService userService = GetService<IUserService>();
        if(userService.getEmailCount(email) != 0)
        {
            return Json(2);
        }

        int value = userService.emailVerify(email);
        if(value == 0)
        {
            return Json(3);
        }
        else
        {
            HttpContext.Session.SetInt32("mailVerify", value);
        }
        return Json(0);
    }
}