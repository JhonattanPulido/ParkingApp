// Libraries
using System.ComponentModel.DataAnnotations;

namespace Utilitaries.DTO
{
    /// <summary>
    /// Vehicle DTO
    /// </summary>
    [Serializable]
    public class VehicleDTO
    {
        // Vars

        /// <summary>
        /// Vehicle number plate
        /// </summary>
        [Required(ErrorMessage = "Vehicle number plate is required")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Vehicle number plate must have 6 characters")]
        [RegularExpression("[A-Z0-9]*", ErrorMessage = "Vehicle number plate must have only uppercase letters and/or numbers")]
        public string? NumberPlate { get; set; }

        /// <summary>
        /// Vehicle type
        /// </summary>
        public VehicleTypeDTO? Type { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public VehicleDTO()
        {
            
        }
    }
}
