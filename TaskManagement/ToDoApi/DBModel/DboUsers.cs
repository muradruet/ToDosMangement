using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.DBModel
{

    [Table("users")]
    public class DboUsers
    {

        [Key]
        public int userId { get; set; }
        public string name { get; set; }
        public string email { get; set; }

        public virtual ICollection<DboTasks> tasks { get; set; }
    }

}
