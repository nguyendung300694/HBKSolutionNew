using HBKSolution.Models;
using HBKSolution.Services;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HBKSolution.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {

        private readonly IDataProjectService _dataProjectService;
        private readonly IProductService _prodService;
        private readonly IProductCategoryService _prodCateService;

        public ProductController(IDataProjectService dataProjectService, IProductService prodService,
            IProductCategoryService prodCateService)
        {
            _dataProjectService = dataProjectService;
            _prodService = prodService;
            _prodCateService = prodCateService;
        }

        [AllowAnonymous]
        // GET: Product
        public ActionResult ProductDetails(int? producId)
        {
            Product product = null;
            if (producId != null)
            {
                _prodService.IncreaseProductView((int)producId);
                product = _prodService.GetProductById((int)producId);
            }
            if (product != null)
            {
                return View(product);
            }
            return View();
        }

        [AllowAnonymous]
        // GET: Products
        public ActionResult Products(int? productCategoryId)
        {
            //_dataProjectService.AddAllData();
            if (productCategoryId != null)
            {
                var productCategory = _prodCateService.GetProductCategoryById(productCategoryId);
                ViewBag.CategoryName = productCategory?.ProductCategoryName;
                ViewBag.ListProduct = productCategory?.Products;
            }
            return View();
        }

        [HttpPost]
        public JsonResult GetListProductByCategoryId(int? categoryId)
        {
            if (categoryId != null)
            {
                IEnumerable listProduct = _prodService.GetListProductByCategoryId((int)categoryId).Select(m => new
                {
                    FilePath = m.ProductExtend.FilePath,
                    ProductName = m.ProductName,
                    Price = m.Price,
                    View = m.View,
                    ProductContent = m.ProductContent
                }).ToList();

                return Json(new { listProduct = listProduct });
            }
            return Json(new { listProduct = new List<Product>() });
        }

        [HttpPost]
        public JsonResult GetAllProductCategory()
        {
            IEnumerable listProdCategory = _prodCateService.GetAllProductCatrgory()
                .OrderByDescending(m => m.CreatedDate).Select(m => new
                {
                    ProductCategoryId = m.ProductCategoryId,
                    FilePath = m.ProductCategoryExtend.FilePath,
                    ProductCategoryName = m.ProductCategoryName
                }).ToList();
            return Json(new { listProdCategory = listProdCategory });
        }

        [HttpPost]
        public JsonResult GetProductCategoryById(int? categoryId)
        {
            if (categoryId != null)
            {
                var category = _prodCateService.GetAllProductCatrgory().Where(m => m.ProductCategoryId == (int)categoryId)
                    .Select(s => new
                    {
                        FilePath = s.ProductCategoryExtend.FilePath,
                        ProductCategoryId = s.ProductCategoryId,
                        ProductCategoryName = s.ProductCategoryName,
                        FolderName = s.FolderName
                    }).Single();

                return Json(new { success = true, category = category });
            }
            return Json(new { success = false });
        }
    }
}