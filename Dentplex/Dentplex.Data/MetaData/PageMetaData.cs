using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dentplex.Data.Model
{
    public class PageMetaData
    {
        [Key]
        public int PageID { get; set; }

        [Display(Name = "نام صفحه")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string PageName { get; set; }

        [Display(Name = "عنوان صفحه")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string PageTitle { get; set; }

        [Display(Name = "متن صفحه")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string PageText { get; set; }

        [Display(Name = "فعال/غیرفعال")]
        public bool PageIsActive { get; set; }
    }

    [MetadataType(typeof(PageMetaData))]
    public partial class Page
    {
    }
}
