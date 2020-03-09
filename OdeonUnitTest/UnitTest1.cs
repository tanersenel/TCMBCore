using ExpressionBuilder.Common;
using ExpressionBuilder.Operations;
using NUnit.Framework;
using OdeonTCMBLib;
using OdeonTCMBLib.Models;
using System.Collections.Generic;
using static OdeonTCMBLib.Enums.Types;

namespace OdeonUnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void GetTodayExhangeRate_NoParameters()
        {
            TSTCMB lib = new TSTCMB("");
            List<FilterModel> filters = new List<FilterModel>();
            var results = lib.GetTodayExhangeRate(filters: filters);
            var objectResult = results as ResultModel;
            Assert.NotNull(results);
            Assert.NotNull(objectResult);
           
        }
        [Test]
        public void GetTodayExhangeRate_Wrong_Auth_Key()
        {
            TSTCMB lib = new TSTCMB("343242");
            var results = lib.GetTodayExhangeRate();
            var objectResult = results as ResultModel;
            Assert.NotNull(results);
            Assert.NotNull(objectResult.ObjectResult);
            
        }
        [Test]
        public void GetTodayExhangeRate_NotNullObject_WhenGroupFilter()
        {
            TSTCMB lib = new TSTCMB("");
            List<FilterModel> filters = new List<FilterModel>()
              {
			
				//CurrencyCode USD "VEYA" EUR olanları filtreleme Group=true gönderilmelidir.
				new FilterModel()
                {
                    FilterColumn = PropertyNames.CurrencyCode,
                    FilterValue1 = "USD",
                    Condition = Operation.EqualTo, 
					Connector = Connector.Or, 
					FilterColumn2 = PropertyNames.CurrencyCode,
					FilterValue2 = "EUR",
                    Condition2 = Operation.EqualTo,
					Group = true ,
					GroupConnector = Connector.And 
				}

          };
            var results = lib.GetTodayExhangeRate();
            var objectResult = results as ResultModel;
            Assert.NotNull(results);
            Assert.NotNull(objectResult.ObjectResult);

        }
        [Test]
        public void GetTodayExhangeRate_NotNullObject_WhenBetweenFilter()
        {
            TSTCMB lib = new TSTCMB("");
            List<FilterModel> filters = new List<FilterModel>()
              {
                //Alış Fiyatına göre 6.0 ile 10.0 arasında olanları filtreleme
                new FilterModel()
                {
                    FilterColumn = PropertyNames.ForexBuying,
                    FilterValue1 = 6.0,
                    FilterValue2 = 10.0,
                    Condition = Operation.Between
                }

          };
            var results = lib.GetTodayExhangeRate();
            var objectResult = results as ResultModel;
            Assert.NotNull(results);
            Assert.NotNull(objectResult.ObjectResult);

        }
        [Test]
        public void GetTodayExhangeRate_NotNullObject_WhenGroupDifferentPropertyFilter()
        {
            TSTCMB lib = new TSTCMB("");
            List<FilterModel> filters = new List<FilterModel>()
              {
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
            var results = lib.GetTodayExhangeRate();
            var objectResult = results as ResultModel;
            Assert.NotNull(results);
            Assert.NotNull(objectResult.ObjectResult);

        }

    }
}