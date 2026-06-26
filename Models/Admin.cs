namespace TravelAgencySystem.Models
{
    public class Admin : Person
    {
        public override string GetRole()
        {
            return "Admin";
        }
    }
}
