///-----------------------------------------------------------------
///   Namespace     : CinemaProject.Controllers
///   Class         : UserController
///   Description   : 유저 컨트롤러
///   Author        : YOON                    
///   Update Date   : 2022-08-01
///-----------------------------------------------------------------
using Microsoft.AspNetCore.Mvc;

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
    
}