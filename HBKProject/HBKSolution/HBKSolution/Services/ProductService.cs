using HBKSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBKSolution.Services
{
    public interface IProductService
    {
        void Save();
        IEnumerable<Product> GetAllProduct();
        IEnumerable<Product> GetNineHotProduct();
        void CreateProduct(Product product);
        void CreateProductExtend(ProductExtend productExtend);
        void IncreaseProductView(int productId);
        Product GetProductById(int productId);
        IEnumerable<Product> GetListProductByCategoryId(int categoryId);
        void DeleteListProduct(List<Product> listProd);
        void DeleteListProductExtend(List<ProductExtend> listProdExtend);
        void DeleteProduct(Product prod);
        void DeleteProductExtend(ProductExtend prodExtend);
        ProductExtend GetProductExtendByProductId(int prodId);
    }

    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _db;

        public ProductService(IDataAccessService dAccess)
        {
            _db = dAccess.Init();
        }

        public void CreateProduct(Product product)
        {
            _db.Products.Add(product);
            Save();
        }

        public void CreateProductExtend(ProductExtend productExtend)
        {
            _db.ProductExtends.Add(productExtend);
            Save();
        }

        public void DeleteListProduct(List<Product> listProd)
        {
            _db.Products.RemoveRange(listProd);
        }

        public void DeleteListProductExtend(List<ProductExtend> listProdExtend)
        {
            _db.ProductExtends.RemoveRange(listProdExtend);
        }

        public void DeleteProduct(Product prod)
        {
            _db.Products.Remove(prod);
        }

        public void DeleteProductExtend(ProductExtend prodExtend)
        {
            _db.ProductExtends.Remove(prodExtend);
        }

        public IEnumerable<Product> GetAllProduct()
        {
            return _db.Products;
        }

        public IEnumerable<Product> GetListProductByCategoryId(int categoryId)
        {
            return _db.Products.Where(p => p.ProductCategoryId == categoryId);
        }

        public IEnumerable<Product> GetNineHotProduct()
        {
            return _db.Products.OrderByDescending(m => m.View).Take(9);
        }

        public Product GetProductById(int productId)
        {
            return _db.Products.First(m => m.ProductId == productId);
        }

        public ProductExtend GetProductExtendByProductId(int prodId)
        {
            return _db.ProductExtends.Where(m => m.ProductId == prodId).Single();
        }

        public void IncreaseProductView(int productId)
        {
            var product = GetProductById(productId);
            product.View += 1;
            Save();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}