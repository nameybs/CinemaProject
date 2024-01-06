///-----------------------------------------------------------------
///   Namespace     : CinemaProject.Controllers
///   Class         : UserController
///   Description   : 유저 컨트롤러
///   Author        : YOON                    
///   Update Date   : 2022-08-01
///-----------------------------------------------------------------
using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MailKit.Security;

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