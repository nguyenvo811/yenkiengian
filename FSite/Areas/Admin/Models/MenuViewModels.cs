using System.Collections.Generic;
using System.Linq;

namespace FSite.Areas.Admin.Models
{
    //public class MenuViewModels
    //{
    //}
    public class MenuTypeItem
    {
        public int? Id { get; set; }
        public string Key { get; set; }
        public string Title { get; set; }
    }
    //Use for nestable list menus
   
    public class MenuItemModels
    {
        public int id { get; set; }
        public string title { get; set; }
        public int? _index { get; set; }
        public int? _pId { get; set; }
      //  public int? _sub { get; set; }//_childCount
     //   public int? _level { get; set; }// 1 , 2 ,3 - select2totree

    }
    //public class HierarchicalHelper
    //{

    //    #region Gen Level
    //    public static IEnumerable<MenuItemModels> GetLevel(List<MenuItemModels> list)
    //    {
    //        foreach (var item in Level(list, null, 0))
    //        {
    //            var _m = list.FirstOrDefault(m => m.id == item.Key);
    //            _m._level = item.Value;
    //        }
    //        return list;
    //    }
    //    private static IEnumerable<KeyValuePair<int, int>> Level(List<MenuItemModels> list, int? parentId, int lvl)
    //    {
    //        return list
    //            .Where(x => x._pId == parentId)
    //            .SelectMany(x =>
    //                new[] { new KeyValuePair<int, int>(x.id, lvl) }.Concat(Level(list, x.id, lvl + 1)));
    //    }
    //    #endregion

    //}

}