using Shop.Core.Logic;
using Shop.Core.Models;
using Shop.Core.ViewModels;
using Shop.DataAcess.InMemory;
using Shop.DataAcess.SQL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.WebUi.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> contextCategory;

        public ProductManagerController()
        {
            context = new SQLRepository<Product>(new MyContext());
            contextCategory = new SQLRepository<ProductCategory>(new MyContext());
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
        public ActionResult Create(Product Product, HttpPostedFileBase image)
        {


            if (!ModelState.IsValid)
            {
                return View(Product);
            }
            else
            {
                if (image != null)
                {
                    //Recupérer la valeur max de l'ID à partir de la base de donnée
                    int maxId;
                    try
                    {
                        // si la table est vide, la méthode Max renvoie null.
                        maxId = context.Collection().Max(p => p.Id);
                        
                    }
                    catch (Exception)
                    {

                        maxId = 0;
                    }
                    int nextId = maxId + 1;
                    Product.Image = nextId + Path.GetExtension(image.FileName);
                    image.SaveAs(Server.MapPath("~/Content/ProdImage/") + Product.Image);
                }
                context.Insert(Product);
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
        public ActionResult Edit(Product product, int id, HttpPostedFileBase image)
        {

            try
            {
                //Product prodTotEdit = context.FindById(id);
                //if (prodTotEdit == null)
                //{
                //    return HttpNotFound();
                //}
                //else
                //{
                    if (!ModelState.IsValid)
                    {
                        return View(product);
                    }
                    else
                    {

                        if (image != null)
                        {
                            product.Image = product.Id + Path.GetExtension(image.FileName);
                            image.SaveAs(Server.MapPath("~/Content/ProdImage/") + product.Image);
                        }
                        
                        context.Update(product);
                        


                        context.saveChanges();
                        return RedirectToAction("Index");
                    }
                //}


                
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