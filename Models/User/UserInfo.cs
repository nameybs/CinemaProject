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
    public string user_num {get; set;} = String.Empty;
    public string user_id {get; set;} = String.Empty;
    public string password {get; set;} = String.Empty;
    public string? user_name {get; set;}
    public DateTime birth {get; set;}
    public string? gender {get; set;}
    public string? email {get; set;}
    public string? phone_number {get; set;}
    public string? post_code {get; set;}
    public string? address {get; set;}
    public short status {get; set;}
    public DateTime reg_date {get; set;}
    public DateTime upt_date {get; set;}

}

