using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CinemaProject.Models;
using CinemaProject.Models.Home;
using CinemaProject.Service.Home;
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

    public IActionResult Test()
    {
        logger.Info("Logtest");
        IHomeService service = GetService<IHomeService>();
        IList<Test>? list = null;
        Test result;
        int count = 0;

        Test test = new Test();
        test.name = "TESTUSER";
        test.age = 20;
        test.birth = new DateTime(2022, 07, 30);

        logger.Info("INSERT DATA");
        count = service.insertTest(test);
        logger.Info("INSERT COUNT : " + count);
        
        logger.Info("MULTIPLE SELECT DATA");
        list = service.multipleSelectTest();
        logger.Info("MULTIPLE SELECT COUNT : " + list.Count);
        foreach (var item in list)
        {
            logger.Info(string.Format("ID : {0}, NAME : {1} , AGE : {2}, BIRTH : {3}",
                        item.id, item.name, item.age, item.birth.ToShortDateString()));
        }
        test = list[list.Count - 1];
        test.name = "TESTUSER2";
        test.age = 30;
        test.birth = new DateTime(2022, 08, 01);

        logger.Info("UPDATE DATA");
        count = service.updateTest(test);
        logger.Info("UPDATE COUNT : " + count);

        logger.Info("SELECT DATA");
        result = service.selectTest(test);
        logger.Info(string.Format("ID : {0}, NAME : {1} , AGE : {2}, BIRTH : {3}",
                            result.id, result.name, result.age, result.birth.ToShortDateString()));

        logger.Info("DELETE DATA");
        count = service.deleteTest(result);
        logger.Info("DELETE COUNT : " + count);

        IList<Test> list2 = new List<Test>();
        for (int i = 0; i < 3; i++)
        {
            
            Test temp = new Test();
            temp.name = "INSERTUSER" + i;
            temp.age = 20 + i;
            temp.birth = new DateTime(22, 07, 30);
            list2.Add(temp);
        }

        logger.Info("MUTIPLE INSERT DATA");
        count = service.multipleInsertTest(list2);
        logger.Info("MUTIPLE INSERT COUNT : " + count);

        logger.Info("MULTIPLE SELECT DATA");
        list = service.multipleSelectTest();
        logger.Info("MULTIPLE SELECT COUNT : " + list.Count);
        list2.Clear();
        foreach (var item in list)
        {
            logger.Info(string.Format("ID : {0}, NAME : {1} , AGE : {2}, BIRTH : {3}",
                        item.id, item.name, item.age, item.birth.ToShortDateString()));
            
            list2.Add(item);
        }

        logger.Info("MULTIPLE DELETE DATA");
        count = service.multipleDeleteTest(list2);
        logger.Info("MULTIPLE DELETE COUNT : " + count);

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
