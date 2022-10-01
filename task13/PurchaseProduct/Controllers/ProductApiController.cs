using Product;
using PurchaseProduct.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PurchaseProduct.Controllers
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqltransaction.rollback?view=dotnet-plat-ext-6.0
    //SqlTransaction.Rollback Method
    public class ProductApiController : ApiController
    {
        ItemEntities db = new ItemEntities();

        [Route("api/ProductApi/insertData")]
        [HttpPost]
        public IHttpActionResult insertData(abcTest[] obj1)
        {
            Class1 obj=new Class1();
            int totalAmount = 0;

            for(int i=0;i< obj1.Length; i++)
            {
                totalAmount = totalAmount + obj1[i].amount;
                obj.InsertDataIntoPurchase_Product(obj1[0].Purchase_Id,(string)obj1[i].Item,obj1[i].quantity,obj1[i].amount);
            }
            obj.InsertDataIntoPurchase_Table(obj1[0].Purchase_Id, obj1[0].Purchase_No, DateTime.Now, totalAmount);
            return Ok();
        }

        [Route("api/ProductApi/Updatedata")]
        [HttpPut]
        public IHttpActionResult Updatedata(abcTest[] obj1)
        {
            Class1 obj=new Class1();
            
            for (int i=0;i<obj1.Length;i++)
            {
                obj.UpdateDataIntoPurchase_Table(obj1[0].PurchaseProductId, obj1[0].Purchase_Id, obj1[i].amount);
                obj.UpdateDataIntoPurchase_Product(obj1[0].PurchaseProductId, obj1[i].quantity, obj1[i].amount);
            }
            return Ok();
        }

        [Route("api/ProductApi/deleteDataByPurchaseProductId")]
        [HttpDelete]
        public IHttpActionResult deleteDataByPurchaseProductId([FromBody]int id)
        {
            Class1 obj = new Class1();
            Purchase_Product purchase_Product = (from c in db.Purchase_Product where c.PurchaseProductId == id select c).FirstOrDefault();
            int purchaseId = (from c in db.Purchase_Product where c.PurchaseProductId == id select c.Purchase_Id).FirstOrDefault();
            int amount = (from c in db.Purchase_Product where c.PurchaseProductId == id select c.Amount).FirstOrDefault();
            if (purchase_Product != null)
            {
                Purchase_Table purchase_Table = (from c in db.Purchase_Table where c.Purchase_Id==purchaseId select c).FirstOrDefault();
                purchase_Table.Total=purchase_Table.Total-amount;
                db.SaveChanges();
                db.Purchase_Product.Remove(purchase_Product);
                db.SaveChanges();
            }
            return Ok();
        }


        [Route("api/ProductApi/deleteDataByPurchaseId")]
        [HttpDelete]
        public IHttpActionResult deleteDataByPurchaseId([FromBody]int id)
        {
            Purchase_Table purchase_Table=(from c in db.Purchase_Table where c.Purchase_Id==id select c).FirstOrDefault();
            db.Purchase_Table.Remove(purchase_Table);
            db.SaveChanges();
            Purchase_Product p = (from c in db.Purchase_Product where c.Purchase_Id == id select c).FirstOrDefault();
            while(p!=null)
            {
                db.Purchase_Product.Remove(p);
                db.SaveChanges();
                p = (from c in db.Purchase_Product where c.Purchase_Id == id select c).FirstOrDefault();
            }
            return Ok();
        }
    }
    public class abcTest
    {      
        [NotMapped]
        public int PurchaseProductId { get; set; }
        public int Purchase_Id { get; set; }
        public string Purchase_No { get; set; }
        public string Item { get; set; }
        public int quantity { get; set; }
        public int amount { get; set; }
    }
}
