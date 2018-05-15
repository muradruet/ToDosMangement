using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Repository.Interfaces
{
    public interface IEmailService
    {
        bool sendEmail(string from, string to, string body);
    }
}