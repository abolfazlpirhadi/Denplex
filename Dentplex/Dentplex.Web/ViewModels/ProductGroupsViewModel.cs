﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dentplex.Web.ViewModels
{
    public class ProductGroupsViewModel
    {
        public int ProductGroupID { get; set; }
        public Nullable<int> ProductParentGroupID { get; set; }

        [Display(Name = "عنوان")]
        public string ProductGroupTitle { get; set; }

        public string ProductGroupName { get; set; }

        [Display(Name = "عکس اصلی")]
        public string ProductGroupImage { get; set; }
        [Display(Name = "عکس بنر")]
        public string ProductGroupBanner { get; set; }
    }
}