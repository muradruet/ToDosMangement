using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.DBModel
{

    [Table("tasks")]
    public class DboTasks
    {

        [Key]
        public int taskId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int? statusId { get; set; }
        public DateTime? duedate { get; set; }
        public DateTime? createdate { get; set; }
        public DateTime? updatetime { get; set; }
        public virtual ICollection<DboUsers> users { get; set; }
    }

}
