namespace File.Manager
{
    /// <summary>
    /// Event that fires when <see cref="SideMenuItemControlViewModel"/> is selected
    /// </summary>
    public class SelectionChangedEvent : EventArgs
    {
        /// <summary>
        /// The actual event to invoke
        /// </summary>
        public event EventHandler? SelectionChanged;

        /// <summary>
        /// Called whenever <see cref="SideMenuItemControlViewModel"/> item is selected
        /// </summary>
        /// <param name="directoryItem">The item being selected</param>
        public void ItemSelected(SideMenuItemControlViewModel directoryItem)
        {
            // Invoke our event 
            SelectionChanged?.Invoke(directoryItem, Empty);
        }
    }
}
