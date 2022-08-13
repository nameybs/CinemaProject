///-----------------------------------------------------------------
///   Namespace     : CinemaProject.Common
///   Class         : Const
///   Description   : Const
///   Author        : YOON                    
///   Update Date   : 2022-07-26
///-----------------------------------------------------------------
namespace CinemaProject.Common;

public static class Const
{
    /// <summary>
    /// Init Config File Path
    /// </summary>
    public const String INIT_CONFIG_PATH = @"init.config";
    
    /// <summary>
    /// Init Config Const
    /// </summary>
    public static String DB_CONFIG = "DBCONFIG";
    
    /// <summary>
    /// RegEx Const
    /// </summary>
    /// <value></value>
    public const String REGEX_USER_NAME = @".{1,40}";
    public const String REGEX_USERID = @"[a-z0-9]{20}";
    public const String REGEX_EMAIL = @"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$";
    public const String REGEX_PASSWORD = @".{4,20}";
    public const String REGEX_DEPTNO = @"[0-9]{4}";
    public const String REGEX_DEPTNAME = @".{1,100}";


}