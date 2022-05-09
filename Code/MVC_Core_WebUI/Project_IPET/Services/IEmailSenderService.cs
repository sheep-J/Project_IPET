using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Project_IPET.Services
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendEmailWithAttachmentsAsync(string email, string subject, string message, List<Attachment> attachments);
    }
}
