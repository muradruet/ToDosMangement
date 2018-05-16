using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ContractDTOs;
using WebApi.DBModel;
using WebApi.Mapping;
using WebApi.Repository;

namespace WebApi.Controllers
{
    [RoutePrefix("api")]
    public class TaskController : System.Web.Http.ApiController
    {
        [HttpGet]
        [Route("public")]
        public IHttpActionResult Public()
        {
            using (IUnitOfWork dbTask = new UnitOfWork())
            {
                var data = dbTask.DataRepo.GetAllTasks();
            }

            return Ok("This is a public endpoint. Don't need to be authenticated");
        }

        [HttpGet]
        [Route("tasks")]
        [Authorize]
        public IHttpActionResult GetTasks()
        {
            using (IUnitOfWork dbTask = new UnitOfWork())
            {
                List<DboTasks> data = dbTask.DataRepo.GetAllTasks();

                return Ok(data.ToTaskDTOs());
            }
        }

        [HttpPost]
        [Route("tasks")]
        [Authorize]
        public IHttpActionResult CreatTask(TaskDTO taskToCreate)
        {
            using (IUnitOfWork dbTask = new UnitOfWork())
            {
                DboTasks newTask = taskToCreate.ToDboTask();
                List<DboUsers> users = dbTask.DataRepo.GetAllUsers();
                List<DboUsers> assignedUsers = taskToCreate.AssignedUsersId != null ?
                    users.Where(u => taskToCreate.AssignedUsersId.Contains(u.userId)).ToList()
                    : null;
                newTask = dbTask.DataRepo.CreateNewTask(newTask, assignedUsers);

                List<Problem> errors = new List<Problem>();
                if (!newTask.Validate(errors))
                {
                    var response = Request.CreateResponse(HttpStatusCode.BadRequest, errors);
                    return ResponseMessage(response);
                }
                dbTask.SaveChanges();

                return Ok(newTask.ToTaskDTO());
            }
        }

        [HttpPut]
        [Route("tasks/{taskId}")]
        [Authorize]
        public IHttpActionResult updateTask(int taskId, TaskDTO taskToCreate)
        {
            using (IUnitOfWork dbTask = new UnitOfWork())
            {
                DboTasks task = dbTask.DataRepo.GetTask(taskId);
                if (task == null)
                {
                    List<Problem> errors = new List<Problem>{
                        new Problem()
                        {
                            Field = "TaskId",
                            Message = "Invalid Task Id"
                        }
                    };
                    var response = Request.CreateResponse(HttpStatusCode.BadRequest, errors);
                    return ResponseMessage(response);
                }

                DboTasks updateTask = taskToCreate.ToDboTask();
                updateTask.taskId = taskId;
                List<DboUsers> users = dbTask.DataRepo.GetAllUsers();
                List<DboUsers> assignedUsers = taskToCreate.AssignedUsersId != null ?
                    users.Where(u => taskToCreate.AssignedUsersId.Contains(u.userId)).ToList()
                    : null;
                updateTask = dbTask.DataRepo.UpdateTask(updateTask, assignedUsers);

                List<Problem> validatrionErrors = new List<Problem>();
                if (!updateTask.Validate(validatrionErrors))
                {
                    var response = Request.CreateResponse(HttpStatusCode.BadRequest, validatrionErrors);
                    return ResponseMessage(response);
                }
                dbTask.SaveChanges();

                return Ok(updateTask.ToTaskDTO());
            }
        }

        [HttpGet]
        [Route("users")]
        [Authorize]
        public IHttpActionResult GetUsers()
        {
            using (IUnitOfWork dbTask = new UnitOfWork())
            {
                List<DboUsers> data = dbTask.DataRepo.GetAllUsers();

                return Ok(data.ToUserDTOs());
            }
        }

        [HttpPost]
        [Route("users")]
        [Authorize]
        public IHttpActionResult CreatUser(UserDTO userToCreate)
        {
            using (IUnitOfWork dbTask = new UnitOfWork())
            {
                DboUsers newUser = userToCreate.ToDboUser();
                newUser = dbTask.DataRepo.CreateNewUser(newUser);
                List<Problem> errors = new List<Problem>();
                if (!newUser.Validate(errors))
                {
                    var response = Request.CreateResponse(HttpStatusCode.BadRequest, errors);
                    return ResponseMessage(response);
                }

                dbTask.SaveChanges();
                return Ok(newUser.ToUserDTO());
            }
        }

        [HttpGet]
        [Route("status")]
        [Authorize]
        public IHttpActionResult GetStatus()
        {

            var status = Enum.GetValues(typeof(StatusEnum))
                .Cast<StatusEnum>()
                .Select(t => new { Id = (int)t, Description = t.GetDescription() }).ToList();

            return Ok(status);
        }

        [HttpDelete]
        [Route("tasks/{taskId}")]
        [Authorize]
        public IHttpActionResult deletetask(int taskId)
        {
            bool isDeleted = false;
            using (IUnitOfWork dbTask = new UnitOfWork())
            {
                isDeleted = dbTask.DataRepo.DeleteTask(taskId);

                List<Problem> errors = new List<Problem>{
                    new Problem()
                    {
                        Field = "TaskId",
                        Message = "Invalid Task Id"
                    }
                };

                if (!isDeleted)
                {
                    var response = Request.CreateResponse(HttpStatusCode.BadRequest, errors);
                    return ResponseMessage(response);
                }
                else
                {
                    dbTask.SaveChanges();
                    return Ok(isDeleted);
                };
            }
        }
    }
}