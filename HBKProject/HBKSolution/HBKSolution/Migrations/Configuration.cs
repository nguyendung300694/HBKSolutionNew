namespace HBKSolution.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Helpers;

    internal sealed class Configuration : DbMigrationsConfiguration<HBKSolution.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }


        protected override void Seed(HBKSolution.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            Assembly assembly = Assembly.GetExecutingAssembly();

            #region Admin
            var admin = context.Users.SingleOrDefault(u => u.Email.Equals("worknit@gmail.com"));
            if (admin == null)
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                ApplicationUser UserAdmin = new ApplicationUser
                {
                    UserName = "nguyendung300694@gmail.com",
                    Email = "nguyendung300694@gmail.com",
                    IsAdmin = true
                    //Address = "9F 910-3D-ho, Indeogwon IT Valley, 40, Imi-ro, Uiwang-si, Gyeonggi-do",
                    //Country = "Korea"
                };
                userManager.Create(UserAdmin, "123456a@");
            }

            #endregion

        }
    }
}
