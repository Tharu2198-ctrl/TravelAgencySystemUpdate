using System.ComponentModel.DataAnnotations;

namespace TravelAgencySystem.Models
{
    public abstract class Person
    {
        [Key]
        public int UserId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; } = string.Empty;

        public abstract string GetRole();

    }
}
