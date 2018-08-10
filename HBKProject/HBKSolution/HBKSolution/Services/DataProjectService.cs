using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HBKSolution.Models;
using System.IO;

namespace HBKSolution.Services
{
    public interface IDataProjectService
    {
        void AddAllData();
    }

    public class DataProjectService : IDataProjectService
    {
        private readonly IProductCategoryService _prodCateService;
        private readonly ApplicationDbContext _db;
        public DataProjectService(IProductCategoryService prodCateService, IDataAccessService dAccess)
        {
            _prodCateService = prodCateService;
            _db = dAccess.Init();
        }

        public void AddAllData()
        {
            List<ProductCategoryAddToDB> productDBList = new List<ProductCategoryAddToDB>();
            productDBList.Add(new ProductCategoryAddToDB
            {
                Name = "Băng tải",
                ImageLocalLink = "~/Content/images/ProductCategoryImg/Bangtai1.jpg"
            });
            productDBList.Add(new ProductCategoryAddToDB
            {
                Name = "Bộ phận điều khiển",
                ImageLocalLink = "~/Content/images/ProductCategoryImg/bophandieukhien.jpg"
            });
            productDBList.Add(new ProductCategoryAddToDB
            {
                Name = "Máy nén khí",
                ImageLocalLink = "~/Content/images/ProductCategoryImg/Maynenkhi1.jpg"
            });
            productDBList.Add(new ProductCategoryAddToDB
            {
                Name = "Thiết bị truyền động",
                ImageLocalLink = "~/Content/images/ProductCategoryImg/thietbitruyendong.jpg"
            });

            var directories = Directory.GetDirectories(HttpContext.Current.Server.MapPath("~/Content/images/ProductImg"));

            #region PRODUCT_CATEGORY
            if (!_db.ProductCategorys.Any())
            {
                foreach (var item in productDBList)
                {
                    //create category and category extend start
                    var cate = new ProductCategory
                    {
                        ProductCategoryName = item.Name,
                        CreatedDate = DateTime.Now
                    };
                    _db.ProductCategorys.Add(cate);
                    _db.SaveChanges();
                    FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath(item.ImageLocalLink));
                    _db.ProductCategoryExtends.Add(new ProductCategoryExtend
                    {
                        ProductCategoryId = cate.ProductCategoryId,
                        FileName = file.Name,
                        FilePath = item.ImageLocalLink,
                        FileSize = (int)file.Length
                    });
                    _db.SaveChanges();
                    // ./create category and category extend end

                    //create product and product extend by category id start
                    var directoryName = "";
                    var index = productDBList.IndexOf(item);
                    if (index == 1)
                    {
                        directoryName = Path.GetFileName(directories[1]);
                    }
                    else if (index == 2)
                    {
                        directoryName = Path.GetFileName(directories[0]);
                    }
                    else if (index == 3)
                    {
                        directoryName = Path.GetFileName(directories[2]);
                    }

                    var filePaths = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/Content/images/ProductImg/" + directoryName));
                    var num = 1;
                    foreach (var filePath in filePaths)
                    {
                        var fileName = Path.GetFileName(filePath);
                        var prod = new Product
                        {
                            CreatedDate = DateTime.Now,
                            ProductCategoryId = cate.ProductCategoryId,
                            ProductContent = "",
                            ProductName = "San pham " + num,
                            View = 0
                        };
                        _db.Products.Add(prod);
                        _db.SaveChanges();
                        var serverPath = "~/Content/images/ProductImg/" + directoryName + "/" + fileName;
                        FileInfo prodInfo = new FileInfo(HttpContext.Current.Server.MapPath(serverPath));
                        _db.ProductExtends.Add(new ProductExtend
                        {
                            FileName = prodInfo.Name,
                            FilePath = serverPath,
                            FileSize = (int)prodInfo.Length,
                            ProductId = prod.ProductId
                        });
                        _db.SaveChanges();
                        num++;
                    }
                    //create product and product extend by category id end

                }
            }
            #endregion


        }
    }
}