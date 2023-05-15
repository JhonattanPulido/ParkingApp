namespace Models
{
    /// <summary>
    /// Vehicle class
    /// </summary>
    [Serializable]
    public class Vehicle
    {
        // Vars

        /// <summary>
        /// Vehicle number plate
        /// </summary>
        public string? NumberPlate { get; set; }

        /// <summary>
        /// Vehicle type
        /// </summary>
        public VehicleType? Type { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Vehicle()
        {

        }
    }
}
