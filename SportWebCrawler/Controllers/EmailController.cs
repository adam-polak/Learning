using System.Net;
using System.Net.Mail;

namespace SportWebCrawler.Controllers;

public class EmailController
{
    public readonly string sender;
    private readonly string sender_password;
    public readonly string receiver;
    private static int portNumber;

    public EmailController(string sender, string sender_password, string receiver)
    {
        this.sender = sender;
        this.sender_password = sender_password;
        this.receiver = receiver;
    }

    public void SendEmail()
    {
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(sender);  
        mail.To.Add(receiver);  
        mail.Subject = $"NBA Games on {DateTime.Now.ToShortDateString()}";  
        mail.IsBodyHtml = true;  
        // mail.Attachments.Add(new Attachment("file name"));
        using(SmtpClient smtp = new SmtpClient("smtp.gmail.com", portNumber)) {  
            smtp.Credentials = new NetworkCredential(sender, sender_password);  
            smtp.EnableSsl = true; 
            smtp.Send(mail);  
        }  
    }

}