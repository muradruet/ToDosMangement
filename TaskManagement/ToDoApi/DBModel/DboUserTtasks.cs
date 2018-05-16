using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.DBModel
{

   // [Table("userstasks")]
    public class DboUserTtasks
    {

        [Key]
        public int taskId { get; set; }
        public int userId { get; set; }
    }

}