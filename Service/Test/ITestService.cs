using CinemaProject.Models.Test;
//-----------------------------------------------------------------
///   Namespace     : CinemaProject.Service.Test
///   Class         : ITestService
///   Description   : 
///   Author        : YOON             
///   Update Date   : 2022-07-26
///-----------------------------------------------------------------
namespace CinemaProject.Service.Test;
public interface ITestService
{
    public TestModel selectTest(TestModel test);
    public Dictionary<string,object> selectTest(Dictionary<string,object> test);
    public IList<TestModel> multipleSelectTest();
    public int insertTest(TestModel test);
    public int multipleInsertTest(IList<TestModel> list);
    public int updateTest(TestModel test);
    public int deleteTest(TestModel test);
    public int multipleDeleteTest(IList<TestModel> list);

}