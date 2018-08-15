
using HBKSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBKSolution.Services
{
    public interface IProductCategoryService
    {
        IEnumerable<ProductCategory> GetAllProductCatrgory();
        ProductCategory GetProductCategoryById(int? id);
        void AddProductCategory(ProductCategory prodCate);
        void AddProductCategoryExtend(ProductCategoryExtend prodCateExtend);
        void DeleteProductCategory(ProductCategory prodCate);
        void DeleteCategoryExtend(ProductCategoryExtend prodCateExtend);
        void Save();
    }

    public class ProductCategoryService : IProductCategoryService
    {
        private readonly ApplicationDbContext _db;

        public ProductCategoryService(IDataAccessService dAcess)
        {
            _db = dAcess.Init();
        }

        public void AddProductCategory(ProductCategory prodCate)
        {
            _db.ProductCategorys.Add(prodCate);
            Save();
        }

        public void AddProductCategoryExtend(ProductCategoryExtend prodCateExtend)
        {
            _db.ProductCategoryExtends.Add(prodCateExtend);
            Save();
        }

        public void DeleteCategoryExtend(ProductCategoryExtend prodCateExtend)
        {
            _db.ProductCategoryExtends.Remove(prodCateExtend);
            Save();
        }

        public void DeleteProductCategory(ProductCategory prodCate)
        {
            _db.ProductCategorys.Remove(prodCate);
            Save();
        }

        public IEnumerable<ProductCategory> GetAllProductCatrgory()
        {
            return _db.ProductCategorys;
        }

        public ProductCategory GetProductCategoryById(int? id)
        {
            return _db.ProductCategorys.Find(id);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}