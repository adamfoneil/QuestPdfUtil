using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPdfUtil;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Testing
{
    [TestClass]
    public class Cases
    {
        [TestMethod]
        public void Case01()
        {
            var actualOutput = BuildPdf("Case01-input.html");

            SaveOutput("Case01-output.pdf", actualOutput);

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

        [TestMethod]
        public void Case03()
        {
            var actualOutput = BuildPdf("Case03-input.html", new Dictionary<string, string>()
            {
                ["<br>"] = "<br/>",
                ["&nbsp;"] = " "
            });

            SaveOutput("Case03-output.pdf", actualOutput);
        }

        private void SaveOutput(string fileName, byte[] content)
        {               
            var dir = new DirectoryInfo(Assembly.GetExecutingAssembly().Location).Parent.Parent.Parent.Parent.FullName;
            var path = Path.Combine(dir, "Resources", fileName);
            var folder = Path.GetDirectoryName(path);
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            File.WriteAllBytes(path, content);
        }

        private byte[] BuildPdf(string resourceName, Dictionary<string, string>? replacements = null)
        {
            var htmlSrc = Resources.GetString(resourceName);
            var htmlDoc = XmlHelper.ToDocument(htmlSrc, replacements: replacements);

            return Document.Create(container =>
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
        }
    }
}