using System.Collections.Generic;
using System.Net.Mail;
using App.DAL;
using App.Properties;
using App.Service.Interfaces;

namespace App.Service
{
    public class BroadcastService:IBroadcastService
    {
        private IEmployeeDAO employeeDataAccessObject;

        public BroadcastService(IEmployeeDAO employeeDataAccessObject)
        {
            this.employeeDataAccessObject = employeeDataAccessObject;
        }

        public void Broadcast(IEnumerable<int> ids, string message)
        {
            var employees = employeeDataAccessObject.GetEmployeesByIds(ids);
            var subject = Settings.Default.subject;
            var from = Settings.Default.from;
            var fromName = Settings.Default.fromName;

            foreach (var employee in employees)
            {
                using (var client = new SmtpClient())
                {
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.EnableSsl = true;
                    var destination = employee.Email;
                    var mail = new MailMessage(new MailAddress(from, fromName), new MailAddress(destination));
                    mail.Subject = subject;
                    mail.Body = message;
                    mail.IsBodyHtml = true;
                    client.Send(mail);
                }
            }
        }
    }
}