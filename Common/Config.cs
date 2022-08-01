using log4net;
///-----------------------------------------------------------------
///   Namespace     : CinemaProject.Common
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
                Const.INIT_CONFIG = new Dictionary<string, string>();
                while((line = file.ReadLine()) != null)  
                {
                    if(String.IsNullOrEmpty(line)) continue;
                    if(line.StartsWith("#")) continue;

                    String[] item = line.Split("==");
                    String key = item[0].Trim();
                    String value = item[1].Trim();
                    Const.INIT_CONFIG.Add(key, value);;
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
