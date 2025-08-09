using System;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using Outlook = Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Interop.Outlook;
using System.IO;
using Ionic.Zip;
using Ionic.Zlib;

namespace OlPassWdGen
{
    public partial class Ribbon1
    {
        Outlook.MailItem mailItem = null;
        string PStr = "!#$%&'()[]+-*{};:,.?_0123456789abcdefghijklmnopqrstuvwxyxABCDEFGHIJKLMNOPQRSTUVWXYZ"; // 84 char
        string WorkPath;

        // 乱数で18文字のパスワードを作成する
        private string PassWord() 
        {
            int i;
            string pwd = "";

            Random rd = new Random();
            for (i = 0; i < 18; i++)
                pwd = pwd + PStr[rd.Next(1, 84)];
            return pwd;
        }
        // 作業フォルダを準備する
        private void CreateWorkDir()
        {
            string workdir;
            workdir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            workdir = workdir + "\\OlPassWdGen";
            if (Directory.Exists(workdir) == false)
                Directory.CreateDirectory(workdir);
            WorkPath = workdir;
        }
        // 作業フォルダを作成・取得する (AppData\Roaming\OlPassWdGen\work)
        private string GetWorkPath()
        {
            string workdir;
            workdir = WorkPath + "\\work";
            if (Directory.Exists(workdir) == false)
                Directory.CreateDirectory(workdir);
            return workdir;
        }
        // ZIPファイル名を作成する
        private string GetZipName()
        {
            string fname;
            DateTime dt = DateTime.Now;
            fname = dt.ToString("yyyyMMdd-HHmm") + ".zip";
            return fname;
        }
        // 初期設定
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
            //メールオブジェクト取得
            if (mailItem == null)
            {
                var inspector = base.Context as Microsoft.Office.Interop.Outlook.Inspector;
                mailItem = inspector.CurrentItem as Outlook.MailItem;
            }
            button1.Enabled = (mailItem.Attachments.Count > 0);
            button2.Enabled = (mailItem.Attachments.Count > 0);
            //ファイルが添付された際のイベントを登録
            (mailItem as ItemEvents_10_Event).BeforeAttachmentAdd += Ribbon1_BeforeAttachmentAdd;
            // 作業フォルダを準備する(AppData\Roaming\OlPassWdGen)
            CreateWorkDir();
        }
        // ファイルが添付された際に発生するイベント
        void Ribbon1_BeforeAttachmentAdd(Attachment attachment, ref bool Cancel)
        {
            //ファイルが添付されたらボタンをアクティブにする
            button1.Enabled = (mailItem.Attachments.Count > 0);
            button2.Enabled = (mailItem.Attachments.Count > 0);
        }
        // 後処理 作業フォルダを削除する (AppData\Roaming\OlPassWdGen内のworkフォルダ)
        private void Ribbon1_Close(object sender, EventArgs e)
        {
            string wp = GetWorkPath();
            if (Directory.Exists(wp) == true)
                Directory.Delete(wp, true);
        }
        // 添付ファイルをまとめてZIPファイルに圧縮する
        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            string originalFile, compressFile, displayName, wp;
            int cnt, i;

            //var inspector = base.Context as Microsoft.Office.Interop.Outlook.Inspector;
            //mailItem = inspector.CurrentItem as Outlook.MailItem;
            wp = GetWorkPath() + "\\";
            compressFile = wp + GetZipName();
            cnt = mailItem.Attachments.Count;
            using (ZipFile zip = new ZipFile(Encoding.GetEncoding("Shift-JIS")))
            {
                zip.CompressionLevel = CompressionLevel.BestCompression;
                for (i = cnt-1; i >= 0; i--)
                {
                    Attachment attachFile = mailItem.Attachments[i + 1];
                    originalFile = wp + attachFile.FileName;
                    attachFile.SaveAsFile(originalFile);
                    zip.AddFile(originalFile, "");
                    attachFile.Delete();
                }
                zip.Save(compressFile);
            }
            //ZIPに圧縮したファイルを再添付する
            displayName = compressFile;
            mailItem.Attachments.Add(compressFile, DisplayName: displayName);
        }
        // 添付ファイルをまとめてパスワード付きZIPファイルに圧縮する
        private void button2_Click(object sender, RibbonControlEventArgs e)
        {
            string originalFile, compressFile, displayName, wp, pw;
            int cnt, i;

            wp = GetWorkPath() + "\\";
            compressFile = wp + GetZipName();
            cnt = mailItem.Attachments.Count;
            pw = PassWord(); 
            using (ZipFile zip = new ZipFile(Encoding.GetEncoding("Shift-JIS")))
            {
                zip.CompressionLevel = CompressionLevel.BestCompression;
                zip.Password = pw;
                //AES 256ビット暗号化(作成されたZIPファイルはエクスプローラで開けない) 
                //zip.Encryption = Ionic.Zip.EncryptionAlgorithm.WinZipAes256;
                for (i = cnt-1; i >= 0; i--)
                {
                    Attachment attachFile = mailItem.Attachments[i + 1];
                    originalFile = wp + attachFile.FileName;
                    attachFile.SaveAsFile(originalFile);
                    zip.AddFile(originalFile, "");
                    attachFile.Delete();
                }
                zip.Save(compressFile);
            }
            //ZIPに圧縮したファイルを再添付する
            displayName = Path.GetFileName(compressFile);
            mailItem.Attachments.Add(compressFile, DisplayName: displayName);
            //元のメールにメッセージを追加する
            mailItem.BodyFormat = Outlook.OlBodyFormat.olFormatPlain;
            mailItem.Body = "***************************************************\r\n"
                           +"このメールの添付ファイルはセキュリティ強化のために、\r\n"
                           +"自動暗号化されています。\r\n"
                           +"ご不便をおかけしますがご理解をお願いいたします。\r\n"
                           +"尚、パスワードは別メールでお知らせします。\r\n"
                           +"***************************************************\r\n\r\n"
                           + mailItem.Body;

            //別インスタンスにコピーを作成する
            Outlook.MailItem copyMail = mailItem.Copy();
            //プレーンテキスト形式
            copyMail.BodyFormat = Outlook.OlBodyFormat.olFormatPlain;
            copyMail.Subject = mailItem.Subject + " [パスワード通知]";
            //本文
            copyMail.Body = "先ほど送信したメールの添付ファイル復号パスワードをお知らせ致します。"
                           +"\r\n\r\n[添付ファイル名]\r\n" + displayName
                           +"\r\n\r\n[パスワード]\r\n" + pw
                           +"\r\n\r\n"
                           +"お手数をおかけし申し訳ございませんがよろしくお願いいたします。\r\n";
            //コピーしたメール(パスワード通知)の添付ファイルを削除する
            cnt = copyMail.Attachments.Count;
            if (cnt > 0)
                for (i = cnt-1; i >=0; i--)
                {
                    Attachment attachFile = copyMail.Attachments[i+1];
                    attachFile.Delete();
                }
            //作成したパスワード通知メールを表示する
            copyMail.Display();
        }
    }
}
