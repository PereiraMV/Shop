using Shop.Core.Logic;
using Shop.Core.Models;
using Shop.DataAcess.InMemory;
using Shop.DataAcess.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.WebUi.Controllers
{
    public class ProductCategoryController : Controller
    {
        IRepository<ProductCategory> context;

        public ProductCategoryController()
        {
            context = new SQLRepository<ProductCategory>(new MyContext());
        }




        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> products = context.Collection().ToList();
            return View(products);
        }


        public ActionResult Create()
        {
            ProductCategory p = new ProductCategory();
            return View(p);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(ProductCategory p)
        {


            if (!ModelState.IsValid)
            {
                return View(p);
            }
            else
            {
                context.Insert(p);
                context.saveChanges();
                return RedirectToAction("Index");
            }

        }

        public ActionResult Edit(int id)
        {
            try
            {
                ProductCategory p = context.FindById(id);
                if (p == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(p);
                }
            }
            catch (Exception)
            {
                return HttpNotFound();

            }

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(ProductCategory product, int id)
        {

            try
            {
                ProductCategory prodTotEdit = context.FindById(id);
                if (prodTotEdit == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    if (!ModelState.IsValid)
                    {
                        return View(product);
                    }
                    else
                    {
                        //context.Update(product); ce n'est pas un context Entity Framework

                        prodTotEdit.Category = product.Category;
                        prodTotEdit.Id = product.Id;




                        context.saveChanges();
                        return RedirectToAction("Index");
                    }
                }



            }
            catch (Exception)
            {

                return HttpNotFound();
            }


        }

        public ActionResult Delete(int id)
        {
            try
            {
                ProductCategory p = context.FindById(id);

                if (p == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(p);
                }
            }
            catch (Exception)
            {

                return HttpNotFound();
            }

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {

            try
            {
                ProductCategory p = context.FindById(id);

                if (p == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    context.Delete(id);
                    context.saveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                return HttpNotFound();
            }

        }


    }
}