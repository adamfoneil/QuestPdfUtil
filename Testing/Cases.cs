using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Previewer;
using QuestPdfUtil;

namespace Testing
{
    [TestClass]
    public class Cases
    {
        [TestMethod]
        public void Case01()
        {
            var htmlSrc = Resources.GetString("Case01-input.html");
            var htmlDoc = XmlHelper.ToDocument(htmlSrc);
            
            var doc = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30f);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(style => style.FontSize(10f));
                    
                    page.Content().Html(htmlDoc);
                });
            });

            doc.ShowInPreviewer();
        }
    }
}