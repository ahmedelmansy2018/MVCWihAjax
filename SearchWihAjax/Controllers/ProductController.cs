using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SearchWihAjax.Models;
namespace SearchWihAjax.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/
        CompanyEntities db = new CompanyEntities();
        public ActionResult Index(string Term=null)
        {
            List<Product> Products = (from pr in db.Products
                                      where pr.Name.StartsWith(Term) || pr.Name == null
                                      select pr).ToList();
            if (Products.Count<=0||Products==null)
            {
                return View(db.Products);
            }
            if(Request.IsAjaxRequest())
            {
                return PartialView("_ProductPartial", Products);
            }
            return View(db.Products);


        }
        public PartialViewResult All()
        {
            return PartialView("_ProductPartial", db.Products);
        }
        public PartialViewResult Max3 ()
        {
            var Products = (from p in db.Products
                            orderby p.Unitprice descending
                            select p).Take(3);
            //OrderByDescending(p=>p.Unitprice).Take(3);
         
            return PartialView("_ProductPartial", Products);
            }
        public PartialViewResult Min3()
        {
            var Products = (from p in db.Products
                            orderby p.Unitprice ascending
                            select p).Take(3);
            return PartialView("_ProductPartial", Products);
        }
    [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
    public JsonResult Create(Product Product)
    {
        db.Products.Add(Product);
      int val=  db.SaveChanges();
      if (val > 0) 
      
      { 
          return Json(new {Status=1,Message="successed"},JsonRequestBehavior.AllowGet); 
      
}
      else
      {
          return Json(new { Status = 0, Message = "Failed" }, JsonRequestBehavior.AllowGet); 
      }
    }
        public JsonResult getProducts()
        {
            var Products = from pr in db.Products
                           join gt in db.Categories
                           on pr.Categoryid equals gt.Categoryid
                           select new
                           {pr.Productid,

                               pr.Name,
                               pr.Unitprice,
                               pr.UnitsInstook,
                               gt.Titel

                           };

            return Json(Products, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getProductByid(int id)
        {

         
            Product p=   db.Products.Find(id);

            return Json(p, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Edit(int id, Product NewProduct)
        {
            Product oldProduct = db.Products.Find(id);
            oldProduct.Name = NewProduct.Name;
            oldProduct.Unitprice = NewProduct.Unitprice;
            oldProduct.UnitsInstook = NewProduct.UnitsInstook;
            oldProduct.Categoryid = NewProduct.Categoryid;
            db.Entry(oldProduct).State = System.Data.EntityState.Modified;


            int val = db.SaveChanges();
            if (val > 0)
            {
                return Json(new { Status = 1, Message = "successed" }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { Status = 0, Message = "Failed" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Delete(int id)
        {
            Product oldProduct = db.Products.Find(id);
            db.Products.Remove(oldProduct);
            int val = db.SaveChanges();
            if (val > 0)
            {
                return Json(new { Status = 1, Message = "successed" }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { Status = 0, Message = "Failed" }, JsonRequestBehavior.AllowGet);
            }
        }



    }
}
