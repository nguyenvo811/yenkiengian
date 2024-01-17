using System.ComponentModel.DataAnnotations;
namespace FSite.Models.Enum
{
    public enum EnumPostTemplate
    {
        [Display(Name = "None")]
        None,//không layout
        [Display(Name="Nội Dung 100%")]
        Full,
        [Display(Name = "Nội Dung nằm bên trái")]
        ContentLeft,
        [Display(Name = "Nội Dung nằm bên phải")]
        ContentRight,
        [Display(Name = "Nội Dung nằm bên trái - Cuộn trang")]
        Scroll_ContentLeft
    }
}