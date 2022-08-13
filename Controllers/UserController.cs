using Microsoft.AspNetCore.Mvc;
///-----------------------------------------------------------------
///   Namespace     : CinemaProject.Controllers
///   Class         : UserController
///   Description   : UserController
///   Author        : YOON                    
///   Update Date   : 2022-08-01
///-----------------------------------------------------------------
namespace CinemaProject.Controllers;

public class UserController : BaseController
{
    public IActionResult Login()
    {
        return View();
    }
    
}