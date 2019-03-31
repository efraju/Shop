using Microsoft.EntityFrameworkCore;
using Shop.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Data
{
    public class ProductRepository:GenericRepository<Product>,IProductRepository
    {
        public DataContext Context { get; }

        public ProductRepository(DataContext context):base(context)
        {
            Context = context;
        }


        public IQueryable GetAllWithUsers()
        {
            return this.Context.Products.Include(p=>p.User);
        }
    }
}
