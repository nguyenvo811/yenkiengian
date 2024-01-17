using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FSite.Helpers
{
    public class CommonHelper
    {
     public static   System.Globalization.CultureInfo cultureInfo = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");

        public static string CurencyDisplay(object v)
        {
            if (v != null)
            {
                if (v is decimal)
                {
                    if (((decimal)v) != 0)
                    {
                        return ((decimal)v).ToString("#,###", cultureInfo.NumberFormat) + " đ";
                    }
                }
            }
            return "0 đ";
        }

        public static string DecimalDisplay(object v)
        {
            if (v != null)
            {
                //if (v is decimal)
                //{
                //    if (((decimal)v)!=0)
                //    {
                return v.ToString().Replace(".00", String.Empty);
                // }
                //}
            }
            return "0";
        }
        public static string DateDisplay(DateTime d)
        {
            return d.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}