//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PurchaseProduct.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Purchase_Product
    {
        public int PurchaseProductId { get; set; }
        public int Purchase_Id { get; set; }
        public string Item { get; set; }
        public int Quantity { get; set; }
        public int Amount { get; set; }
    }
}
