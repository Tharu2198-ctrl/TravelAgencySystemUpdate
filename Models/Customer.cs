namespace TravelAgencySystem.Models
{
    public class Customer : Person
    {
     //   public int CustomerId { get; set; }
     //   public required string FullName { get; set; }
     //   public required string Email { get; set; }
     //   public required string Password { get; set; }
     //   public required string Phone { get; set; }

        public override string GetRole()
        {
            return "Customer";
        }
    }
}