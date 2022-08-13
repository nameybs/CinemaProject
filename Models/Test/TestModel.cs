
using System;
///-----------------------------------------------------------------
///   Namespace     : CinemaProject.Model.Test
///   Class         : Test
///   Description   : Test
///   Author        : YOON             
///   Update Date   : 2022-07-30
///-----------------------------------------------------------------
namespace CinemaProject.Models.Test;
public class TestModel
{
    public String id    {get; set;} = String.Empty;
    public String name 	{get; set;} = String.Empty;
    public Int32 age	{get; set;}
    public DateTime birth {get; set;}
    public DateTime regdt	{get; set;}
    public DateTime uptdt	{get; set;} 
}