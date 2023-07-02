///-----------------------------------------------------------------
///   Namespace     : CinemaProject.Common
///   Class         : Config
///   Description   : Init파일 취득 클래스
///   Author        : YOON                    
///   Update Date   : 2022-07-26
///-----------------------------------------------------------------
using System.Text;
using log4net;

namespace CinemaProject.Common;

public static class Config
{
    private static readonly log4net.ILog logger = LogManager.GetLogger(typeof(Config));
    public static Dictionary<String, String>? INIT_CONFIG;

    /// <summary>
    /// Init파일 정보 취득
    /// </summary>
    public static void ReadInitConfig() {
        try
        {
            logger.Info("===== Start Read InitConfig =====");
            using(StreamReader file = new StreamReader(Const.INIT_CONFIG_PATH, Encoding.GetEncoding("utf-8")))
            {
                String? line;
                INIT_CONFIG = new Dictionary<string, string>();
                while((line = file.ReadLine()) != null)  
                {
                    if(String.IsNullOrEmpty(line)) continue;
                    if(line.StartsWith("#")) continue;

                    String[] item = line.Split("==");
                    String key = item[0].Trim();
                    String value = item[1].Trim();
                    if(!INIT_CONFIG.ContainsKey(item[0]))
                    {
                        INIT_CONFIG.Add(key, value);
                    }
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

    /// <summary>
    /// Config값 취득
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetConfigValue(string key) {

        string value = string.Empty;
        if(INIT_CONFIG != null)
        {
            if(INIT_CONFIG[key] != null)
            {
                value = INIT_CONFIG[key];
            }
        }
        return value;
    }
}