using Mvc.Mailer;
using System;

namespace ReAgent.Services.API
{
    public interface IMailer
    {
        MvcMailMessage SendEmailOrderChange(string emailTo, int orderId, string orderStatus);
        MvcMailMessage SendEmailVerification(string emailTo, Guid token);
    }
}
