using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace FSite.Models
{
    public class StaticViewModel
    {
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
        public string Location
        {
            get;
            set;
        }
        public bool IsEdit
        {
            get;
            set;
        }
        public string MetaStr { get; set; }
        public virtual MetaData Meta { get; set; }
    }

    public class StaticFrontEndMetaData
    {
        public StaticFrontEndMetaData()
        {
            Seo = new StaticSeoMetaData();
            Agent = new StaticContactMetaData();
            Mail = new StaticMailMetaData();
            Loan = new Static_Loan_MetaData();
            Map = new StaticMapMetaData();
        }
        [Display(Name = "Logo top")]
        public string Header_Logo { get; set; }
        [Display(Name = "Logo trang giới thiệu")]
        public string Header_Logo1 { get; set; }

        #region Social  

        [Display(Name = "Hotline")]
        public string Hotline { get; set; }

        [Display(Name = "Email")]
        public string Header_Email { get; set; }
        [Display(Name = "FaceBook")]
        public string facebook { get; set; }
        [Display(Name = "Twitter")]
        public string twitter { get; set; }

        [Display(Name = "Youtube")]
        public string youtube { get; set; }
        [Display(Name = "Zalo")]
        public string zalo { get; set; }
        [Display(Name = "linkedin")]
        public string linkedin { get; set; }

        #endregion

        [Display(Name = "Tiêu đề liên hệ", Description = "Nội dung kế bên nút liên hệ")]
        public string Footer_Contact { get; set; }
        [Display(Name = "Chân trang")]
        public string Footer { get; set; }
        [Display(Name = "Chân trang điện thoại")]
        public string Footer_Mobile { get; set; }

        //liên hệ basic trang liên hệ
        [Display(Name = "Trang liên hệ")]
        public string Contact { get; set; }
        [Display(Name = "Url Api", Description = "http://.../")]
        public string ApiUrl { get; set; }
        [Display(Name = "Domain", Description = "http://...")]
        public string domain { get; set; }

        #region Sliders
        public IEnumerable<StaticSliderItemMetaData> files { get; set; }
        #endregion
        #region locations
        public IEnumerable<StaticPointMetaData> locations { get; set; }
        #endregion  
        #region locations
        public IEnumerable<StaticProduct_PolicyMetaData> policies { get; set; }
        #endregion
        //<!--  gui toi admin cua web -->
        //<add key = "ContactLat" value="10.799496" />
        //<add key = "ContactLng" value="106.72346140000002" />



        public StaticSeoMetaData Seo { get; set; }
        public StaticContactMetaData Agent { get; set; }
        public StaticMailMetaData Mail { get; set; }
        public StaticMapMetaData Map { get; set; }
        public Static_Loan_MetaData Loan { get; set; }

        public StaticProviderMetaData Provider { get; set; }

    }

    #region MetaItem
    public class StaticProviderMetaData
    {
        public StaticProviderItemMetaData Google { get; set; }
    }
    public class StaticProviderItemMetaData
    {
        [Display(Name = "Api Key")]//run map api key
        public string Api { get; set; }

    }

    public class StaticContactMetaData
    {
        [Display(Name = "Hình đại diện")]
        public string Avatar { get; set; }
        [Display(Name = "Họ tên")]
        public string FullName { get; set; }
        [Display(Name = "Tài khoản")]
        public string User { get; set; }

        [Display(Name = "Điện thoại")]
        public string Phone { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Add { get; set; }
    }
    public class StaticSeoMetaData
    {
        [Display(Name = "Site chia sẻ")]
        public string SiteUri { get; set; }
        [Display(Name = "Meta - Hình")]
        public string SiteLogo { get; set; }

        [Display(Name = "Meta - Tiêu đề")]
        public string SiteTitle { get; set; }
        [Display(Name = "Meta - Keyword")]
        public string SiteKeyWord { get; set; }
        [Display(Name = "Meta - Mô tả")]
        public string SiteDesc { get; set; }
    }

    public class StaticMailMetaData
    {
        public StaticMailMetaData()
        {
            deliveryMethod = "Network";
            defaultCredentials = false;
            enableSsl = false;
        }

        [Display(Name = "1 Mail Nhận", Description = "tponguyen86@gmail.com")]
        public string Recieve { get; set; }
        [Display(Name = "Gửi Đến 1 Mail")]
        public string From { get; set; }

        [Display(Name = "Tiêu đề email nhận")]
        public string SysMailDisplay { get; set; }
        [Display(Name = "Danh sách BCC", Description = "tponguyen86@gmail.com;tponguyen86@gmail.com;")]
        public string SystemsMailBCC { get; set; }
        [Display(Name = "Danh sách CC", Description = "tponguyen86@gmail.com;tponguyen86@gmail.com;")]
        public string SystemsMailCC { get; set; }


        #region smtp
        //     <mailSettings>
        //  <smtp deliveryMethod = "Network" from="traminguyenhuynh@gmail.com">
        //    <network defaultCredentials = "false" host="smtp.gmail.com" port="587" userName="traminguyenhuynh@gmail.com" password="trami@123654" enableSsl="true" />
        //  </smtp>
        //</mailSettings>

        [Display(Name = "deliveryMethod", Description = "Network")]
        public string deliveryMethod { get; set; }
        [Display(Name = "defaultCredentials", Description = "false")]
        public bool defaultCredentials { get; set; }

        [Display(Name = "host", Description = "smtp.gmail.com")]
        public string host { get; set; }
        [Display(Name = "port", Description = "587")]
        public string port { get; set; }

        [Display(Name = "userName", Description = "")]
        public string userName { get; set; }
        [Display(Name = "password", Description = "")]
        public string password { get; set; }
        [Display(Name = "enableSsl", Description = "true")]
        public bool enableSsl { get; set; }
        #endregion
    }
    //public class StaticItemMetaData
    //{
    //    [Display(Name = "Hiển thị")]
    //    public string Text { get; set; }//text
    //    [Display(Name = "Giá trị")]
    //    public string Value { get; set; }//value


    //}
    #region Loan

    public class Static_Loan_MetaData
    {
        //loanAmount       : '$1,000.00',
        //loanDuration     : '12',
        //  valueAddedTax    : '16%',
        //  serviceFee       : '5%',
        //  paymentFrequency : 'monthly'
        public Static_Loan_MetaData()
        {
            Duration = 12;
            AddedTax = 0;

        }
        [Display(Name = "Tháng mặc định (Duration):")]
        public int Duration { get; set; }
        [Display(Name = "Thuế (AddedTax %):")]
        public float AddedTax { get; set; }

        [Display(Name = "Phí dịch vụ (ServiceFee %):")]
        public float ServiceFee { get; set; }

        [Display(Name = "Phương Thức Thanh Toán(Payment frequency):")]
        public Static_Loan_PaymentFrequency_MetaData PaymentFrequency { get; set; }

        public IEnumerable<Static_Loan_Bank_MetaData> banks { get; set; }
    }
    public enum Static_Loan_PaymentFrequency_MetaData
    {
        [Display(Name = "Mỗi tuần")]
        weekly,
        [Display(Name = "Hai Tuần")]
        biweekly,
        [Display(Name = "Mỗi tháng")]
        monthly
    }
    public class Static_Loan_Bank_MetaData
    {
        public Static_Loan_Bank_MetaData()
        {
            //Title = "Mặc định";
            //CreditScore = "7.6";
        }
        [Display(Name = "Mã")]
        public int? Id { get; set; }
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }
        [Display(Name = "Mã")]
        public string Ma { get; set; }

        [Display(Name = "Điểm tín dụng (Credit Score)")]
        public string CreditScore { get; set; }
        [Display(Name = "Mô tả")]
        public string Desc { get; set; }

        [Display(Name = "Website")]
        public string Url { get; set; }
        [Display(Name = "Đường dẫn tệp tin")]
        public string Path { get; set; }
        [Display(Name = "Vị trí")]
        public int? Index { get; set; }
        [Display(Name = "Sử dụng")]
        public bool? IsActive { get; set; }
    }
    #endregion

    #region Map
    public class StaticMapMetaData
    {

        [Display(Name = "Icon bản đồ")]
        public string Path { get; set; }


    }
    #endregion

    public class StaticSliderItemMetaData
    {
        public int Id { get; set; }
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }
        [Display(Name = "Mô tả")]
        public string Desc { get; set; }

        [Display(Name = "Đường dẫn chi tiết")]//link detail
        public string Url { get; set; }

        public string FileName { get; set; }
        [Display(Name = "Đường dẫn tệp tin")]
        public string Path { get; set; }
        [Display(Name = "Vị trí")]
        public int? Index { get; set; }
        [Display(Name = "Sử dụng")]
        public bool? IsActive { get; set; }
        public string ContentType { get; set; }

    }
    public class StaticPointMetaData
    {
        public StaticPointMetaData()
        {
            Zoom = 14;
        }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Display(Name = "Nhúng")]
        public string Embed { get; set; }
        [Display(Name = "Hình")]
        public string Icon { get; set; }
        [Display(Name = "Lat")]
        public string Lat { get; set; }
        [Display(Name = "Lng")]
        public string Lng { get; set; }
        [Display(Name = "Phóng to")]
        public int? Zoom { get; set; }


        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }
        [Display(Name = "Mô tả")]
        public string Desc { get; set; }
        [Display(Name = "Sắp xếp")]
        public int? Index { get; set; }
        [Display(Name = "Kích hoạt")]
        public bool? IsActive { get; set; }
    }

    public class StaticProduct_PolicyMetaData
    {
        public StaticProduct_PolicyMetaData()
        {
          
        }
        [Display(Name = "Hình")]
        public string Icon { get; set; }
     
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }
        [Display(Name = "Mô tả")]
        public string Desc { get; set; }
        [Display(Name = "Sắp xếp")]
        public int? Index { get; set; }
        [Display(Name = "Kích hoạt")]
        public bool? IsActive { get; set; }
    }
    #endregion


    #region x_editable
    public class x_editable
    {
        //public string type { get; set; } 
        public string id { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        public EnumControl? type { get; set; }
        public string url { get; set; }
        public string pk { get; set; }



        public string config { get; set; }//ext

    }
    public enum EnumControl
    {
        [Display(Name = "text", Description = "_text")]
        text,
        [Display(Name = "textarea", Description = "_textarea")]
        textarea,
        [Display(Name = "select", Description = "_select")]
        select,
        [Display(Name = "date", Description = "_date")]
        date,
        [Display(Name = "datetime", Description = "_datetime")]
        datetime,
        [Display(Name = "dateui", Description = "_dateui")]
        dateui,
        [Display(Name = "combodate", Description = "_combodate")]
        combodate,
        [Display(Name = "html5types", Description = "_html5types")]
        html5types,
        [Display(Name = "checklist", Description = "_checklist")]
        checklist,
        [Display(Name = "wysihtml5", Description = "_wysihtml5")]
        wysihtml5,
        [Display(Name = "typeahead", Description = "_typeahead")]
        typeahead,
        [Display(Name = "typeaheadjs", Description = "_typeaheadjs")]
        typeaheadjs,
        [Display(Name = "select2", Description = "_select2")]
        select2
    }
    #endregion
}