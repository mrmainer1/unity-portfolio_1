using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Network.Email
{
    public class EEEmailSender : MonoBehaviour
    {
        [SerializeField] private string login, password, addressSender, nameSender;
    
        public void Send(EmailData emailData)
        {
            var from = new MailAddress(addressSender, nameSender);
            var to = new MailAddress(emailData.AddressReceiver);
            var m = new MailMessage(from, to)
            {
                Subject = emailData.Subject, 
                Body = emailData.Body,
                IsBodyHtml = emailData.IsBodyHtml
            };
            var smtp = new SmtpClient(emailData.AddressSMTP, emailData.Port)
            {
                Credentials = new NetworkCredential(login, password), 
                EnableSsl = emailData.SSL
            };
            smtp.Send(m);
        }
    
        public class EmailData
        {
            public string AddressReceiver;
            public string Subject;
            public string Body;
            public bool IsBodyHtml;
            public string AddressSMTP;
            public int Port;
            public bool SSL = true;
            public List<Attachment> Attachments = new List<Attachment>();
        }
    }
}