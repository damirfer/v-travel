using System.Collections.Generic;

namespace Model
{
    public class Guide
    {
        public int GuideId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Bio { get; set; }
        public string PhotoUrl { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public virtual IList<GuideLanguage> GuideLanguage { get; set; }

    }
}