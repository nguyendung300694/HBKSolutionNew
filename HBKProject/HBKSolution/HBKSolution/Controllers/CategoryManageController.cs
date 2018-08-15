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
    public class CategoryManageController : Controller
    {

        private readonly IProductService _prodService;
        private readonly IProductCategoryService _prodCateService;

        public CategoryManageController(IProductService prodService,
            IProductCategoryService prodCateService)
        {
            _prodService = prodService;
            _prodCateService = prodCateService;
        }

        [Authorize]
        // GET: CategoryManage
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddOrEditProductCategory(ProductCategoryViewModel model)
        {
            try
            {
                if (model.IsAdd)
                {
                    ProductCategory cate = new ProductCategory
                    {
                        FolderName = model.FolderName,
                        ProductCategoryName = model.ProductCategoryName,
                        CreatedDate = DateTime.Now
                    };

                    _prodCateService.AddProductCategory(cate);

                    ProductCategoryExtend cateEx = new ProductCategoryExtend
                    {
                        ProductCategoryId = cate.ProductCategoryId,
                        FileName = model.FileUpload.FileName,
                        FileSize = model.FileUpload.ContentLength,
                        FilePath = Util.CreateProductCategoryImage(model.FileUpload, User.Identity.GetUserId())
                    };

                    _prodCateService.AddProductCategoryExtend(cateEx);

                    var category = new
                    {
                        ProductCategoryName = cate.ProductCategoryName,
                        FilePath = cate.ProductCategoryExtend.FilePath,
                        ProductCategoryId = cate.ProductCategoryId
                    };

                    return Json(new { success = true, category = category, IsAdd = true });
                    //return Json(true);
                }
                else
                {
                    var cate = _prodCateService.GetProductCategoryById(model.ProductCategoryId);
                    if (model.FileUpload == null)
                    {
                        cate.ProductCategoryName = model.ProductCategoryName;
                        _prodCateService.Save();
                    }
                    else
                    {
                        Util.DeleteFileLocal(cate.ProductCategoryExtend.FilePath);
                        cate.ProductCategoryExtend.FilePath = Util.CreateProductCategoryImage(model.FileUpload, User.Identity.GetUserId());
                        cate.ProductCategoryExtend.FileName = model.FileUpload.FileName;
                        cate.ProductCategoryExtend.FileSize = model.FileUpload.ContentLength;
                        cate.ProductCategoryName = model.ProductCategoryName;
                        _prodCateService.Save();
                    }
                    var category = new
                    {
                        ProductCategoryName = cate.ProductCategoryName,
                        FilePath = cate.ProductCategoryExtend.FilePath,
                        ProductCategoryId = cate.ProductCategoryId
                    };

                    return Json(new { success = true, category = category, IsAdd = false });
                }
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public JsonResult DeleteCategoryById(int? categoryId)
        {
            if(categoryId!= null)
            {
                ProductCategory prodCate = _prodCateService.GetProductCategoryById((int)categoryId);
                if (prodCate != null)
                {
                    List<ProductExtend> listProdExtend = new List<ProductExtend>();
                    foreach(var item in prodCate.Products)
                    {
                        listProdExtend.Add(item.ProductExtend);
                        Util.DeleteFileLocal(item.ProductExtend.FilePath);
                    }
                    _prodService.DeleteListProductExtend(listProdExtend);
                    _prodService.DeleteListProduct(prodCate.Products.ToList());
                    Util.DeleteFileLocal(prodCate.ProductCategoryExtend.FilePath);
                    _prodCateService.DeleteCategoryExtend(prodCate.ProductCategoryExtend);
                    _prodCateService.DeleteProductCategory(prodCate);
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false });
        }
    }
}