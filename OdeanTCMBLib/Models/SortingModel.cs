using System;
using System.Collections.Generic;
using System.Text;
using static OdeanTCMBLib.Enums.Types;

namespace OdeanTCMBLib.Models
{
    public class SortingModel
    {
        public PropertyNames SortingColumn { get; set; }
        public SortingTypes SortingType { get; set; }
    }
}
