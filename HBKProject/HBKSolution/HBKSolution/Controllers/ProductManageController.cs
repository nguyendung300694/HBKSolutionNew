using HBKSolution.Models;
using HBKSolution.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HBKSolution.Controllers
{
    [Authorize]
    public class ProductManageController : Controller
    {
        private readonly IProductService _prodService;
        private readonly IProductCategoryService _prodCateService;

        public ProductManageController(IProductService prodService,
            IProductCategoryService prodCateService)
        {
            _prodService = prodService;
            _prodCateService = prodCateService;
        }

        // GET: ProductManage
        public ActionResult Index()
        {
            var listCategory = _prodCateService.GetAllProductCatrgory();
            ViewBag.ListCategory = listCategory;
            return View();
        }

        [HttpPost]
        public JsonResult AddOrEditProduct(ProductViewModel model)
        {
            try
            {
                if (model.IsAdd)
                {
                    Product prod = new Product
                    {
                        Price = model.Price,
                        ProductContent = model.ProductContent,
                        ProductCategoryId = model.ProductCategoryId,
                        ProductName = model.ProductName,
                        View = 0,
                        CreatedDate = DateTime.Now
                    };
                    _prodService.CreateProduct(prod);
                    var cate = _prodCateService.GetProductCategoryById(model.ProductCategoryId);
                    ProductExtend prodEx = new ProductExtend
                    {
                        ProductId = prod.ProductId,
                        FileName = model.FileUpload.FileName,
                        FileSize = model.FileUpload.ContentLength,
                        FilePath = Util.CreateProductImage(model.FileUpload, User.Identity.GetUserId(), cate.FolderName)
                    };
                    _prodService.CreateProductExtend(prodEx);
                    var product = new
                    {
                        FilePath = prod.ProductExtend.FilePath,
                        ProductName = prod.ProductName,
                        Price = prod.Price,
                        View = prod.View,
                        ProductContent = prod.ProductContent,
                        ProductId = prod.ProductId
                    };
                    return Json(new { success = true, product = product, IsAdd = true });
                }
                else
                {
                    var prod = _prodService.GetProductById(model.ProductId);
                    if (model.FileUpload == null)
                    {
                        prod.ProductName = model.ProductName;
                        prod.ProductContent = model.ProductContent;
                        prod.Price = model.Price;
                        _prodService.Save();
                    }
                    else
                    {
                        Util.DeleteFileLocal(prod.ProductExtend.FilePath);
                        prod.ProductExtend.FilePath = Util.CreateProductImage(model.FileUpload, User.Identity.GetUserId(), prod.ProductCategory.FolderName);
                        prod.ProductExtend.FileName = model.FileUpload.FileName;
                        prod.ProductExtend.FileSize = model.FileUpload.ContentLength;
                        prod.ProductName = model.ProductName;
                        prod.ProductContent = model.ProductContent;
                        prod.Price = model.Price;
                        _prodService.Save();
                    }
                    var product = new
                    {
                        FilePath = prod.ProductExtend.FilePath,
                        ProductName = prod.ProductName,
                        Price = prod.Price,
                        View = prod.View,
                        ProductContent = prod.ProductContent,
                        ProductId = prod.ProductId
                    };
                    return Json(new { success = true, product = product, IsAdd = false });
                }
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public JsonResult DeleteProductById(int? categoryId, int? productId)
        {
            if (categoryId != null & productId != null)
            {
                Product prod = _prodService.GetAllProduct().First(m => m.ProductCategoryId == categoryId & m.ProductId == productId);
                if (prod != null)
                {
                    Util.DeleteFileLocal(prod.ProductExtend.FilePath);
                    _prodService.DeleteProductExtend(prod.ProductExtend);
                    _prodService.DeleteProduct(prod);
                    _prodService.Save();
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false });
        }
    }
}