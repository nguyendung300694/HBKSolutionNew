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
                        ProductCategoryId = cate.ProductCategoryId,
                        FolderName = cate.FolderName
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
                        ProductCategoryId = cate.ProductCategoryId,
                        FolderName = cate.FolderName
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
            if (categoryId != null)
            {
                //ProductCategory prodCate = _prodCateService.GetProductCategoryById((int)categoryId);
                //if (prodCate != null)
                //{
                //    foreach (var item in prodCate.Products)
                //    {
                //        var pathProd = item.ProductExtend.FilePath;
                //        ProductExtend prodEx = _prodService.GetProductExtendByProductId(item.ProductId);
                //        _prodService.DeleteProductExtend(prodEx);
                //        Util.DeleteFileLocal(pathProd);
                //    }
                //    List<Product> listProd = new List<Product>();
                //    listProd = _prodService.GetListProductByCategoryId((int)categoryId).ToList();
                //    _prodService.DeleteListProduct(listProd);
                //    _prodService.Save();
                //    _prodCateService.Save();
                //    var pathCate = prodCate.ProductCategoryExtend.FilePath;
                //    ProductCategoryExtend prodCateEx = _prodCateService.GetProductCategoryExtendByCategoryId((int)categoryId);
                //    _prodCateService.DeleteCategoryExtend(prodCateEx);
                //    ProductCategory prodCa = _prodCateService.GetProductCategoryById(categoryId);
                //    _prodCateService.DeleteProductCategory(prodCa);
                //    //Util.DeleteFileLocal(pathCate);
                //    _prodCateService.Save();

                //    return Json(new { success = true });
                //}
                foreach(var prod in _prodService.GetListProductByCategoryId((int)categoryId))
                {
                    Util.DeleteFileLocal(prod.ProductExtend.FilePath);
                    ProductExtend prodEx = _prodService.GetProductExtendByProductId(prod.ProductId);
                    _prodService.DeleteProductExtend(prodEx);
                }
                List<Product> listProd = new List<Product>();
                listProd = _prodService.GetListProductByCategoryId((int)categoryId).ToList();
                _prodService.DeleteListProduct(listProd);
                _prodService.Save();

                var prodCateEx = _prodCateService.GetProductCategoryExtendByCategoryId((int)categoryId);
                Util.DeleteFileLocal(prodCateEx.FilePath);
                _prodCateService.DeleteCategoryExtend(prodCateEx);
                var prodCate = _prodCateService.GetProductCategoryById((int)categoryId);
                _prodCateService.DeleteProductCategory(prodCate);
                _prodCateService.Save();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}