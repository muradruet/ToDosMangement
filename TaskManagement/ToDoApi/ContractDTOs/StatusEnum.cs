using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApi.ContractDTOs
{
    public enum StatusEnum
    {
        [Description("un-assigned")]
        UnAssigned = 1,
        [Description("In-Progress")]
        InProgress = 2,
        [Description("Completed")]
        Completed = 3,
        [Description("Cancel")]
        Cancel = 4
    }
}