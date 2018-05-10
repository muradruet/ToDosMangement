using System.Collections.Generic;
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
        //[Authorize]
        public IHttpActionResult GetTasks()
        {
            using (IUnitOfWork dbTask = new UnitOfWork())
            {
                List<DboTasks> data = dbTask.DataRepo.GetAllTasks();

                return Ok(data.ToTaskDTOs());
            }
        }

        [HttpGet]
        [Route("users")]
        //[Authorize]
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