
using ExpressionBuilder.Common;
using System;
using static OdeanTCMBLib.Enums.Types;

namespace OdeanTCMBLib.Models
{
    public class FilterModel
    {
        public FilterPropertyNames FilterColumn { get; set; }
        public object FilterValue1 { get; set; }
        public object FilterValue2 { get; set; }
        public Operation Condition { get; set; }
        public FilterStatementConnector Connector { get; set; }



    }
}
