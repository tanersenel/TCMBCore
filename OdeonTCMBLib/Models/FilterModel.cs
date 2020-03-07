
using ExpressionBuilder.Common;
using System;
using static OdeonTCMBLib.Enums.Types;

namespace OdeonTCMBLib.Models
{
    public class FilterModel
    {
        public PropertyNames FilterColumn { get; set; }
        public object FilterValue1 { get; set; }
        public object FilterValue2 { get; set; }
        public Operation Condition { get; set; }
        public FilterStatementConnector Connector { get; set; }



    }
}
