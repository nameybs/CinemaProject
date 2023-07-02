///-----------------------------------------------------------------
///   Namespace     : CinemaProject.Controller
///   Class         : HomeController
///   Description   : 기본 컨트롤러
///   Author        : YOON                
///   Update Date   : 2022-07-27
///-----------------------------------------------------------------
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CinemaProject.Models;
using log4net;

namespace CinemaProject.Controllers;

public class HomeController : BaseController
{
    private static readonly log4net.ILog logger = LogManager.GetLogger(typeof(HomeController));

    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
