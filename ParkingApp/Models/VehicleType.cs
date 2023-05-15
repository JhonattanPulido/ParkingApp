namespace Models
{
    /// <summary>
    /// Vehicle type class
    /// </summary>
    [Serializable]
    public class VehicleType
    {
        // Vars

        /// <summary>
        /// Vehicle type ID
        /// </summary>
        public byte? Id { get; private set; }

        /// <summary>
        /// Vehicle type name
        /// </summary>
        public string? Name { get; private set; }

        /// <summary>
        /// Vehicle type cost
        /// </summary>
        public float? Cost { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public VehicleType()
        {
            
        }
    }
}
