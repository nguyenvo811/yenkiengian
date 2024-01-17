using System.ComponentModel.DataAnnotations;
namespace FSite.Models.Enum
{
    public enum EnumTypeRequest
    {
        [Display(Name = "Liên Hệ", Description = "General_Contact")]
        General_Contact,
        [Display(Name = "Liên Hệ", Description = "General_Contact_Page")]
        General_Contact_Page,
        [Display(Name = "Đặt lịch hẹn", Description = "General_Book_Home")]
        General_Book_Home,
        [Display(Name = "Đặt lịch hẹn", Description = "General_Book_SideBar")]
        General_Book_SideBar,
        [Display(Name = "Đặt lịch hẹn", Description = "General_Book_Modal")]
        General_Book_Modal
    }
}