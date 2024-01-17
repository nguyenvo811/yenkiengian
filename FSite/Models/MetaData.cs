using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FSite.Models
{
    public class MetaData
    {
        public Dictionary<string, string> Data { get; set; }

        public string Serialized
        {
            get { return JsonConvert.SerializeObject(Data); }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                var metaData = JsonConvert.DeserializeObject<Dictionary<string, string>>(value);
                Data = metaData ?? new Dictionary<string, string>();
            }
        }
    }

    #region Agency MetaData
    public class AgencyMetaData
    {
        [Display(Name = "FaceBook")]
        public string facebook { get; set; }
        [Display(Name = "Twitter")]
        public string twitter { get; set; }
        [Display(Name = "Google Plus")]
        public string Google_Plus { get; set; }
        [Display(Name = "Youtube")]
        public string youtube { get; set; }
        [Display(Name = "Zalo")]
        public string zalo { get; set; }
        [Display(Name = "linkedin")]
        public string linkedin { get; set; }
    }
    #endregion
}