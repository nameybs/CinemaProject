using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
///-----------------------------------------------------------------
///   Namespace     : CinemaProject.Models.User
///   Class         : UserInfo
///   Description   : UserInfo
///   Author        : YOON                    
///   Update Date   : 2024-01-06
///-----------------------------------------------------------------
namespace CinemaProject.Models.User;

public class UserInfo
{
    public String userId {get; set;} = String.Empty;
    public String userPassword {get; set;} = String.Empty;
}

