using System.Xml;

namespace OdeonTCMBLib.Models
{
    public class ResultModel
    {
        public ExchangeRateModel ObjectResult { get; set; }
        public XmlResultModel XmlResult { get; set; }
        public string JsonResult { get; set; }
        public CsvResultModel CsvResult { get; set; }
        public ErrorModel Error { get; set; }
    }
    public class XmlResultModel
    {
        public XmlDocument XmlDocumentObject { get; set; }
        public string XmlFileName { get; set; }
    }
    public class CsvResultModel
    {
        public string CsvString { get; set; }
        public string CsvFileName { get; set; }
    }
}

