using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_IPET.Models.EF;
using Project_IPET.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Controllers
{
    public class Back_FeedbackController : Controller
    {
        private readonly MyProjectContext _context;
        private readonly IEmailSenderService _emailSender;

        public Back_FeedbackController(MyProjectContext context, IEmailSenderService emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.CustomerContacts.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(int id, string mailaddress, string subject, string replymessage)
        {
            var contact = _context.CustomerContacts.Single(c => c.ContactId==id && c.ContactMail == mailaddress && c.ContactSubject == subject);
            await _emailSender.SendEmailAsync(mailaddress, $"IPET 客服訊息回覆: ( { subject } )", $"{ replymessage }");
            contact.ReplyStatus = true;
            contact.ReplyMessage = replymessage;
            _context.CustomerContacts.Update(contact);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult ContactDetail(int Id)
        {
            var Contact = _context.CustomerContacts.Where(n => n.ContactId == Id).Select(p => new
            {
                detailcontactname = p.ContactName,
                detailcontactmessage = p.ContactMessage,
            });
            return Json(Contact);
        }

    }
}
