using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.ContractDTOs
{
    public class TaskDTO
    {
        public int? TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public StatusEnum? Status { get; set; }
        public DateTime? DueDate { get; set; }
        public List<int> AssignedUsersId { get; set; }
    }
}