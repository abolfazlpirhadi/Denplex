using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentplex.Data.Model
{
    public class ProductMetaData
    {
        [Key]
        public int ProductID { get; set; }
        public int ProductGroupID { get; set; }
        public int ProductSubGroupID { get; set; }

        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string ProductTitle { get; set; }
        public string ProductShortText { get; set; }
        public string ProductText { get; set; }
        public string ProductImage { get; set; }
    }

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
    }
}
