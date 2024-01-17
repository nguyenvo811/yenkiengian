namespace FSite.Models
{
    public class DatatablesResultModels
    {
        public DatatablesResultModels()
        {
            draw = 1;
        }
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public object data { get; set; }


    }
 public class DatatablesModels
    {
        public object data { get; set; }
    }
}