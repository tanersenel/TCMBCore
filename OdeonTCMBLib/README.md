# TCMBCore
.Net Core TCMB Exchange rate app sample 

<h2>TCMB Core Project</h2>
<h3> Kütüphaneleri projemize Referans olarak ekliyoruz.</h3>
<pre>
      using OdeonTCMBLib;
      using ExpressionBuilder.Common;
      using OdeonTCMBLib.Models;
      using static OdeonTCMBLib.Enums.Types;
</pre>

<h3>Örnek Kullanım</h3>
<pre>
            TSTCMB lib = new TSTCMB(""); // class constructer içine authkey ekliyoruz.
            //filtrelerimizi oluşturuyoruz
            //yazdığımız alanın data tipine göre value göndermeliyiz. int ise int double ise double. CurrencyModel den data tiplerini görebilirsiniz
            List<FilterModel> filters = new List<FilterModel>()
            {
                    //CurrencyCode a göre filtreleme
                    new FilterModel()
                    {
                        FilterColumn = PropertyNames.CurrencyCode,
                        FilterValue1 = "USD",
                        Condition = Operation.EqualTo,
                        Connector = FilterStatementConnector.Or
                    },
                     //CurrencyCode USD "VEYA" EUR olanları filtreleme bir önceki filterda  
					 //FilterStatementConnector.Or veya koşulunu ekler
                    new FilterModel()
                    {
                        FilterColumn =PropertyNames.CurrencyCode,
                        FilterValue1 = "EUR",
                        Condition = Operation.EqualTo,
                        Connector = FilterStatementConnector.And
                    },
                     //Alış Fiyatına göre 5.0 ile 15.0 arasında olanları filtreleme
                    new FilterModel()
                    {
                        FilterColumn = PropertyNames.ForexBuying,
                        FilterValue1 = 5.0,
                        FilterValue2 = 10.0,
                        Condition = Operation.Between,
                        Connector = FilterStatementConnector.And
                    },
                     //Satış Fiyatına değeri 7.0 dan büyük olanları filtreleme
                    new FilterModel()
                    {
                        FilterColumn = PropertyNames.ForexSelling,
                        FilterValue1 = 7.0,
                        Condition = Operation.GreaterThan,
                        Connector = FilterStatementConnector.And
                    }
            };

            //sıralama kriterimizi ekliyoruz. 
            var sorting = new SortingModel()
            {
                SortingColumn = PropertyNames.CurrencyCode,
                SortingType = SortingTypes.ASC
            };

            //kütüphanemize sorguyu gönderiyoruz.
            //4 farklı tipte data response içerisinde yer alır.
            var response = lib.GetTodayExhangeRate(sorting, filters);

            if (response.Error.Error)
            {
                Console.WriteLine(response.Error.ErrorMessage);
            }
            else
            {
                var obj = response.ObjectResult;
                var xml = response.XmlResult;
                var json = response.JsonResult;
                var csv = response.CsvResult;
            }
</pre>