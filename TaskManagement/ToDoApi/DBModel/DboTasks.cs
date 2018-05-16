using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.ContractDTOs;

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

        public bool Validate(List<Problem> validatioErrors)
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(this.name))
            {
                isValid = false;
                validatioErrors.Add(new Problem { Field = "User Name", Message = "Task name can not be null or empty" });
            }

            if (string.IsNullOrEmpty(this.name))
            {
                isValid = false;
                validatioErrors.Add(new Problem { Field = "User Description", Message = "Task description can not be null or empty" });
            }

            if (duedate==null)
            {
                isValid = false;
                validatioErrors.Add(new Problem { Field = "DueDate", Message = "DueDate can not be null or empty" });
            }

            if (duedate.HasValue && duedate.Value.Date < DateTime.Now.Date)
            {
                isValid = false;
                validatioErrors.Add(new Problem { Field = "DueDate", Message = "DueDate can not past date" });
            }

            if (statusId.HasValue)
            {
                if ( statusId.Value == (int)StatusEnum.UnAssigned && users.Count > 0)
                {
                    isValid = false;
                    validatioErrors.Add(new Problem { Field = "statusId", Message = "Un-Assigned can not have any users assigned to it" });
                }

                if (statusId.Value != (int)StatusEnum.UnAssigned && users.Count < 1)
                {
                    isValid = false;
                    validatioErrors.Add(new Problem { Field = "Assigned", Message = "Assigned to users can not be empty" });
                }

                if (statusId.Value == (int)StatusEnum.Cancel)
                {
                    isValid = false;
                    validatioErrors.Add(new Problem { Field = "statusId", Message = "Cancel type task can not be created" });
                }
            }
            return isValid;
        }
    }

}
