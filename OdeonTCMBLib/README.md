
# TCMBCore
.Net Core TCMB Exchange rate app sample 

# Nuget Package Manager Console dan projeye eklenebilir
https://www.nuget.org/packages/OdeonTCMBLib/
```
PM>Install-Package OdeonTCMBLib -Version 1.0.0
```

# Proje Açıklaması
Proje TCMB döviz kurları sayfasından günlük döviz kurlarını belirlenenkriterlere filtreler.
Proje .Net Core 3.0 ile hazırlanmıştır
# Kullanılan Harici Kütüphaneler (Nuget)
Newtonsoft.Json
NUnit
System.Configuration.ConfigurationManager



# Kütüphaneleri projemize Referans olarak ekliyoruz.
```CSharp
using OdeonTCMBLib;
using OdeonTCMBLib.Models;
using static OdeonTCMBLib.Enums.Types;
using ExpressionBuilder.Operations;
using ExpressionBuilder.Common;
```
# Örnek Kullanım
```CSharp
		TSTCMB lib = new TSTCMB(""); // class constructor içine authkey ekliyoruz.
          //filtrelerimizi oluşturuyoruz
          //yazdığımız alanın data tipine göre value göndermeliyiz. int ise int double ise double. CurrencyModel den data tiplerini görebilirsiniz
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
```
# Filtreleme Örnekleri
   - Basic Kullanım (Tek Para Birimine Göre Filtreleme)
 ```CSharp
 List<FilterModel> filters = new List<FilterModel>()
{ 
	new FilterModel()
	{
		FilterColumn =PropertyNames.CurrencyCode,
		FilterValue1 = "EUR",
		Condition = Operation.EqualTo,
		Connector = Connector.And
	}
};
 ```
 - Kur Adının İlk harfine göre filtreleme
```CSharp
List<FilterModel> filters = new List<FilterModel>()
{ 
	new FilterModel()
	{
		FilterColumn = PropertyNames.CurrencyName,
		FilterValue1 = "U",
		Condition = Operation.StartsWith
	}
};
```
 - CurrencyCode USD "VEYA" EUR olanları filtreleme Group=true gönderilmelidir.
```CSharp
List<FilterModel> filters = new List<FilterModel>()
{
	new FilterModel()
	{
		FilterColumn = PropertyNames.CurrencyCode,
		FilterValue1 = "USD",
		Condition = Operation.EqualTo, //CurrencyCode == "USD"
		Connector = Connector.Or, // || 
		FilterColumn2 = PropertyNames.CurrencyCode, //grupta kullanılacak ikinci alan
		FilterValue2 = "EUR",
		Condition2 = Operation.EqualTo, //CurrencyCode == "EUR"
		Group = true, //çıktısı (x=> (x.CurrencoCode =="USD" || x.CurrencyCode ==""EUR))
		GroupConnector = Connector.And //bir sonraki filtre ile arasındaki Connector çıktısı:  (x=> (x.CurrencoCode =="USD" || x.CurrencyCode ==""EUR)) && 
	}
};
```
 - Alış Fiyatına göre 6.0 ile 10.0 arasında olanları filtreleme
```CSharp
List<FilterModel> filters = new List<FilterModel>()
{
	new FilterModel()
	{
		FilterColumn = PropertyNames.ForexBuying,
		FilterValue1 = 6.0,
		FilterValue2 = 10.0,
		Condition = Operation.Between
	}
};
```
 - Alış Fiyatı 6.5 dan büyük ve Satış fiyatı 7.2 den küçük olanları filtreleme
```CSharp
List<FilterModel> filters = new List<FilterModel>()
{
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
```
# Filtrelemede Data tipine göre kullanılabilecek Operation Tipleri
 

<ul>
<li>Default
<ul>
<li>EqualTo</li>
<li>NotEqualTo</li>
</ul>
</li>
<li>Text
<ul>
<li>Contains</li>
<li>DoesNotContain</li>
<li>EndsWith</li>
<li>EqualTo</li>
<li>IsEmpty</li>
<li>IsNotEmpty</li>
<li>IsNotNull</li>
<li>IsNotNullNorWhiteSpace</li>
<li>IsNull</li>
<li>IsNullOrWhiteSpace</li>
<li>NotEqualTo</li>
<li>StartsWith</li>
</ul>
</li>
<li>Number
<ul>
<li>Between</li>
<li>EqualTo</li>
<li>GreaterThan</li>
<li>GreaterThanOrEqualTo</li>
<li>LessThan</li>
<li>LessThanOrEqualTo</li>
<li>NotEqualTo</li>
</ul>
</li>
<li>Boolean
<ul>
<li>EqualTo</li>
<li>NotEqualTo</li>
</ul>
</li>
<li>Date
<ul>
<li>Between</li>
<li>EqualTo</li>
<li>GreaterThan</li>
<li>GreaterThanOrEqualTo</li>
<li>LessThan</li>
<li>LessThanOrEqualTo</li>
<li>NotEqualTo</li>
</ul>
</li>
</ul>

### Todos

 - Write Unit Tests

License
----

MIT


