using File.Manager;

namespace File.Manager
{
    /// <summary>
    /// Tab item data model
    /// </summary>
    public class TabItemModel
    {
        /// <summary>
        /// The header of this tab
        /// </summary>
        public string Header { get; set; } = string.Empty;

        /// <summary>
        /// The content of this tab
        /// </summary>
        public DriveBarControlViewModel? Content { get; set; }
    }
}