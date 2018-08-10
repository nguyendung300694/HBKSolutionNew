using HBKSolution.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HBKSolution.Controllers
{
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
    }
}