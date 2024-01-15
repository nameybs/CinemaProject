using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//-----------------------------------------------------------------
///   Namespace     : CinemaProject.Common
///   Class         : CommonUtil
///   Description   : 
///   Author        : YOON             
///   Update Date   : 2024-01-15
///-----------------------------------------------------------------
namespace CinemaProject.Common;

public class CommonUtil
{
    /// <summary>
    /// GetTextFromFile
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetTextFromFile(String path)
    {
        return System.IO.File.ReadAllText(path);
    }
}