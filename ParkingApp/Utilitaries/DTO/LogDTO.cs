// Libraries
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Utilitaries.DTO
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class LogDTO
    {
        // Vars

        /// <summary>
        /// Parking log ID
        /// </summary>
        [StringLength(36, MinimumLength = 36, ErrorMessage = "Parking log ID must have 36 characters")]
        public string? Id { get; set; }

        /// <summary>
        /// Vehicle entry date and time
        /// </summary>
        [Required(ErrorMessage = "Vehicle entry date and time is required")]
        public DateTime? Entry { get; set; }

        /// <summary>
        /// Vehicle departure date and time
        /// </summary>
        public DateTime? Departure { get; set; }

        /// <summary>
        /// Parking total price
        /// </summary>
        [JsonIgnore]
        public float? Price { get; set; }

        /// <summary>
        /// Bull discount number
        /// </summary>
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Bill discount number must have 8 numbers")]
        [RegularExpression("[0-9]*", ErrorMessage = "Bill discount number must have only numbers")]
        public string? BillDiscountNumber { get; set; }

        /// <summary>
        /// Associated vehicle
        /// </summary>
        public VehicleDTO? Vehicle { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public LogDTO()
        {
            
        }
    }
}
