
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
        public PropertyNames FilterColumn2 { get; set; }
        public object FilterValue1 { get; set; }
        public object FilterValue2 { get; set; }
        public IOperation Condition { get; set; } = Operation.EqualTo;
        public IOperation Condition2 { get; set; } = Operation.EqualTo;
        public Connector Connector { get; set; } = Connector.And;
        public Connector GroupConnector { get; set; } = Connector.And;
        public bool Group { get; set; } = false;



    }
}
