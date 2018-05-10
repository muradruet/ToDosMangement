using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.ContractDTOs;
using WebApi.DBModel;

namespace WebApi.Mapping
{
    public static class DbModelMapping
    {
        public static List<TaskDTO> ToTaskDTOs(this List<DboTasks> tasks)
        {
            List<TaskDTO> taskDTOs = new List<TaskDTO>();
            if (tasks != null)
            {
                taskDTOs.AddRange(tasks.Select(t => t.ToTaskDTO()));
            }

            return taskDTOs;
        }

        public static TaskDTO ToTaskDTO(this DboTasks task)
        {
            TaskDTO taskDTO = new TaskDTO();
            if (task != null)
            {
                taskDTO.TaskId = task.taskId;
                taskDTO.Name = task.name;
                taskDTO.Description = task.description;
                taskDTO.Status = (StatusEnum)task.statusId;
                taskDTO.DueDate = task.duedate;
                taskDTO.AssignedUsersId = task.users != null ? task.users.Select(x => x.userId).ToList() : null;
            }

            return taskDTO;
        }

        public static List<UserDTO> ToUserDTOs(this List<DboUsers> users)
        {
            List<UserDTO> userDTOs = new List<UserDTO>();
            if (users != null)
            {
                userDTOs.AddRange(users.Select(t => t.ToUserDTO()));
            }

            return userDTOs;
        }

        public static UserDTO ToUserDTO(this DboUsers user)
        {
            UserDTO userDTO = new UserDTO();
            if (user != null)
            {
                userDTO.UserId = user.userId;
                userDTO.UserName = user.name;
                userDTO.UserEmail = user.email;
            }

            return userDTO;
        }

        public static DboUsers ToDboUser(this UserDTO user)
        {
            DboUsers dboUser = new DboUsers();
            if (user != null)
            {
                dboUser.name = user.UserName;
                dboUser.email = user.UserEmail;
            }

            return dboUser;
        }

        
    }
}