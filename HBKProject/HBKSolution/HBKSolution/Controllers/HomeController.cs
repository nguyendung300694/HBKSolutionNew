using HBKSolution.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HBKSolution.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _prodService;
        private readonly IProductCategoryService _prodCateService;

        public HomeController(IProductService prodService,
            IProductCategoryService prodCateService)
        {
            _prodCateService = prodCateService;
            _prodService = prodService;
        }

        public ActionResult Index()
        {
            var listCategory = _prodCateService.GetAllProductCatrgory();
            var listHotProduct = _prodService.GetNineHotProduct();
            ViewBag.ListHotProduct = listHotProduct.ToList();
            ViewBag.ListCategory = listCategory;
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult NavDropdownItems()
        {
            var listCategory = _prodCateService.GetAllProductCatrgory();
            ViewBag.ListCategory = listCategory;
            return PartialView("~/Views/Shared/_NavDropdownItem.cshtml");
        }

        [ChildActionOnly]
        public PartialViewResult Footer()
        {
            var listCategory = _prodCateService.GetAllProductCatrgory();
            ViewBag.ListCategory = listCategory;
            return PartialView("~/Views/Shared/_Footer.cshtml");
        }
    }
}