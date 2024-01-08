using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
///-----------------------------------------------------------------
///   Namespace     : CinemaProject.Models.User
///   Class         : UserInfo
///   Description   : UserInfo
///   Author        : YOON                    
///   Update Date   : 2024-01-07
///-----------------------------------------------------------------
namespace CinemaProject.Models.User;

public class UserInfo
{
    public string user_num {get; set;}
    public string user_id {get; set;} = String.Empty;
    public string password {get; set;} = String.Empty;
    public string user_name {get; set;} = String.Empty;
    public DateTime birth {get; set;}
    public string gender {get; set;} = String.Empty;
    public string email {get; set;} = String.Empty;
    public string phone_number {get; set;} = String.Empty;
    public string post_code {get; set;} = String.Empty;
    public string address {get; set;} = String.Empty;
    public short status {get; set;}
    public DateTime reg_date {get; set;}
    public DateTime upt_date {get; set;}
}

