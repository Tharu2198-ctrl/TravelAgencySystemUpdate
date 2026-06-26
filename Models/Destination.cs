namespace TravelAgencySystem.Models
{
    public class Destination
    {
        public int DestinationId { get; set; }
        //public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}