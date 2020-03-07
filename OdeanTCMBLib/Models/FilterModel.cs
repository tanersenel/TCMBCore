
using ExpressionBuilder.Common;
using System;
using static OdeanTCMBLib.Enums.Types;

namespace OdeanTCMBLib.Models
{
    public class FilterModel
    {
        public string FilterColumn { get; set; }
        public object FilterValue1 { get; set; }
        public object FilterValue2 { get; set; }
        public Operation Condition { get; set; }
       
      

    }
}
