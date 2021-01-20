using Shop.Core.Logic;
using Shop.Core.Models;
using Shop.DataAcess.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.WebUi.Controllers
{
    public class AchatController : Controller
    {
        IRepository<Product> context;
        List<Product> lstProd = new List<Product>();
        public AchatController()
        {
            context = new SQLRepository<Product>(new MyContext());
            
        }
        // GET: Achat
        public ActionResult Ajouter(int id)
        {

            Product p = context.FindById(id);
            if (Session["Products"] == null)
            {
                lstProd.Add(p);
                Session["Products"] = lstProd;
                Session["nbProd"] = 1;
            }

            else
            {
                lstProd = (List < Product >) Session["Products"];
                lstProd.Add(p);
                Session["Products"] = lstProd;
                Session["nbProd"] = lstProd.Count;

                decimal total = 0;
                foreach (var item in lstProd)
                {
                    total += item.Price;
                }
                Session["total"] = total;

            }
            return RedirectToAction("Index","Home");
        }

        public ActionResult Panier()
        {
            lstProd = (List<Product>)Session["Products"];
            return View(lstProd);

        }

    }
}