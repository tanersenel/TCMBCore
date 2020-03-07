using ExpressionBuilder.Generics;
using OdeonTCMBLib.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using OdeanTCMBLib.Models;
using static OdeanTCMBLib.Enums.Types;

namespace OdeanTCMBLib
{
    public class TSTCMB
    {
        static string _authKey = "";
        static bool auth = false;
        public TSTCMB(string authKey)
        {
            if (_authKey == authKey)
            {
                auth = true;
                _authKey = authKey;
            }
        }
        public ResultModel GetTodayExhangeRate(SortingDataTypes sortingColumn, SortingTypes sortingType, List<FilterModel> filters)
        {
            ResultModel result = new ResultModel();
            
            //authkey yanlış ise error döner
            if (!auth)
            {
                result.Error = new ErrorModel() { Error = true, ErrorMessage = "Invalid Login Attempt!!!" };
                return result;
            }
            try
            {
                using (var client = new WebClient())
                {
                    var xmlStr = client.DownloadString("https://www.tcmb.gov.tr/kurlar/today.xml"); //xmli string olarak downoad ediyoruz.
                    var exchangeRates = Serializer.Deserialize<ExchangeRateModel>(xmlStr); //xml stringi objeye deserialize ediyoruz.
                    string sortingColumnName = Enum.GetName(typeof(SortingDataTypes), sortingColumn); // filtrelenmesi istenen sütun adını alıyoruz

                    //filter expressionları oluşturuyoruz.
                    var filter = new Filter<Currency>();
                    foreach (var filteritem in filters)
                    {
                        string filterColumName = Enum.GetName(typeof(FilterPropertyNames), filteritem.FilterColumn);
                        filter.By(filterColumName, filteritem.Condition, filteritem.FilterValue1, filteritem.FilterValue2, filteritem.Connector);
                    }
                   
                    exchangeRates.Currency = exchangeRates.Currency.Where(filter).ToList();
                    //sıralamayı yapıyoruz.
                    if (sortingType == SortingTypes.ASC)
                    {
                        exchangeRates.Currency = exchangeRates.Currency.OrderBy(x => x.GetType().GetProperty(sortingColumnName).GetValue(x, null)).ToList();
                    }
                    else if (sortingType == SortingTypes.DESC)
                    {
                        exchangeRates.Currency = exchangeRates.Currency.OrderByDescending(x => x.GetType().GetProperty(sortingColumnName).GetValue(x, null)).ToList();
                    }
                    result.JsonResult = Serializer.SerializeJson<ExchangeRateModel>(exchangeRates);
                    result.ObjectResult = exchangeRates;
                    result.XmlResult = Serializer.SerializeXml<ExchangeRateModel>(exchangeRates);
                    result.CsvResult = Serializer.SerializeCSV<ExchangeRateModel>(exchangeRates);
                    result.Error = new ErrorModel() { Error = false, ErrorMessage = "" };
                }
            }
            catch (Exception ex)
            {
                result.Error.Error = true;
                result.Error.ErrorMessage = ex.Message;
            }
            return result;
        }


    }
}
