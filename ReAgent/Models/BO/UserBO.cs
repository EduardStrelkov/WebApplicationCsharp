using ReAgent.Models.Enums;
using System;

namespace ReAgent.Models.BO
{
    public class UserBO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Guid? VerificationToken { get; set; }
        public DateTime RegistrationDate { get; set; }
        public UserRole Role { get; set; }
    }
}