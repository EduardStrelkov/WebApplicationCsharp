using Mvc.Mailer;
using ReAgent.Services.API;
using System;
using System.Collections.Generic;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;

namespace ReAgent.Services.Impl
{
    public class Mailer : MailerBase, IMailer
    {
        public string FromDisplayName { get; set; }
        public Mailer()
        {
            MasterName = "_Layout";
        }

        public virtual MvcMailMessage SendEmailOrderChange(string emailTo, int orderId, string orderStatus)
        {
            ViewData.Model = new Tuple<int, string>(orderId, orderStatus);

            SmtpSection section = (SmtpSection)System.Configuration.ConfigurationManager.GetSection("system.net/mailSettings/smtp");

            MvcMailMessage mailMessage = this.Populate(x =>
            {
                x.ViewName = "EmailOrderChange";
                x.From = new MailAddress(section.From, this.FromDisplayName);
                x.To.Add(emailTo);
                x.Subject = String.Concat(this.FromDisplayName, " - статус заказа изменен");
                x.BodyEncoding = Encoding.UTF8;
                x.SubjectEncoding = Encoding.UTF8;
                x.BodyTransferEncoding = System.Net.Mime.TransferEncoding.Base64;
            });

            Dictionary<string, string> resources = this.CreateMailResources(mailMessage, "EmailOrderChange");
            this.PopulateBody(mailMessage, "EmailOrderChange", resources);

            Mailer.AdjustMessageEncoding(mailMessage);

            return mailMessage;
        }

        public virtual MvcMailMessage SendEmailVerification(string emailTo, Guid token)
        {
            ViewData.Model = token.ToString();

            SmtpSection section = (SmtpSection)System.Configuration.ConfigurationManager.GetSection("system.net/mailSettings/smtp");

            MvcMailMessage mailMessage = this.Populate(x =>
            {
                x.ViewName = "EmailVerification";
                x.From = new MailAddress(section.From, this.FromDisplayName);
                x.To.Add(emailTo);
                x.Subject = String.Concat(this.FromDisplayName, " - подтверждение email");
                x.BodyEncoding = Encoding.UTF8;
                x.SubjectEncoding = Encoding.UTF8;
                x.BodyTransferEncoding = System.Net.Mime.TransferEncoding.Base64;
            });

            Dictionary<string, string> resources = this.CreateMailResources(mailMessage, "EmailVerification");
            this.PopulateBody(mailMessage, "EmailVerification", resources);

            Mailer.AdjustMessageEncoding(mailMessage);

            return mailMessage;

        }

        private Dictionary<string, string> CreateMailResources(MailMessage mailMessage, string viewName)
        {
            Dictionary<string, string> resources = new Dictionary<string, string>();
            resources["reagent-logo"] = CurrentHttpContext.Server.MapPath("~/Content/images/logo.jpg");

            return resources;
        }

        private static void AdjustMessageEncoding(MvcMailMessage mailMessage)
        {
            int index = -1;
            for (int i = 0; i < mailMessage.AlternateViews.Count; i++)
            {
                if (mailMessage.AlternateViews[i].ContentType.MediaType == "text/html")
                {
                    index = i;
                    break;
                }
            }

            if (index >= 0)
            {
                mailMessage.AlternateViews[index].ContentType = new System.Net.Mime.ContentType("text/html; charset=utf-8");
            }
        }

    }
}