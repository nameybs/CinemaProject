using CinemaProject.Models.Home;
//-----------------------------------------------------------------
///   Namespace     : CinemaProject.Service.Home
///   Class         : IHomeService
///   Description   : 
///   Author        : YOON             
///   Update Date   : 2022-07-26
///-----------------------------------------------------------------
namespace CinemaProject.Service.Home;
public interface IHomeService
{
    public Test selectTest(Test test);
    public IList<Test> multipleSelectTest();
    public int insertTest(Test test);
    public int multipleInsertTest(IList<Test> list);
    public int updateTest(Test test);
    public int deleteTest(Test test);
    public int multipleDeleteTest(IList<Test> list);

}