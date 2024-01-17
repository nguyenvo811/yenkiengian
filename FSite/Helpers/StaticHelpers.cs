using FSite.Models;
using System;
using System.Linq;
namespace FSite.Helpers
{
    public static class StaticHelpers
    {
        //public static StaticFrontEndMetaData GetMeta(this System.Web.Mvc.HtmlHelper helper, string path, Object value)
        //  {
        //    System.Xml.Linq.XDocument oXmlDocument = System.Xml.Linq.XDocument.Load(path);
        //    var items = (from item in oXmlDocument.Descendants("StaticViewModel")
        //                 where Convert.ToInt32(item.Element("Id").Value) == Convert.ToInt32(value)
        //                 select item.Element("MetaStr").Value
        //                 ).SingleOrDefault();
        //    StaticFrontEndMetaData meta = new StaticFrontEndMetaData();
        //        if (!string.IsNullOrEmpty(items))
        //            meta = Newtonsoft.Json.JsonConvert.DeserializeObject<StaticFrontEndMetaData>(items);
        //    return meta;
        //  }
        public static StaticFrontEndMetaData GetMeta(string path)
        {
            System.Xml.Linq.XDocument oXmlDocument = System.Xml.Linq.XDocument.Load(path);
            var items = (from item in oXmlDocument.Descendants("StaticViewModel")
                         select item.Element("MetaStr").Value
                         ).SingleOrDefault();
            StaticFrontEndMetaData meta = new StaticFrontEndMetaData();
            if (!string.IsNullOrEmpty(items))
                meta = Newtonsoft.Json.JsonConvert.DeserializeObject<StaticFrontEndMetaData>(items);
            return meta;
        }
        public static StaticFrontEndMetaData GetMeta(string path, Object value)
        {
            System.Xml.Linq.XDocument oXmlDocument = System.Xml.Linq.XDocument.Load(path);
            var items = (from item in oXmlDocument.Descendants("StaticViewModel")
                         where Convert.ToInt32(item.Element("Id").Value) == Convert.ToInt32(value)
                         select item.Element("MetaStr").Value
                         ).SingleOrDefault();
            StaticFrontEndMetaData meta = new StaticFrontEndMetaData();
            if (!string.IsNullOrEmpty(items))
                meta = Newtonsoft.Json.JsonConvert.DeserializeObject<StaticFrontEndMetaData>(items);
            return meta;
        }
    }
}