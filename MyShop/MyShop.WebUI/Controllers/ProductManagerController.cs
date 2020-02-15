using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModel;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
       IRepository<Product> context;
        IRepository<ProductCategory> productcategories;
        // GET: ProductManager

        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory>productCategoryContext)
        {
            context = productContext;
            productcategories = productCategoryContext;
        }
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);

        }
        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.product = new Product();
            viewModel.ProductCategories = productcategories.Collection();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.product = product;
                viewModel.ProductCategories = productcategories.Collection();
                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Edit(Product product, string Id)
        {
            Product productToEdit = context.Find(Id);
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                productToEdit.Category = product.Category;
                productToEdit.Name = product.Name;
                productToEdit.Description = product.Description;
                productToEdit.image = product.image;
                productToEdit.Price = product.Price;
                context.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}