using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using Shop.Core.Models;

namespace Shop.DataAcess.InMemory
{
    public class ProductCategoryRepository
    { 
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;




        public ProductCategoryRepository()

        {
            productCategories = cache["productCategories"] as List<ProductCategory>;

            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }

        }

        public void saveChanges()
        {
            cache["productCategories"] = productCategories;
        }

        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }
        public void Update(ProductCategory p)
        {
            ProductCategory prodToUpdate = productCategories.Find(prod => prod.Id == p.Id);

            if (prodToUpdate != null)
            {
                prodToUpdate = p;
            }
            else
            {
                throw new Exception("Product category not found");
            }
        }

        public ProductCategory FindById(int id)
        {
            ProductCategory p = productCategories.Find(prod => prod.Id == id);
            if (p != null)
            {
                return p;
            }
            else
            {
                throw new Exception("Product category not found");
            }
        }


        // Le type IQueryable accepte les requètes LINQ contrairement à une liste classique
        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        public void Delete(int id)
        {
            ProductCategory prodToDelete = productCategories.Find(prod => prod.Id == id);

            if (prodToDelete != null)
            {
                productCategories.Remove(prodToDelete);
            }
            else
            {
                throw new Exception("Product category not found");
            }
        }

    }
}
