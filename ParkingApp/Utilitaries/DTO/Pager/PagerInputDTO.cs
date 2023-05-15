// Libraries
using System.ComponentModel.DataAnnotations;

namespace Utilitaries.DTO.Pager
{
    /// <summary>
    /// Paget input DTO
    /// </summary>
    [Serializable]
    public class PagerInputDTO
    {
        // Vars

        /// <summary>
        /// Page index
        /// </summary>
        [Required]
        [Range(1,255, ErrorMessage = "The page index must be between 1 and 255")]
        public byte PageIndex { get; set; }

        /// <summary>
        /// Items count
        /// </summary>
        [Required]
        [Range(1, 255, ErrorMessage = "The items count must be between 1 and 255")]
        public byte ItemsCount { get; set; }

        /// <summary>
        /// Vehicles entry date
        /// </summary>
        [Required]
        public DateTime Entry { get; set; }

        /// <summary>
        /// Vehicles departure date
        /// </summary>
        [Required]
        public DateTime Departure { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public PagerInputDTO()
        {
            
        }
    }
}
