using HBKSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBKSolution.Services
{
    public interface IDataAccessService
    {
        ApplicationDbContext Init();
    }

    public class DataAccessService : IDataAccessService
    {
        private readonly ApplicationDbContext dbContext;
        public ApplicationDbContext Init()
        {
            return dbContext ?? new ApplicationDbContext();
        }
    }
}