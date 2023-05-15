namespace Utilitaries.Exceptions
{
    /// <summary>
    /// Custom bad request exception
    /// </summary>
    public class CustomBadRequestException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        public CustomBadRequestException(string message) : base(message)
        {

        }
    }
}
