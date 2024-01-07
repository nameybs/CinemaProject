///-----------------------------------------------------------------
///   Namespace     : CinemaProject.Controllers
///   Class         : UserController
///   Description   : 유저 컨트롤러
///   Author        : YOON                    
///   Update Date   : 2024-01-06
///-----------------------------------------------------------------
using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MailKit.Security;
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
        HttpContext.Session.Remove("user_num");

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
        HttpContext.Session.SetString("user_num", resultInfo[0].user_num);

        return Json(0);
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
    public IActionResult EmailVerify()
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse("nameybs@gmail.com"));
        email.To.Add(MailboxAddress.Parse("nameybs@gmail.com"));
        email.Subject = "Test Email";
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html) 
        {
            Text = "test"
        };

        using (var smtp = new SmtpClient()) {
            smtp.Connect("smtp.gmail.com", 587, false);
            smtp.Authenticate("nameybs@gmail.com", "rnzoodfqbcnwqhlr");
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        return View();
    }
    
}