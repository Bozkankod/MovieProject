using MovieProject.Email.Abstract;
using System.Net;
using System.Net.Mail;

public class EmailService : IEmailService
{
    public void SendEmail(string to, string subject, string body)
    {
        var fromAddress = new MailAddress("movierecommendationbatu@yandex.com");
        var toAddress = new MailAddress(to);
        const string fromPassword = "tywediprwhcqojus";
        var smtp = new SmtpClient
        {
            Host = "smtp.yandex.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        };
        using var message = new MailMessage(fromAddress, toAddress)
        {
            Subject = subject,
            Body = body
        };
        try
        {
            smtp.Send(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }
}