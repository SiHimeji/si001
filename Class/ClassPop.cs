using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Windows.Forms;
namespace cLib
{
    public class ClassPop
    {
        public DataTable GetPOP()
        {
            DateTime tm= DateTime.Now.Date;
            string  tmg;
            string yyyy=tm.ToString("yyyy");
            DataTable dt = new DataTable();
            dt.Columns.Add("time", typeof(string));     // -0
            dt.Columns.Add("mail", typeof(string));     // -1


            string msg;
            using (var client = new ImapClient())
            {
                msg = "";
                try
                {
                    var host = "sv1114.xserver.jp";        // サーバーのホスト名
                    var port = 993;                        // サーバーのポート (通常993)
                    var user = "info@harimarche.com";      // メールアドレス
                    var password = "0909pc201";            // パスワード
                                           // サーバーに接続・認証
                    client.Connect(host, port, true);
                    client.Authenticate(user, password);
                    msg += "サーバーに接続しました。\r\n";

                    // 受信トレイを開く
                    var inbox = client.Inbox;
                    inbox.Open(FolderAccess.ReadOnly);
                    // inbox.Count.ToString();
                    //bar.Maximum=  inbox.Count;
                    //bar.Minimum = 0;
                    //bar.Value = 0;
                    string bomg=string.Empty;
                    string jyusinDay = string.Empty;
                    string body = string.Empty;
                    string shop = string.Empty;

                    // メールを取得
                    Console.WriteLine("メールを取得");
                    for (int i = 0; i<inbox.Count; i++)
                    {
                        var message = inbox.GetMessage(i);

                        if (message.Subject.Contains("Undelivered") && message.Subject.Contains("Sender"))     // == "Undelivered Mail Returned to Sender")
                        {
                            jyusinDay = message.Date.ToString("yyyy/MM/dd HH:mm:ss");
                            body = message.TextBody.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
                            
                            DataRow row = dt.NewRow();
                            row["time"] = jyusinDay;
                            row["mail"] = mailAdres(body);
                            dt.Rows.Add(row);
                        }
                    }
                    // サーバーから切断
                    dt.AcceptChanges();
                    client.Disconnect(true);
                    msg += $"サーバーから切断しました。\r\n";
                }
                catch (Exception ex)
                {
                    msg += $"エラーが発生しました: {ex.Message}\r\n";
                }
                Console.WriteLine(msg);

            }
            return dt;
        }
        //
        private string mailAdres(string body)
        {
            string ml=string.Empty;
            ml = body.Substring(body.IndexOf("<")+1, body.IndexOf(">")- body.IndexOf("<")-1);
            return ml;
        }
        //
    
        public void SendPop()
        {

            SendMail("info@si-himeji.co.jp", "E5fAH2jn");

        }

        static async private void SendMail(string userName, string password)
        {
            // MimeMessageを作り、宛先やタイトルなどを設定する
            var message = new MimeKit.MimeMessage();
            message.From.Add(new MimeKit.MailboxAddress("送信元", "info@si-himeji.co.jp"));
            message.To.Add(new MimeKit.MailboxAddress("送信先", "si.himeji@gmail.com"));
            // message.Cc.Add(……省略……);
            // message.Bcc.Add(……省略……);

            message.Subject = "赤松自工カメラ停止";
            // 本文を作る
            var textPart = new MimeKit.TextPart(MimeKit.Text.TextFormat.Plain);
            textPart.Text = @"MailKit を使ってメールを送ってみるテストです。";

            // MimeMessageを完成させる
            message.Body = textPart;

            // SMTPサーバに接続してメールを送信する
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    await  client.ConnectAsync("sv329.xserver.jp", 587);

                    await  client.AuthenticateAsync(userName, password);

                    await  client.SendAsync(message);

                    await  client.DisconnectAsync(true);
                }
                catch (Exception ex)
                {

                }
            }
        }
        ///
    }
}
