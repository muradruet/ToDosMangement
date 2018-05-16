using System;
using System.Collections.Generic;
using System.Linq;
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

        public DboTasks CreateNewTask(DboTasks newTask, List<DboUsers> assignedUsers)
        {
            foreach (var user in assignedUsers)
            {
                Context.users.Attach(user);
            }
            newTask.users = assignedUsers;
            newTask.createdate = DateTime.Now;

            return  Context.tasks.Add(newTask);
         
            //Context.SaveChanges();
        }

        public DboUsers CreateNewUser(DboUsers user)
        {
            var newUser = Context.users.Add(user);
            return newUser;
        }

        public bool DeleteTask(int id)
        {
            DboTasks task = Context.tasks.Include("users").Where(x => x.taskId == id).FirstOrDefault();


            if (task != null)
            {

                var users = task.users.ToList();
                foreach (DboUsers user in users)
                {
                    //Context.users.Remove(user);
                    task.users.Remove(user);
                }

                Context.tasks.Remove(task);
                
                //DboUserTtasks ut = Context.usersTasks.FirstOrDefault(x => x.taskId == id);
                //Context.usersTasks.Remove(ut);

                return true;
            }
            else
            {
                return false;
            }
        }

        public List<DboTasks> GetAllTasks()
        {
            return Context.tasks.Include("users").ToList();
            
        }

        public List<DboUsers> GetAllUsers()
        {
            return Context.users.ToList();
        }

        public DboTasks GetTask(int taskId)
        {
           return Context.tasks.Include("users").FirstOrDefault(x => x.taskId == taskId);
        }

        public DboTasks UpdateTask(DboTasks updateTask, List<DboUsers> assignedUsers)
        {
            foreach (var user in assignedUsers)
            {
                Context.users.Attach(user);
            }
            updateTask.users = assignedUsers;

            DboTasks task = Context.tasks.First(x => x.taskId == updateTask.taskId);
            task.name = updateTask.name;
            task.description = updateTask.description;
            task.statusId = updateTask.statusId;
            task.duedate = updateTask.duedate;
            task.users = updateTask.users;
            task.updatetime = DateTime.Now;

            return task;

        }
    }
}