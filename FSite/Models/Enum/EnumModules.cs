using System.ComponentModel.DataAnnotations;
namespace FSite.Models.Enum
{
    //public class EnumModules
    //{
    //}
    public enum EnumSliderModules
    {
        [Display(Name = "Slider Banner - Trang chủ", Description = "_SliderHome")]
        Home,
        [Display(Name = "Slider - Tin tức", Description = "_SliderBlog")]
        Blog,
        [Display(Name = "Slider - Dịch vụ", Description = "_SliderService")]
        Service,
      
        [Display(Name = "Slider - Sản phẩm", Description = "_SliderProduct")]
        Product,
        [Display(Name = "Slider - Hỏi đáp", Description = "_SliderFaq")]
        Faq,
        [Display(Name = "Slider - Liên hệ", Description = "_SliderConact")]
        Conact

    }

}