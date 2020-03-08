using OdeonTCMBLib;
using OdeonTCMBLib.Models;
using System;
using Xunit;

namespace OdeonTCMBLibUnitTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        [Fact]
        public void GetTodayExhangeRate_NoParameters()
        {
            TSTCMB lib = new TSTCMB("");
            var results = lib.GetTodayExhangeRate();
            var objectResult = results as ResultModel;
            Assert.NotNull(results);
            Assert.NotNull(objectResult);
            Assert.Equal("", objectResult.Error.ErrorMessage);
        }
        [Fact]
        public void GetTodayExhangeRate_Wrong_Auth_Key()
        {
            TSTCMB lib = new TSTCMB("343242");
            var results = lib.GetTodayExhangeRate();
            var objectResult = results as ResultModel;
            Assert.NotNull(results);
            Assert.NotNull(objectResult);
            Assert.Equal("", objectResult.Error.ErrorMessage);
        }
    }
}
