using System.Net;
using System.Net.Mail;
using System.Text;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Domain.Services;

public class EmailSettings
    {
        public string MailToAddress = "mir-n-mir@mail.ru";
        public string MailFromAddress = "aridon@example.com";
        public bool UseSsl = true;
        public string Username = "MySmtpUsername";
        public string Password = "MySmtpPassword";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"c:\OnlineShopEmails";
    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private readonly EmailSettings _emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            _emailSettings = settings;
        }

        public void ProcessOrder(Order order, ShippingDetails shippingInfo, CancellationToken cancellationToken)
        {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.EnableSsl = _emailSettings.UseSsl;
                    smtpClient.Host = _emailSettings.ServerName;
                    smtpClient.Port = _emailSettings.ServerPort;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials
                        = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);

                    if (_emailSettings.WriteAsFile)
                    {
                        smtpClient.DeliveryMethod
                            = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                        smtpClient.PickupDirectoryLocation = _emailSettings.FileLocation;
                        smtpClient.EnableSsl = false;
                    }

                    StringBuilder body = new StringBuilder()
                        .AppendLine("Новый заказ обработан")
                        .AppendLine("---")
                        .AppendLine("Товары:");

                    foreach (var line in order.Items)
                    {
                        var subtotal = line.Price * line.Quantity;
                        body.AppendFormat("{0} x {1} (Итого: {2:c}",
                            line.Price, line.Quantity, subtotal);
                    }

                    body.AppendFormat("Общая стоимость: {0:c}", order.GetTotalPrice())
                        .AppendLine("---")
                        .AppendLine("Доставка:")
                        .AppendLine(shippingInfo.Name)
                        .AppendLine(shippingInfo.Adress)
                        .AppendLine(shippingInfo.City)
                        .AppendLine("---");

                    MailMessage mailMessage = new MailMessage(
                        _emailSettings.MailFromAddress,
                        _emailSettings.MailToAddress,
                        "Новый заказ отправлен!",
                        body.ToString());

                    if (_emailSettings.WriteAsFile)
                    {
                        mailMessage.BodyEncoding = Encoding.UTF8;
                    }

                    smtpClient.SendAsync(mailMessage, null);
            }
        }
    }
