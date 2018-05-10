using System.Collections.Generic;
using System.Linq;
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
        //[Authorize]
        public IHttpActionResult CreatTask(TaskDTO taskToCreate)
        {
            using (IUnitOfWork dbTask = new UnitOfWork())
            {
                DboTasks newTask = taskToCreate.ToDboTask();
                List<DboUsers> users = dbTask.DataRepo.GetAllUsers();
                List<DboUsers> assignedUsers = taskToCreate.AssignedUsersId!= null ?
                    users.Where(u => taskToCreate.AssignedUsersId.Contains(u.userId)).ToList()
                    : null;
                newTask = dbTask.DataRepo.CreateNewTask(newTask, assignedUsers);

                dbTask.SaveChanges();

                return Ok(newTask.ToTaskDTO());
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
                dbTask.SaveChanges();

                return Ok(newUser.ToUserDTO());
            }
        }
    }
}