using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjTest.Models;
using Project_IPET.Models;
using Project_IPET.Models.EF;
using Project_IPET.Services;
using Project_IPET.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project_IPET.Controllers
{
    public class Empty_SigninController : Controller
    {
        private readonly MyProjectContext _context;
        private readonly IEmailSenderService _emailSender;

        public Empty_SigninController(MyProjectContext context, IEmailSenderService emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult chkId(CLoginViewModel vModel)
        {
            Member customer = _context.Members.FirstOrDefault(m => m.UserId == vModel.txtAccount);
            string name = customer.Name;
            bool banned = customer.Banned;

            if (!banned) {
                if (customer != null && customer.Password.Equals(vModel.txtPassword))
                {
                    string json = JsonSerializer.Serialize(customer);
                    HttpContext.Session.SetString(CDictionary.SK_LOGINED_USER, json);
                    HttpContext.Session.SetString(CDictionary.SK_LOGINED_NAME, name);
                    return Content("true");
                }
                return Content("false");
            }
            return Content("null");
        }

        [HttpPost]
        public IActionResult chkRoId()
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            if (!string.IsNullOrEmpty(json))
            {
                int RoleId = (new CMembersFactory(_context)).getRoId(json);
                return RoleId == 1 ? RedirectToAction("Index", "Front_Home") : RedirectToAction("Index", "Back_Home");
            }
            return RedirectToAction("Index", "Empty_Signin");
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        public IActionResult chkEmail(CFrontMembersViewModel vModel)
        {
            var datas = _context.Members.Any(m => m.Email == vModel.Email);
            if (!string.IsNullOrEmpty(vModel.Email))
            {
                if (datas)
                    return Content("true");
                return Content("false");
            }
            return Content("null");
        }

        [HttpPost]
        public async Task mailtoGetCode(CFrontMembersViewModel vModel)
        {
            var datas = _context.Members.FirstOrDefault(m => m.Email == vModel.Email);
            var email = datas.Email;
            var id = datas.MemberId;
            string json = GetCode(8);
            HttpContext.Session.SetString(CDictionary.SK_VERIFLCATION_CODE, json);
            HttpContext.Session.SetInt32(CDictionary.SK_VERIFLCATION_ID, id);

            await _emailSender.SendEmailAsync(
                email,
                "重設密碼認證信件( IPET )",
                $"<h2>認證代碼為 : <b>{json}</b></h2>");
        }

        public string GetCode(int length)
        {
            const string BASECODE = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random rndNum = new Random((int)DateTime.Now.Ticks);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                int Num = rndNum.Next(BASECODE.Length);
                builder.Append(BASECODE[Num]);
            }
            return builder.ToString();
        }

        public IActionResult chkCode(string Code)
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_VERIFLCATION_CODE);
            if (json != null)
            {
                if (json == Code)
                {
                    return Content("true");
                }
                return Content("false");
            }
            return Content("null");
        }

        public IActionResult ResetPwd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ResetPwd(CEmptySignupViewModel vModel)
        {
            var memberId = HttpContext.Session.GetInt32(CDictionary.SK_VERIFLCATION_ID);
            var member = _context.Members.FirstOrDefault(m => m.MemberId == memberId);

            if (member != null)
                member.Password = vModel.NewPwd;

            _context.SaveChanges();
            return RedirectToAction("Index", "Front_Myaccount");
        }

        public IActionResult chkFlag(CEmptySignupViewModel vModel)
        {
            if (vModel.FlagPassword)
            {
                return Content("true");
            }
            return Content("false");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
