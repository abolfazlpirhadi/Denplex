
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Dentplex.Data.Model
{

using System;
    using System.Collections.Generic;
    
public partial class Product
{

    public int ProductID { get; set; }

    public int ProductGroupID { get; set; }

    public int ProductSubGroupID { get; set; }

    public string ProductTitle { get; set; }

    public string ProductShortText { get; set; }

    public string ProductText { get; set; }

    public string ProductImage { get; set; }



    public virtual ProductGroup ProductGroup { get; set; }

    public virtual ProductGroup ProductGroup1 { get; set; }

}

}
