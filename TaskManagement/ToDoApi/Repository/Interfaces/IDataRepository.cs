﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.DBModel;

namespace WebApi.Repository.Interfaces
{
    public interface IDataRepository
    {
        List<DboUsers> GetAllUsers();
        List<DboTasks> GetAllTasks();
        DboUsers CreateNewUser(DboUsers user);
        DboTasks CreateNewTask(DboTasks newTask, List<DboUsers> assignedUsers);
        bool DeleteTask(int id);
        DboTasks GetTask(int taskId);
        DboTasks UpdateTask(DboTasks updateTask, List<DboUsers> assignedUsers);
        
    }
}