using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Project_IPET.Services
{
    public class CSendGmailService
    {



        public void sendGmail()
        {
            MailMessage mail = new MailMessage();
            //前面是發信email後面是顯示的名稱
            mail.From = new MailAddress("XXXX@gmail.com", "IPet寵物店客服系統(自動發送)");

            //收信者email
            mail.To.Add("XXXXXX@gmail.com");

            //設定優先權
            mail.Priority = MailPriority.Normal;

            //標題
            mail.Subject = "IPet寵物店忘記密碼驗證信";

            //內容
            mail.Body = "<h1>親愛的IPet顧客你好,以下為重置密碼驗證碼</h1><br><p>驗證碼為:987987</p>";

            //內容使用html
            mail.IsBodyHtml = true;

            //設定gmail的smtp (這是google的)
            SmtpClient MySmtp = new SmtpClient("smtp.gmail.com", 587);

            //您在gmail的帳號密碼
            MySmtp.Credentials = new System.Net.NetworkCredential("XXXXXX@gmail.com", "********");

            //開啟ssl
            MySmtp.EnableSsl = true;

            //發送郵件
            MySmtp.Send(mail);

            //放掉宣告出來的MySmtp
            MySmtp = null;

            //放掉宣告出來的mail
            mail.Dispose();
        }
    }
}
