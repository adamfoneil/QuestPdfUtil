using System.Xml.Linq;

namespace QuestPdfUtil
{
    public static class XmlHelper
    {
        public static XDocument ToDocument(string input, string defaultEnclosingTag = "body", Dictionary<string, string>? replacements = null)
        {
            if (replacements != null)
            {
                // the input may have some known "errors" that need to be made xml-compliant
                foreach (var key in replacements) input = input.Replace(key.Key, key.Value);
            }

            try
            {
                // maybe the content is already XML-compliant
                return XDocument.Parse(input);
            }
            catch
            {
                // or maybe you just need an enclosing tag
                return XDocument.Parse($"<{defaultEnclosingTag}>{input}</{defaultEnclosingTag}>");
            }

            // at this point, it was not meant to be (your input is too broken)
        }
    }
}