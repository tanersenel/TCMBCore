using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using OdeonTCMBLib.Models;

namespace OdeonTCMBLib.Functions
{
    public static class Serializer
    {
        public static T Deserialize<T>(string input) where T : class
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (StringReader sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }

        public static XmlResultModel SerializeXml<T>(T ObjectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(ObjectToSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, ObjectToSerialize);
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(textWriter.ToString());

                string fileName = DateTime.Now.ToFileTimeUtc() + ".xml";
                xd.Save(fileName);
                return new XmlResultModel() { XmlDocumentObject = xd , XmlFileName=fileName };
            }
        }
        public static CsvResultModel SerializeCSV<T>(T ObjectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(ObjectToSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, ObjectToSerialize);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(textWriter.ToString());

                string fileName = DateTime.Now.ToFileTimeUtc() + ".csv";

                XElement xmlElement = XElement.Parse(xmlDoc.InnerXml.ToString());
                var csvData = GetCsvFromXML(xmlElement);
               
                using (StreamWriter file =
                new StreamWriter(fileName, true))
                {
                    file.Write(csvData);
                }
                return new CsvResultModel() { CsvString = csvData, CsvFileName = fileName };

            }
           
        }

        private static string GetCsvFromXML(XElement xmlElement)
        {
            StringBuilder sb = new StringBuilder();
            var lines = from d in xmlElement.Elements()
                        let line = string.Join(",", d.Elements().Select(s => s.Value))
                        select line;
            sb.Append(string.Join(Environment.NewLine, lines));
            return sb.ToString();

        }

        public static string SerializeJson<T>(T ObjectToSerialize)
        {
            return JsonConvert.SerializeObject(ObjectToSerialize);
        }

    }
}
