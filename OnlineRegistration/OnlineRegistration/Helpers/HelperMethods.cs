using OnlineRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineRegistration.Helpers
{
    public static class HelperMethods
    {
        public static string GetImageSource(this Course course)
        {
            if (course?.FileContent == null)
                return "";

            var base64 = Convert.ToBase64String(course.FileContent);
            return string.Format("data:image;base64,{0}", base64);
        }
    }
}