using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentplex.Data.Model
{
    public class SliderMetaData
    {
        [Key]
        public int SlideID { get; set; }

        [Display(Name = "عنوان اسلایدر")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string SlideTitle { get; set; }

        [Display(Name = "تصویر")]
        public string SlideImage { get; set; }

        [Display(Name = "فعال/غیرفعال")]
        public bool SlideIsActive { get; set; }

        [Display(Name = "توضیحات")]
        [DataType(DataType.MultilineText)]
        public string SlideDesc { get; set; }
    }

    [MetadataType(typeof(SliderMetaData))]
    public partial class Slider
    {
    }
}
