using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuestPdfUtil;

namespace Testing
{
    [TestClass]
    public class Xml
    {
        [TestMethod]
        public void HtmlParse()
        {
            var html = Resources.GetString("Case01-input.html");
            var xmlDoc = XmlHelper.ToDocument(html);
        }
    }
}
