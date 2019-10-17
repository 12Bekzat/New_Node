using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Shop.Services
{
  public class Register
  {
    private const int MAX_ATTEMPT = 10;
    private static bool isRegister { get; set; } = false;
    public static void ToComeIn(string phoneNumber = null, string email = null)
    {
      if (phoneNumber != null)
      {
        // отправить смс-код 
        int sendCode = SendMessageCode();

        int attempt = 0;
        while (true)
        {
          if (attempt == MAX_ATTEMPT)
          {
            Console.WriteLine("Вы написали код 10 раз!\nВам вышлен новый код!\nНажмите Enter что бы подтвердить отправку...");
            Console.ReadLine();
            sendCode = SendMessageCode();
          }
          string code = Console.ReadLine();
          if (string.Equals(code, sendCode))
          {
            // вход в магазин

          }
        }
      }
      else if (email != null)
      {
        // отрправить email-код
        int sendCode = SendEmailCode();

        int attempt = 0;
        while (true)
        {
          if (attempt == MAX_ATTEMPT)
          {
            Console.WriteLine("Вы написали код 10 раз!\nВам вышлен новый код!\nНажмите Enter что бы подтвердить отправку...");
            Console.ReadLine();
            sendCode = SendEmailCode();
          }
          string code = Console.ReadLine();
          if (string.Equals(code, sendCode))
          {
            // вход в магазин

          }
        }
      }
    }

    public static void ToRegister(User user, string phoneNumber = null, string email = null)
    {
      // to registr

      isRegister = true;
    }

    private static int SendMessageCode()
    {
      const string accountSid = "AC72eab5c9bc0bd97ed11b1bbb38666350";
      const string authToken = "56afd5e30952054631405de6e68b55a9";

      TwilioClient.Init(accountSid, authToken);

      int sendCode = new Random().Next(100_000, 999_999);

      var message = MessageResource.Create(
          body: $"Добро пожаловать!\n" +
          $"Ваш код:  {sendCode}",
          from: new Twilio.Types.PhoneNumber("+13343263032"),
          to: new Twilio.Types.PhoneNumber("+77774213007")
      );

      return sendCode;
    }

    private static int SendEmailCode()
    {
      MailMessage mail = new MailMessage();
      SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

      int code = new Random().Next(100_000, 999_999);

      mail.From = new MailAddress("tnik8080@gmail.com");
      mail.To.Add("golumvicerlid35@gmail.com");
      mail.Subject = "Код";
      mail.Body = $"Здраствуйте вот ваш код: {code}";
      SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
      SmtpServer.UseDefaultCredentials = false;
      SmtpServer.Port = 587;
      SmtpServer.Credentials = new System.Net.NetworkCredential("tnik8080@gmail.com", "DD123123");
      SmtpServer.EnableSsl = true;

      SmtpServer.Send(mail);

      return code;
    }
  }
}
