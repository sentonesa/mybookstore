﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;


namespace MyBookstore.App_Code
{
    public class Katulong
    {
        public static string GetCon()
        {
            return ConfigurationManager.ConnectionStrings["MyCon"].ConnectionString;
        }
    }
}