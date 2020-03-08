
using ExpressionBuilder.Common;
using ExpressionBuilder.Interfaces;
using ExpressionBuilder.Operations;
using System;
using static OdeonTCMBLib.Enums.Types;

namespace OdeonTCMBLib.Models
{
    public class FilterModel
    {
        public PropertyNames FilterColumn { get; set; }
        public object FilterValue1 { get; set; }
        public object FilterValue2 { get; set; }
        public IOperation Condition { get; set; }
        public IOperation Condition2 { get; set; }
        public Connector Connector { get; set; }
        public bool Group { get; set; }



    }
}
