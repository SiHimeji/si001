using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;

namespace cLib
{
    public class MailService
    {
        private readonly Mail _mail;

        public MailService(Mail mail)
        {
            _mail = mail;
        }
        public async Task SendMailAsync(string to, string from, string cc, string subject, string text)
        {
            // MimeMessageクラスのインスタンスを生成
            var message = new MimeKit.MimeMessage();

            // 送信元を追加  
            message.From.Add(new MimeKit.MailboxAddress("<送信元>", from));

            // 宛先を追加  
            message.To.Add(new MimeKit.MailboxAddress("<宛先>", to));

            // Ccを追加  
            message.Cc.Add(new MimeKit.MailboxAddress("<Cc>", cc));

            // 件名を設定
            message.Subject = subject;

            // 本文を設定 
            var textPart = new MimeKit.TextPart(MimeKit.Text.TextFormat.Plain);
            textPart.Text = text;
            message.Body = textPart;

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    // SMTPサーバに接続  
                    await client.ConnectAsync(_mail.SmtpHost, _mail.SmtpPort);
                    Debug.WriteLine("接続完了");

                    // SMTPサーバ認証  
                    await client.AuthenticateAsync(_mail.UserName, _mail.Password);

                    // 送信  
                    await client.SendAsync(message);
                    Debug.WriteLine("送信完了");

                    // 切断  
                    await client.DisconnectAsync(true);

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed send:{ex}");
                }
            }
        }
    }
    public class Mail
    {
        public string SmtpHost { get; set; }

        public int SmtpPort { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
