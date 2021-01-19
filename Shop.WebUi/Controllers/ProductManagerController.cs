using Shop.Core.Models;
using Shop.Core.ViewModels;
using Shop.DataAcess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.WebUi.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;
        ProductCategoryRepository contextCategory;

        public ProductManagerController()
        {
            context = new ProductRepository();
            contextCategory = new ProductCategoryRepository();
        }




        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }


        public ActionResult Create()
        {
            ProductCategoryViewModel viewModel = new ProductCategoryViewModel();
            viewModel.Product = new Product();
            viewModel.ProductCategories = contextCategory.Collection();
            return View(viewModel);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Product p)
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
                Product p = context.FindById(id);
                if (p == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    ProductCategoryViewModel viewModel = new ProductCategoryViewModel();
                    viewModel.Product = p;
                    viewModel.ProductCategories = contextCategory.Collection();
                    return View(viewModel);
                }
            }
            catch (Exception)
            {
                return HttpNotFound();

            }

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Product product, int id)
        {

            try
            {
                Product prodTotEdit = context.FindById(id);
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
                        prodTotEdit.Name = product.Name;
                        prodTotEdit.Description = product.Description;
                        prodTotEdit.Category = product.Category;
                        prodTotEdit.Id = product.Id;
                        prodTotEdit.Price = product.Price;
                        prodTotEdit.Image = product.Image;
                        


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
                Product p = context.FindById(id);

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
                Product p = context.FindById(id);

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