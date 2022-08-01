using CinemaProject.Service;
using Microsoft.AspNetCore.Mvc;
using log4net;
///-----------------------------------------------------------------
///   Namespace     : CinemaProject.Controller
///   Class         : BaseController
///   Description   : 
///   Author        : YOON                
///   Update Date   : 2022-07-27
///-----------------------------------------------------------------
namespace CinemaProject.Controllers;

public class BaseController : Controller
{
    private ServiceFactory? serviceFactory;
    private static readonly log4net.ILog logger = LogManager.GetLogger(typeof(BaseController));

    public BaseController()
    {
    }
    
    protected internal T GetService<T>()
    {
        if (serviceFactory == null) serviceFactory = new ServiceFactory();
        return serviceFactory.GetService<T>();
    }
}