namespace Utilitaries.Exceptions
{
    /// <summary>
    /// Custom not found exception
    /// </summary>
    public class CustomNotFoundException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CustomNotFoundException(string message) : base(message)
        {

        }
    }
}
