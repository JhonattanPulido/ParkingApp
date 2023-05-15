namespace Utilitaries.DTO.Pager
{
    /// <summary>
    /// Pager output DTO
    /// </summary>
    [Serializable]
    public class PagerOutputDTO<T>
    {
        // Vars

        /// <summary>
        /// Items list
        /// </summary>
        public List<T>? Items { get; set; }

        /// <summary>
        /// Total items count
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Total pages count
        /// </summary>
        public byte? TotalPages { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="items">Items list</param>
        /// <param name="totalItems">Total items count</param>
        public PagerOutputDTO(List<T> items, int totalItems)
        {
            Items = items;
            TotalItems = totalItems;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="items">Items list</param>
        /// <param name="totalItems">Total items count</param>
        /// <param name="itemsCount">Items count per page</param>
        public PagerOutputDTO(List<T> items, int totalItems, byte itemsCount)
        {
            Items = items;
            TotalItems = totalItems;
            TotalPages = (byte) Math.Ceiling((float) totalItems / (float) itemsCount);
        }
    }
}
