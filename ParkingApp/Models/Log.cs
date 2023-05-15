// Libraries
using System.Text.Json.Serialization;

namespace Models
{
    /// <summary>
    /// Log class
    /// </summary>
    [Serializable]
    public class Log
    {
        // Vars

        /// <summary>
        /// Log ID
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Vehicle entry date and time
        /// </summary>
        public DateTime? Entry { get; set; }

        /// <summary>
        /// Vehicle departure date and time
        /// </summary>
        public DateTime? Departure { get; set; }

        /// <summary>
        /// Parking total price
        /// </summary>
        public float? Price { get; set; }

        /// <summary>
        /// Parking spent time
        /// </summary>
        public string? Time { get; set; }

        /// <summary>
        /// Bill discount number
        /// </summary>
        public string? BillDiscountNumber { get; set; }

        /// <summary>
        /// Associated vehicle
        /// </summary>
        public Vehicle? Vehicle { get; set; }

        /// <summary>
        /// Associated vehicle as JSON
        /// </summary>
        [JsonIgnore]
        public string? VehicleJSON { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Log()
        {
            
        }
    }
}
