// Libraries
using System.ComponentModel.DataAnnotations;

namespace Utilitaries.DTO
{
    /// <summary>
    /// Vehicle type DTO
    /// </summary>
    [Serializable]
    public class VehicleTypeDTO
    {
        // Vars

        /// <summary>
        /// Vehicle type ID
        /// </summary>
        [Required(ErrorMessage = "Vehicle type ID is required")]
        [Range(1,3, ErrorMessage = "Vehicle type ID must be between 1 and 3")]
        public byte? Id { get; set; }

        /// <summary>
        /// Vehicle type name
        /// </summary>
        [MinLength(2, ErrorMessage = "Vehicle type name must be at least 2 characters long")]
        [MaxLength(32, ErrorMessage = "Vehicle type name must be a maximum 32 characters")]
        public string? Name { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public VehicleTypeDTO()
        {
            
        }
    }
}
