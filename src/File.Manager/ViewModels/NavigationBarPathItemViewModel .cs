namespace File.Manager
{
    /// <summary>
    /// View model for Navigation bar path item <see cref="NavigationBarPathItemControl"/>
    /// </summary>
    public class NavigationBarPathItemViewModel : ViewModelBase
    {
        /// <summary>
        /// Directory name
        /// </summary>
        public string DirectoryName { get; set; } = string.Empty;
    }
}
