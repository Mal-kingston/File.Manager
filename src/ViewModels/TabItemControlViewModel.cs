using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File.Manager
{
    /// <summary>
    /// A basic tab item view model
    /// </summary>
    public class TabItemControlViewModel :ViewModelBase
    {
        /// <summary>
        /// The header of this tab
        /// </summary>
        public string? Header { get; set; }

        /// <summary>
        /// The content of this tab
        /// </summary>
        public object? Content { get; set; }

    }
}