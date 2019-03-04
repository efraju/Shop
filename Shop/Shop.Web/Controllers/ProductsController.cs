﻿namespace Shop.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Shop.Web.Data;
    using Shop.Web.Data.Entities;
    using Shop.Web.Helpers;
    using System.Threading.Tasks;

    public class ProductsController : Controller
    { 
        private readonly IUserHelper userHelper;

        public readonly IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository,IUserHelper userHelper)
        { 
            this.productRepository = productRepository;
            this.userHelper = userHelper;
        }

        // GET: Products
        public  IActionResult  Index()
        {
            return View(this.productRepository.GetAll());
        }

        // GET: Products/Details/5
        public  IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = this.productRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                //TODO: Limpiar

                product.User = await this.userHelper.GetUserByEmailAsync("efraju.56@gmail.com");
               await this.productRepository.CreateAsync(product); 
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product =await productRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>  Edit(Product product)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    product.User = await this.userHelper.GetUserByEmailAsync("efraju.56@gmail.com");
                    await this.productRepository.UpdateAsync(product);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.productRepository.ExistAsync(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public  IActionResult  Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = this.productRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product =await this.productRepository.GetByIdAsync(id);
           await this.productRepository.DeleteAsync(product); 
            return RedirectToAction(nameof(Index));
        }
         
    }
}
