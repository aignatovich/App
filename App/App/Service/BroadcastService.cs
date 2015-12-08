using App.DAL;
using App.Models;
using App.Properties;
using App.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace App.Service
{
    public class BroadcastService:IBroadcastService
    {
        private IEmployeeDAO employeeDataAccessObject;

        public BroadcastService(IEmployeeDAO employeeDataAccessObject)
        {
            this.employeeDataAccessObject = employeeDataAccessObject;
        }

        public void Broadcast(IEnumerable<Int32> ids, string message)
        {
            ICollection<EmployeeModel> employees = employeeDataAccessObject.GetEmployeesByIds(ids);

            var subject = Settings.Default.subject;
            var from = Settings.Default.from;
            string fromName = Settings.Default.fromName;

            foreach (EmployeeModel employee in employees)
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