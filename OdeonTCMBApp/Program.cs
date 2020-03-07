using ExpressionBuilder.Common;
using OdeanTCMBLib;
using System;
using System.Collections.Generic;
using OdeanTCMBLib.Models;
using static OdeanTCMBLib.Enums.Types;

namespace OdeonTCMBApp
{
    class Program
    {
        static void Main()
        {
            TSTCMB lib = new TSTCMB(""); // class constructer içine authkey ekliyoruz.

            List<FilterModel> filters = new List<FilterModel>();

            //filtrelerimizi oluşturuyoruz
            //yazdığımız alanın data tipine göre value göndermeliyiz. int ise int double ise double. CurrencyModel den data tiplerini görebilirsiniz
            //CurrencyCode a göre filtreleme
            //filters.Add(new FilterModel()
            //{
            //    FilterColumn = "CurrencyCode",
            //    FilterValue1 = "USD",
            //    Condition = Operation.EqualTo
            //});

            //Alış Fiyatına göre filtreleme
            filters.Add(new FilterModel()
            {
                FilterColumn = "ForexBuying",
                FilterValue1 = 5.0,
                FilterValue2 = 15.0,
                Condition = Operation.Between
            });
            //Alış Fiyatına göre filtreleme
            filters.Add(new FilterModel()
            {
                FilterColumn = "ForexSelling",
                FilterValue1 = 5.0,
                FilterValue2 = 15.0,
                Condition = Operation.Between
            });

            //kütüphanemize sorguyu gönderiyoruz.
            //4 farklı tipte data response içerisinde yer alır.
            var response = lib.GetTodayExhangeRate(sortingColumn: SortingDataTypes.CurrencyCode, sortingType: SortingTypes.ASC, filters);

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
