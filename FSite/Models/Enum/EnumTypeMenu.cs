using System.ComponentModel.DataAnnotations;

namespace FSite.Models.Enum
{
    public enum EnumTypeMenu
    {
        [Display(Name = "Tĩnh", Description = "/{0}")]
        Static,
        [Display(Name = "Trang", Description = "/{0}.html")]
        Post,
        [Display(Name = "Tin tức - Danh mục", Description = "/tin-tuc/{0}")]
        CategoryNews,
        [Display(Name = "Dịch vụ - Danh mục", Description = "/dich-vu/{0}")]
        ServiceCate,
        [Display(Name = "Dịch vụ", Description = "/{0}/dich-vu")]
        Service,
        [Display(Name = "Danh mục sản phẩm", Description = "/san-pham/{0}")]
        ProductCate,
        [Display(Name = "Hỏi đáp - Danh mục", Description = "/hoi-dap/{0}")]
        FaqCate,
        [Display(Name = "Hỏi đáp", Description = "/{0}/hoi-dap")]
        Faq,

    }
}