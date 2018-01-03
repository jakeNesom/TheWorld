using System;
using System.Diagnostics;

namespace TheWorld.Services
{
    public class DebugMailService : IMailService
    {
        public DebugMailService()
        {

        }

        public void SendMail(string to, string from, string subject, string body)
        {
            Debug.WriteLine($"Sending  Mail: To: {to} From: {from} Subject: {subject}");
        }
    }
}
