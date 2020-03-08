using System;
using System.Collections.Generic;
using OdeonTCMBLib;
using OdeonTCMBLib.Models;
using static OdeonTCMBLib.Enums.Types;
using ExpressionBuilder.Operations;
using ExpressionBuilder.Common;

namespace OdeonTCMBApp
{
    class Program
    {
        static void Main()
        {
            TSTCMB lib = new TSTCMB(""); // class constructer içine authkey ekliyoruz.
            //filtrelerimizi oluşturuyoruz
            //yazdığımız alanın data tipine göre value göndermeliyiz. int ise int double ise double. CurrencyModel den data tiplerini görebilirsiniz
            List<FilterModel> filters = new List<FilterModel>()
            {
                     //CurrencyCode USD "VEYA" EUR olanları filtreleme Group=true gönderilmelidir.
                     //CurrencyCode USD olan veya  
                    new FilterModel() 
                    {
                        FilterColumn = PropertyNames.CurrencyCode,
                        FilterValue1 = "USD",
                        Condition = Operation.EqualTo, //CurrencyCode == "USD"
                        Connector = Connector.Or, // || 
                        FilterValue2 = "EUR",
                        Condition2 = Operation.EqualTo,//CurrencyCode == "EUR"
                        Group = true //çıktısı (x=> (x.CurrencoCode =="USD" || x.CurrencyCode ==""EUR))
                    },
                     //Sadece CurrencyCode a göre filtreleme
                    //new FilterModel()
                    //{
                    //    FilterColumn =PropertyNames.CurrencyCode,
                    //    FilterValue1 = "EUR",
                    //    Condition = Operation.EqualTo,
                    //    Connector = Connector.And
                    //},
                    //Alış Fiyatına göre 6.0 ile 10.0 arasında olanları filtreleme
                    new FilterModel()
                    {
                        FilterColumn = PropertyNames.ForexBuying,
                        FilterValue1 = 6.0,
                        FilterValue2 = 10.0,
                        Condition = Operation.Between,
                        Connector = Connector.And
                    },
                     //Alış Fiyatına göre 6.0 dan büyük 7.2 den küçük olanları filtreleme
                    new FilterModel()
                    {
                        FilterColumn = PropertyNames.ForexBuying,
                        FilterValue1 = 6.0,
                        Condition = Operation.GreaterThan,
                        FilterValue2 = 7.2,
                        Condition2 = Operation.LessThan,
                        Connector= Connector.And,
                        Group=true
                    },
                     //Satış Fiyatına değeri 7.0 dan büyük olanları filtreleme
                    new FilterModel()
                    {
                        FilterColumn = PropertyNames.ForexSelling,
                        FilterValue1 = 7.0,
                        Condition = Operation.GreaterThan,
                        Connector = Connector.And
                    },
                    //kur adı U harfi ile başlayanları filtreleme
                    new FilterModel()
                    {
                        FilterColumn = PropertyNames.CurrencyName,
                        FilterValue1 = "U",
                        Condition = Operation.StartsWith
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


            Console.Read();

        }
    }
}
