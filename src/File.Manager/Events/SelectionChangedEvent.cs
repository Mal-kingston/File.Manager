namespace File.Manager
{
    /// <summary>
    /// Event that fires when an item of a collection is selected
    /// </summary>
    public class SelectionChangedEvent : EventArgs
    {
        /// <summary>
        /// The actual event to invoke
        /// </summary>
        public event EventHandler? SelectionChanged;

        /// <summary>
        /// Called whenever item of a collection is selected
        /// </summary>
        /// <param name="obj">The object being selected</param>
        public void ItemSelected(object obj)
        {
            // Invoke our event 
            SelectionChanged?.Invoke(obj, Empty);
        }
    }
}
