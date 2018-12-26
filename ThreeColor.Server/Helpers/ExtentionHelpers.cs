using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThreeColor.Data.Models;

namespace ThreeColor.Server.Helpers
{
    public static class ExtentionHelpers
    {
        public static bool CompareTo(this Users first, Users second)
        {
            return string.Equals(first.Activity, second.Activity) &&
                string.Equals(first.Gender, second.Gender) &&
                first.Age == second.Age;
        }
    }
}