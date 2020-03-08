using System.Collections.Generic;
using System.Xml.Serialization;

namespace OdeonTCMBLib.Models
{
   
    [XmlRoot(ElementName = "Currency")]
    public class Currency
    {
        [XmlElement(ElementName = "Unit")]
        public string Unit { get; set; }
        [XmlElement(ElementName = "Isim")]
        public string Isim { get; set; }
        [XmlElement(ElementName = "CurrencyName")]
        public string CurrencyName { get; set; }

        [XmlIgnore]
        public double ForexBuying { get; set; }
        [XmlElement("ForexBuying")]
        public string ForexBuyingString
        {
            get { return ForexBuying.ToString("F2");}
            set { double amount = 0;
                if (double.TryParse(value, out amount))
                    ForexBuying = amount;
            }
        }
        [XmlIgnore]
        public double ForexSelling { get; set; }
        [XmlElement("ForexSelling")]
        public string ForexSellingString
        {
            get { return ForexSelling.ToString("F2"); }
            set
            {
                double amount = 0;
                if (double.TryParse(value, out amount))
                    ForexSelling = amount;
            }
        }   
        public double BanknoteBuying { get; set; }
        [XmlElement("BanknoteBuying")]
        public string BanknoteBuyingString
        {
            get { return BanknoteBuying.ToString("F2"); }
            set
            {
                double amount = 0;
                if (double.TryParse(value, out amount))
                    BanknoteBuying = amount;
            }
        }
        public double BanknoteSelling { get; set; }
        [XmlElement("BanknoteSelling")]
        public string BanknoteSellingString
        {
            get { return BanknoteSelling.ToString("F2"); }
            set
            {
                double amount = 0;
                if (double.TryParse(value, out amount))
                    BanknoteSelling = amount;
            }
        }

        public double CrossRateUSD { get; set; }
        [XmlElement("CrossRateUSD")]
        public string CrossRateUSDString
        {
            get { return CrossRateUSD.ToString("F2"); }
            set
            {
                double amount = 0;
                if (double.TryParse(value, out amount))
                    CrossRateUSD = amount;
            }
        }
        public double CrossRateOther { get; set; }
        [XmlElement("CrossRateOther")]
        public string CrossRateOtherString
        {
            get { return CrossRateOther.ToString("F2"); }
            set
            {
                double amount = 0;
                if (double.TryParse(value, out amount))
                    CrossRateOther = amount;
            }
        }
      
        [XmlAttribute(AttributeName = "CrossOrder")]
        public string CrossOrder { get; set; }
        [XmlAttribute(AttributeName = "Kod")]
        public string Kod { get; set; }
        [XmlAttribute(AttributeName = "CurrencyCode")]
        public string CurrencyCode { get; set; }
    }

    [XmlRoot(ElementName = "Tarih_Date")]
    public class ExchangeRateModel
    {
        [XmlElement(ElementName = "Currency")]
        public List<Currency> Currency { get; set; }
        [XmlAttribute(AttributeName = "Tarih")]
        public string Tarih { get; set; }
        [XmlAttribute(AttributeName = "Date")]
        public string Date { get; set; }
        [XmlAttribute(AttributeName = "Bulten_No")]
        public string Bulten_No { get; set; }
    }
}
