using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.DBContex;
using WebApi.DBModel;
using WebApi.Repository.Interfaces;

namespace WebApi.Repository
{
    public class DataRepository : IDataRepository
    {
        protected readonly ToDoDBContext Context;

        public DataRepository(ToDoDBContext context)
        {
            this.Context = context;
        }

        public DboUsers CreateNewUser(DboUsers user)
        {
            var newUser = Context.users.Add(user);
            return newUser;
        }

        public List<DboTasks> GetAllTasks()
        {
            return Context.tasks.Include("users").ToList();
            
        }

        public List<DboUsers> GetAllUsers()
        {
            return Context.users.ToList();
        }
    }
}