using Djinni.Data.Entities;
using System.Text.RegularExpressions;

namespace Djinni.Services
{
    public class Export
    {
        public struct Data
        {
            public string PropertyName;
            public string Value;
        }

        public static List<string> ExportData(string text, string startString = "{", string endString = "}")
        {
            var matched = new List<string>();
            string trimmed = text.Trim();
            string arrayed, arrayedsecond, splited, secondsplit, thirdremove, fourthsplit, rmvquote, rmvquote2, removewhitespace, quote, quote2, finaltrim, baku = "";
            var indexStart = text.IndexOf(startString, StringComparison.Ordinal);
            var indexEnd = text.IndexOf(endString, StringComparison.Ordinal);
            if (indexStart != -1 && indexEnd != -1)
            {
                arrayed = trimmed.Substring(indexStart + startString.Length, indexEnd - indexStart - startString.Length - 2);
                arrayedsecond = arrayed.Replace("Address: {", "");
                splited = arrayedsecond.Replace("\t","");
                secondsplit = splited.Replace(" \t","");
                thirdremove = secondsplit.Replace("prospect ","");
                fourthsplit = thirdremove.Replace("28/7","");
                rmvquote = fourthsplit.Replace("‘","");
                rmvquote2 = rmvquote.Replace("’","");
                removewhitespace = rmvquote2.Replace(" ", "");
                quote = removewhitespace.Replace("“","");
                quote2 = quote.Replace("”,", "");
                finaltrim = quote2.Trim();
                matched = finaltrim.Split(",").ToList();
                
            }
            return matched;
        }


        public static List<Data> ExporttValuesFromData(List<string> text)
        {
            var listOfData = new List<Data>();
            var allData = text;
            foreach (var data in allData)
            {
                var pName = data.Substring(0, data.IndexOf(":", StringComparison.Ordinal));
                var pValue = data.Substring(data.IndexOf(":", StringComparison.Ordinal) + 1);
                listOfData.Add(new Data { PropertyName = pName, Value = pValue });
            }
            return listOfData;
        }
    }
}
