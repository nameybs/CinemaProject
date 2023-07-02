///-----------------------------------------------------------------
///   Namespace     : CinemaProject.Controller
///   Class         : TestController
///   Description   : 테스트 컨트롤러
///   Author        : YOON                
///   Update Date   : 2022-07-27
///-----------------------------------------------------------------
using Microsoft.AspNetCore.Mvc;
using CinemaProject.Models.Test;
using CinemaProject.Service.Test;
using log4net;

namespace CinemaProject.Controllers;

public class TestController : BaseController
{
    private static readonly log4net.ILog logger = LogManager.GetLogger(typeof(TestController));

    public IActionResult Index()
    {
        logger.Info("Logtest");
        ITestService service = GetService<ITestService>();
        IList<TestModel>? list = null;
        TestModel result;
        Dictionary<string, object> dicResult;
        int count = 0;

        TestModel test = new TestModel();
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

        logger.Info("SELECT DATA(Object)");
        result = service.selectTest(list[0]);
        logger.Info(string.Format("OBJECT RESULT : ID : {0}, NAME : {1} , AGE : {2}, BIRTH : {3}",
                    result.id, result.name, result.age, result.birth.ToShortDateString()));

        Dictionary<string, object> dicParam = new Dictionary<string, object>();
        dicParam.Add("id", list[0].id);
        logger.Info("SELECT DATA(Dictionary)");
        dicResult = service.selectTest(dicParam);
        logger.Info(string.Format("DIC RESULT : ID : {0}, NAME : {1} , AGE : {2}, BIRTH : {3}",
                    dicResult["id"], dicResult["name"], dicResult["age"], dicResult["birth"]));

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

        IList<TestModel> list2 = new List<TestModel>();
        for (int i = 0; i < 3; i++)
        {
            
            TestModel temp = new TestModel();
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
}
