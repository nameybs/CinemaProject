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

namespace CinemaProject.Controllers;

public class BaseController : Controller
{
    private BaseService? baseService;
    private static readonly log4net.ILog logger = LogManager.GetLogger(typeof(BaseController));

    protected BaseController()
    {
    }

    internal T GetService<T>()
    {
        if (baseService == null) baseService = new BaseService();
        return baseService.GetService<T>();
    }
}