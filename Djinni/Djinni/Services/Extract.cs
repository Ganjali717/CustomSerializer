using Djinni.Data.Entities;
using System.Text.RegularExpressions;

namespace Djinni.Services
{
    public class Extract
    {
        public struct Data
        {
            public string PropertyName;
            public string Value;
        }

        public static List<string> ExtractData(string text, string startString = "{", string endString = "}", bool raw = true)
        {
            string trimmedtxt = text.Trim();
            var matched = new List<string>();
            var exit = false;
            while (!exit)
            {
                var indexStart = trimmedtxt.IndexOf(startString, StringComparison.Ordinal);
                var indexEnd = trimmedtxt.IndexOf(endString, StringComparison.Ordinal);
                if (indexStart != -1 && indexEnd != -1)
                {
                    if (raw)
                        matched.Add("{" + trimmedtxt.Substring(indexStart + startString.Length, indexEnd - indexStart - startString.Length) + "}");
                    else
                        matched.Add(trimmedtxt.Substring(indexStart + startString.Length, indexEnd - indexStart - startString.Length));

                    trimmedtxt = trimmedtxt.Substring(indexEnd + endString.Length); 
                }
                else
                {
                    exit = true;
                }
            }
            return matched;
        }


        public static List<Data> ExtractValuesFromData(string text)
        {
            var listOfData = new List<Data>();
            var allData = ExtractData(text, "{", "}");

            string trimmed = text.Trim();

            string firstname = Regex.Match(trimmed, @"^firstName").ToString();
            
            

            foreach (var data in allData)
            {
                var pName = data.Substring(2, data.IndexOf(":", StringComparison.Ordinal) - 2);
                var pValue = data.Substring(data.IndexOf(":", StringComparison.Ordinal) + 10, data.IndexOf('\"'));
                listOfData.Add(new Data { PropertyName = pName, Value = pValue });
            }
            return listOfData;
        }
    }
}
