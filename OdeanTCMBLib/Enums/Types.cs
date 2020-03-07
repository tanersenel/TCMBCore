namespace OdeanTCMBLib.Enums
{
    public class Types
    {
        public enum OutputTypes
        {
            Csv = 0,
            Xml = 1,
            Json = 2,
            Obj = 3
        }
        public enum SortingTypes
        {
            ASC = 0,
            DESC = 1
        }
        
        public enum PropertyNames
        {
            Unit,
            Isim,
            CurrencyName,
            ForexBuying,
            ForexSelling,
            BanknoteBuying,
            BanknoteSelling,
            CrossRateUSD,
            CrossRateOther,
            CrossOrder,
            Kod,
            CurrencyCode
        }

    }
}
