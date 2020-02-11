namespace AutomationPractice.Models
{
    using Enums;

    public class ContactUser
    {
        public ContactUser(ContactSubject subject, string email, string message)
        {
            this.Subject = subject;
            this.Email = email;
            this.Message = message;
        }

        public ContactSubject Subject { get; set; }

        public string Email { get; set; }

        public string OrderReference { get; set; }

        public string FileAttach { get; set; }

        public string Message { get; set; }
    }
}