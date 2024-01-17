using System.ComponentModel.DataAnnotations;
namespace FSite.Models.Enum
{
    public enum EnumContentStatus
    {
        [Display(Name = "Mới")]
        New=0,
        [Display(Name = "Xử lý sau")]
        Pending=1,
        [Display(Name = "Đã xử lý")]
        Close=5
    }
}