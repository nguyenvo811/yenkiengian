using System.ComponentModel.DataAnnotations;
namespace FSite.Models.Enum
{
    public enum EnumUserRole
    {
        [Display(Name = "Người dùng")]
        Visitor,
        //[Display(Name = "Đại Lý")]
        //Affiliate,
        [Display(Name = "Quản trị")]
        Operator,
        [Display(Name = "Quản trị hệ thống")]
        Admin

    }
}