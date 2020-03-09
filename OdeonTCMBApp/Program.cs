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
            TSTCMB lib = new TSTCMB(""); // class constructor içine authkey ekliyoruz.

			List<FilterModel> filters = new List<FilterModel>()
			  {
				//Sadece CurrencyCode a göre filtreleme
				new FilterModel()
				{
					FilterColumn =PropertyNames.CurrencyCode,
					FilterValue1 = "EUR",
					Condition = Operation.EqualTo,
					Connector = Connector.And
				},
				//CurrencyCode USD "VEYA" EUR olanları filtreleme Group=true gönderilmelidir.
				new FilterModel()
				{
					FilterColumn = PropertyNames.CurrencyCode,
					FilterValue1 = "USD",
					Condition = Operation.EqualTo, //CurrencyCode == "USD"
					Connector = Connector.Or, // || 
					FilterColumn2 = PropertyNames.CurrencyCode, //grupta kullanılacak ikinci alan
					FilterValue2 = "EUR",
					Condition2 = Operation.EqualTo,//CurrencyCode == "EUR"
					Group = true ,//çıktısı (x=> (x.CurrencoCode =="USD" || x.CurrencyCode ==""EUR))
					GroupConnector = Connector.And //bir sonraki filtre ile arasındaki Connector çıktısı:  (x=> (x.CurrencoCode =="USD" || x.CurrencyCode ==""EUR)) && 
				},        
				//Alış Fiyatı 6.5 dan büyük ve Satış fiyatı 7.2 den küçük olanları filtreleme
				new FilterModel()
				{
					FilterColumn = PropertyNames.ForexBuying,
					FilterValue1 = 6.5,
					Condition = Operation.GreaterThan,
					FilterColumn2 = PropertyNames.ForexSelling,
					FilterValue2 = 7.2,
					Condition2 = Operation.LessThan,
					Connector= Connector.And,
					Group=true
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
            var response = lib.GetTodayExhangeRate();

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
