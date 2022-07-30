using log4net;
///-----------------------------------------------------------------
///   Namespace     : Common
///   Class         : Config
///   Description   : Read InitConfig
///   Author        : YOON                    
///   Update Date   : 2022-07-26
///-----------------------------------------------------------------
namespace CinemaProject.Common;

public static class Config
{
    private static readonly log4net.ILog logger = LogManager.GetLogger(typeof(Config));

    /// <summary>
    /// Read IninConfig
    /// </summary>
    public static void ReadInitConfig() {
        try
        {
            logger.Info("===== Start Read InitConfig =====");
            using(StreamReader file = new StreamReader(Const.INIT_CONFIG_PATH))
            {
                String? line;
                while((line = file.ReadLine()) != null)  
                {
                    if(String.IsNullOrEmpty(line)) continue;
                    if(line.StartsWith("#")) continue;

                    // DB Config
                    if(line.StartsWith("DBCONFIG")) Const.DB_CONFIG = line.Split("==")[1].Trim();
                }
            }
            logger.Info("===== End Read InitConfig =====");
        }
        catch (Exception)
        {
            logger.Info("===== Fail Read InitConfig =====");
            throw;
        }
    }
}
