namespace File.Manager
{
    /// <summary>
    /// An extension of fontawesome icons used in this application 
    /// </summary>
    public static class FontAwesomeExtension
    {
        /// <summary>
        /// Gets a font-awesome icon type requested
        /// </summary>
        /// <param name="Icon">The icon requested to get</param>
        /// <returns>Font-awesome icon</returns>
        public static string GetFontAwesomeIcon(this IconType Icon)
        {
            // Look for the icon
            switch(Icon)
            {
                // Folder icon
                case IconType.Folder:
                    return "\uf07b";
                // Music icon
                case IconType.Music:
                    return "\uf001";
                // Pictures icon
                case IconType.Pictures:
                    return "\uf03e";
                // Video icon
                case IconType.Videos:
                    return "\uf03d";
                // Downloads
                case IconType.Downloads:
                    return "\uf019";
                //Drives icon
                case IconType.Drives:
                    return "\uf0a0";
                // This PC icon
                //case IconType.ThisPC:
                //    return "\ue4e5";
                case IconType.Home:
                    return "\ue163";
                // Documents
                case IconType.Documents:
                    return "\uf15c";
                // Search
                case IconType.Search:
                    return "\xf002";
                // Search
                case IconType.Close:
                    return "\xf00d";
                // Default icon to return [this is set to folder icon]
                default:
                    return "\uf07b";
            }
        }
    }
}
