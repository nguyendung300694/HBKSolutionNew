using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HBKSolution.Services
{
    public static class Util
    {
        public static string CreateProductImage(HttpPostedFileBase file, string userId)
        {
            string virtualPath = "~/Content/images/ProductImg/" + userId;
            string FolderPath = HttpContext.Current.Server.MapPath(virtualPath);
            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);
            //string FilePath = Path.Combine(FolderPath, file.FileName);
            file.SaveAs(Path.Combine(FolderPath, file.FileName));
            return virtualPath + "/" + file.FileName + "?w=708&h=472";
        }

        public static string CreateProductCategoryImage(HttpPostedFileBase file, string userId)
        {
            string virtualPath = "~/Content/images/ProductCategoryImg/" + userId;
            string FolderPath = HttpContext.Current.Server.MapPath(virtualPath);
            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);
            //string FilePath = Path.Combine(FolderPath, file.FileName);
            file.SaveAs(Path.Combine(FolderPath, file.FileName));
            return virtualPath + "/" + file.FileName + "?w=300&h=212";
        }

        public static string CreateUPhoto(string userId, HttpPostedFileBase file)
        {
            string virtualPath = "~/Content/images/Upload/" + userId;
            string FolderPath = HttpContext.Current.Server.MapPath(virtualPath);
            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);
            //string FilePath = Path.Combine(FolderPath, file.FileName);
            file.SaveAs(Path.Combine(FolderPath, file.FileName));
            return virtualPath + "/" + file.FileName;
        }

        public static string CreateCommentAttachment(string userId, HttpPostedFileBase file)
        {
            string virtualPath = "~/Content/CommentAttachments/" + userId;
            string FolderPath = HttpContext.Current.Server.MapPath(virtualPath);
            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);
            //string FilePath = Path.Combine(FolderPath, file.FileName);
            file.SaveAs(Path.Combine(FolderPath, file.FileName));
            return virtualPath + "/" + file.FileName;
        }

        public static string CreateCommunityAttachment(string userId, HttpPostedFileBase file)
        {
            string virtualPath = "~/Content/CommunityAttachments/" + userId;
            string FolderPath = HttpContext.Current.Server.MapPath(virtualPath);
            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);
            //string FilePath = Path.Combine(FolderPath, file.FileName);
            file.SaveAs(Path.Combine(FolderPath, file.FileName));
            return virtualPath + "/" + file.FileName;
        }

        public static string DefaultImage()
        {
            return "../Content/images/no-image.svg";
        }

        public static string DefaultImg()
        {
            return "~/Content/images/no-image.svg";
        }

        public static string DefaultAvatar()
        {
            return "~/Content/images/default-avatar.png";
        }

        public static void DeleteFileLocal(string FilePath)
        {
            //if (!FilePath.Equals(DefaultImg()))
            //{
            //    FilePath = HttpContext.Current.Server.MapPath(FilePath);
            //    if (File.Exists(FilePath))
            //    {
            //        File.Delete(FilePath);
            //    }
            //}
            var paths = FilePath.Split('?');
            FilePath = HttpContext.Current.Server.MapPath(paths[0]);
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }

        public static string isNavMenuActive(this HtmlHelper html, string actionText, string controllerText)
        {
            var routeData = html.ViewContext.RouteData;
            var routeAction = (string)routeData.Values["action"];
            var routeController = (string)routeData.Values["controller"];
            var returnActive = routeAction == actionText && routeController == controllerText;
            return returnActive ? "active" : "";
        }
    }
}