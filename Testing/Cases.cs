using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Previewer;
using QuestPdfUtil;
using System;
using System.IO;
using System.Linq;

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
            
            var actualOutput = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30f);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(style => style.FontSize(10f));                    
                    page.Content().Html(htmlDoc);
                });
            }).GeneratePdf();

            /*
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "QuestPdfUtil", "Case01-output.pdf");
            var folder = Path.GetDirectoryName(path);
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);            
            File.WriteAllBytes(path, actualOutput);
            */
            
            /*
             * it seems you can't do a binary comparison of PDFs -- the same PDF will be slightly different each time you build it.
            var expectedOutput = Resources.GetBytes("Case01-output.pdf");

            if (expectedOutput.Length == actualOutput.Length)
            {
                for (int i = 0; i < expectedOutput.Length; i++)
                {
                    if (expectedOutput[i] != actualOutput[i]) Assert.Fail();
                }                
            } 
            */
        }
    }
}