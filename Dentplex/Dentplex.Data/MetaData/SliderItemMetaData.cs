using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentplex.Data.Model
{
    public class SliderItemMetaData
    {
        [Key]
        public int SlideItemID { get; set; }

        [Display(Name = "اسلایدر")]
        public int SlideID { get; set; }

        [Display(Name = "عنوان اسلاید")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string SlideItemTitle { get; set; }

        [Display(Name = "تصویر")]
        //[Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string SlideItemImage { get; set; }

        [Display(Name = "لینک")]
        [Url]
        public string SlideItemLink { get; set; }

        [Display(Name = "ترتیب نمایش")]
        public int SlideItemOrder { get; set; }
    }

    [MetadataType(typeof(SliderItemMetaData))]
    public partial class SliderItem
    {
    }
}
