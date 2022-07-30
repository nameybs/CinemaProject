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
    private log4net.ILog? logger;
    
    protected internal T GetService<T>()
    {
        if (serviceFactory == null) serviceFactory = new ServiceFactory();
        return serviceFactory.GetService<T>();
    }

    protected internal log4net.ILog GetLog<T>()
    {
        if (logger == null) logger = LogManager.GetLogger(typeof(T));
        return logger;
    }
}