using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgencySystem.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public int NumberOfPeople { get; set; }

        public int UserId { get; set; }
        public int PackageId { get; set; }
        
        [ForeignKey("UserId")]
        public Customer Customer { get; set; }
        public Package Package { get; set; }
    }
}