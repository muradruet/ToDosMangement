using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.ContractDTOs
{
    public class UserDTO
    {
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}