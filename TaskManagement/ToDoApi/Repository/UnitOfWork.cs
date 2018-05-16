using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.DBContex;
using WebApi.Repository.Interfaces;

namespace WebApi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ToDoDBContext _context;

        public UnitOfWork()
        {
            _context = new ToDoDBContext();
            DataRepo = new DataRepository(_context);
        }
        public IDataRepository DataRepo { get; private set; }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}