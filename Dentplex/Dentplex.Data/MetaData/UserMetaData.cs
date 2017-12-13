using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentplex.Data.Model
{
    public class UserMetaData
    {
        [Key]
        public int UserID { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string UserName { get; set; }

        [Display(Name = "پسورد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string UserPassword { get; set; }

       

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [RegularExpression(".+\\@.+\\..+",ErrorMessage = "لطفا ایمیل صحیح وارد نمایید")]
        public string UserEmail { get; set; }

        [Display(Name = "وضعیت")]
        public bool UserIsActive { get; set; }
    }

    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {
    }

}
