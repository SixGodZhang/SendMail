using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace SendMail
{
    internal class Program
    {
        private static string sender = "";
        private static string receiver = "";
        private static string license = "";
        private static string subject = "";
        private static string body = "";

        private static void Main(string[] args)
        {
            ReadInitSettingFromFile();

            //实例化一个发送邮件类。
            MailMessage mailMessage = new MailMessage();
            //发件人邮箱地址，方法重载不同，可以根据需求自行选择。
            mailMessage.From = new MailAddress(sender);
            //收件人邮箱地址。
            mailMessage.To.Add(new MailAddress(receiver));
            //邮件标题。
            mailMessage.Subject = subject;
            //邮件内容。
            mailMessage.Body = body;

            //实例化一个SmtpClient类。
            SmtpClient client = new SmtpClient();
            //在这里我使用的是qq邮箱，所以是smtp.qq.com，如果你使用的是126邮箱，那么就是smtp.126.com。
            client.Host = "smtp.qq.com";
            //使用安全加密连接。
            client.EnableSsl = true;
            //不和请求一块发送。
            client.UseDefaultCredentials = false;
            //验证发件人身份(发件人的邮箱，邮箱里的生成授权码);
            client.Credentials = new NetworkCredential(sender, license);
            //发送
            for (int i = 0; i < 10; i++)
            {
                client.Send(mailMessage);
            }
            
            //Console.WriteLine("发送成功");
        }

        private static void ReadInitSettingFromFile()
        {
            string path = System.Environment.CurrentDirectory + "/setting.ini";
            //Console.WriteLine(">>>>>"+ path);

            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("sender"))
                {
                    sender = line.Split(':')[1];
                }else if (line.Contains("receiver"))
                {
                    receiver = line.Split(':')[1];
                }else if (line.Contains("license"))
                {
                    license = line.Split(':')[1];
                }
                else if (line.Contains("subject"))
                {
                    subject = line.Split(':')[1];
                }
                else if (line.Contains("body"))
                {
                    body = line.Split(':')[1];
                }
            }
        }


    }
}