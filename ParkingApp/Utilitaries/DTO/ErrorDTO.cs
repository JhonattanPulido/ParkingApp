namespace Utilitaries.DTO
{
    /// <summary>
    /// Error DTO
    /// </summary>
    [Serializable]
    public class ErrorDTO
    {
        // Vars

        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Error message</param>
        public ErrorDTO(string message = "There was an internal server error")
        {
            Message = message;
        }
    }
}
