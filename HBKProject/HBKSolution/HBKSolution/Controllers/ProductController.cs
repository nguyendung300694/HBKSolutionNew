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

                //foreach (var item in _prodService.GetAllProduct())
                //{
                //    item.Price = 0;
                //    _prodService.Save();
                //}

                return Json(new { listProduct = listProduct });
            }
            return Json(new { listProduct = new List<Product>() });
        }

    }
}