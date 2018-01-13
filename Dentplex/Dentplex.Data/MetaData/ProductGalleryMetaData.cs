using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dentplex.Data.Model
{
    public class ProductGalleryMetaData
    {
        [Key]
        public int ProductGalleryID { get; set; }
        public int ProductID { get; set; }

        [Display(Name = "رنگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string ProductColor { get; set; }

        [Display(Name = "تصویر")]
        public string ProductImageName { get; set; }

        [Display(Name = "عنوان")]
        public string ProductImageTitle { get; set; }
        
    }

    [MetadataType(typeof(ProductGalleryMetaData))]
    public partial class ProductGallery
    {
    }
}
