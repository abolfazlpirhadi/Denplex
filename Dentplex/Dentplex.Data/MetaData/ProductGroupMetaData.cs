using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentplex.Data.Model
{
    public class ProductGroupMetaData
    {
        [Key]
        public int ProductGroupID { get; set; }
        public int ProductParentGroupID { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string ProductGroupTitle { get; set; }
    }

    [MetadataType(typeof(ProductGroupMetaData))]
    public partial class ProductGroup
    {
    }
}
