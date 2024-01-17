namespace FSite.Areas.Admin.Models
{
    public class MapViewModels
    {

    }
    public class MapPointModel
    {
        private string location { get; set; }
        public string Location {
            get
            {
                if (string.IsNullOrEmpty(location))
                    return System.Configuration.ConfigurationManager.AppSettings.Get("ContactAdd");
                return location;
            }
            set
            {
                this.location = value;
            }
        }
        public decimal? Lat { get; set; }
        public decimal? Lng { get; set; }
       
        public string _Lat
        {
            get
            {
                if (Lat.HasValue)
                    return Lat.Value.ToString().Replace(",", ".");
                return System.Configuration.ConfigurationManager.AppSettings.Get("ContactLat").Replace(",", ".");
            }
            //set
            //{
            //    this.name = value;
            //}
        }
        public string _Lng
        {
            get
            {
                if (Lng.HasValue)
                    return Lng.Value.ToString().Replace(",", ".");
                return System.Configuration.ConfigurationManager.AppSettings.Get("ContactLng").Replace(",", ".");
            }
            //set
            //{
            //    this.name = value;
            //}
        }


    }
}