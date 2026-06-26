using System.ComponentModel.DataAnnotations;

namespace TravelAgencySystem.Models
{
    public class Package
    {
        [Key]
        public int PackageId { get; set; }

        public string PackageName { get; set; }

        public string Country { get; set; }

        public decimal Price { get; set; }

        public int DurationDays { get; set; }

        public int DestinationId { get; set; }

        public Destination Destination { get; set; }
    }
}