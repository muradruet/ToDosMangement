using WebApi.Repository.Interfaces;

namespace WebApi.Repository
{
    public class EmailService : IEmailService
    {
        public bool sendEmail(string from, string to, string body)
        {
            //send email goes here
            return true;
        }
    }
}