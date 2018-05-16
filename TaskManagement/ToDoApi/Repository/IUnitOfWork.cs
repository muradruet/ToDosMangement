using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Repository.Interfaces;

namespace WebApi.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IDataRepository DataRepo { get; }
        int SaveChanges();
    }
}