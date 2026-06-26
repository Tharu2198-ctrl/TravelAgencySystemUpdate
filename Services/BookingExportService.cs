using System.IO;
using System.Text.Json;
using TravelAgencySystem.Interfaces;
using TravelAgencySystem.Models;
namespace TravelAgencySystem.Services
{
    public class BookingExportService : IExportable
    {
        private readonly Booking _booking;

        public BookingExportService(Booking booking)
        {
            _booking = booking;
        }

        public void Export()
        {
            string json =
                JsonSerializer.Serialize(
                    _booking,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });

            File.WriteAllText(
                "booking.json",
                json);
        }
    }
}
