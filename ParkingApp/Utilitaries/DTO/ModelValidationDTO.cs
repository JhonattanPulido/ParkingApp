namespace Utilitaries.DTO
{
    /// <summary>
    /// Error DTO
    /// </summary>
    [Serializable]
    public class ModelValidationDTO
    {
        // Vars

        /// <summary>
        /// Model validation errors
        /// </summary>
        public object Errors { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="errors">Model validation errors</param>
        /// <param name="message">Error message</param>
        public ModelValidationDTO(object errors, string message = "There was an error with the model validation")
        {
            Errors = errors;
            Message = message;
        }
    }
}
