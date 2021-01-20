using Shop.Core.Logic;
using Shop.Core.Models;
using Shop.Core.ViewModels;
using Shop.DataAcess.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.WebUi.Controllers
{
    
    public class HomeController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> contextCategory;

        public HomeController()
        {
            context = new SQLRepository<Product>(new MyContext());
            contextCategory = new SQLRepository<ProductCategory>(new MyContext());
        }

        public ActionResult Index(string category = null)
        {
            List<Product> products;
            List<ProductCategory> categories = contextCategory.Collection().ToList();
            if (category == null)
            {
                products = context.Collection().ToList();
            }
            else
            {
                products = context.Collection().Where(p => p.Category == category).ToList();
            }

            ProductListViewModel viewModel = new ProductListViewModel();
            viewModel.Products = products;
            viewModel.ProductCategories = categories;
            return View(viewModel);
        }

        public ActionResult Details(int id)
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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}