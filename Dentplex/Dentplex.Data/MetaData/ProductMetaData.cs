using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dentplex.Data.Model
{
    public class ProductMetaData
    {
        [Key]
        public int ProductID { get; set; }

        [Display(Name = "گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int ProductGroupID { get; set; }

        [Display(Name = "زیر گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int ProductSubGroupID { get; set; }

        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string ProductTitle { get; set; }

        [Display(Name = "شرح محصول")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string ProductShortText { get; set; }

        [Display(Name = "توضیح")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string ProductText { get; set; }

        [Display(Name = "تصویر")]
        public string ProductImage { get; set; }
    }

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
    }
}
