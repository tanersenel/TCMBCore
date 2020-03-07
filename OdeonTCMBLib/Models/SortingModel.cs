using System;
using System.Collections.Generic;
using System.Text;
using static OdeonTCMBLib.Enums.Types;

namespace OdeonTCMBLib.Models
{
    public class SortingModel
    {
        public PropertyNames SortingColumn { get; set; }
        public SortingTypes SortingType { get; set; }
    }
}
