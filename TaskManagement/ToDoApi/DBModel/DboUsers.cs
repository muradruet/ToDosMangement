using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.ContractDTOs;

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

        public bool Validate(List<Problem> validatioErrors)
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(this.name))
            {
                isValid = false;
                validatioErrors.Add(new Problem { Field = "User Name", Message = "User name can not be null or empty" });
            }

            if (string.IsNullOrEmpty(this.name))
            {
                isValid = false;
                validatioErrors.Add(new Problem { Field = "User Email", Message = "User email can not be null or empty" });
            }
            return isValid;
        }
    }

}
