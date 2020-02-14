using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productcategories;

        public ProductCategoryRepository()
        {
            productcategories = cache["productcategories"] as List<ProductCategory>;
            if (productcategories == null)
            {
                productcategories = new List<ProductCategory>();

            }
        }
        public void Commit()
        {
            cache["productcategories"] = productcategories;
        }

        public void Insert(ProductCategory p)
        {
            productcategories.Add(p);
        }

        public void Update(ProductCategory productcategory)
        {
            ProductCategory productCategoryToUpdate = productcategories.Find(p => p.Id == productcategory.Id);
            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = productcategory;

            }
            else
            {
                throw new Exception("Product Category not Found");

            }
        }
        public ProductCategory Find(string Id)
        {
            ProductCategory productcategory = productcategories.Find(p => p.Id == Id);
            if (productcategory != null)
            {
                return productcategory;
            }
            else
            {
                throw new Exception("Product Not Found");

            }

        }
        public IQueryable<ProductCategory> Collection()
        {
            return productcategories.AsQueryable();
        }


        public void Delete(string Id)
        {
            ProductCategory productCategoryToDelete = productcategories.Find(p => p.Id == Id);
            if (productCategoryToDelete != null)
            {
                productcategories.Remove(productCategoryToDelete);

            }
            else
            {
                throw new Exception("Product Category not Found");

            }

        }
    }
}
