[QuestPDF](https://www.questpdf.com/documentation/#introduction) is a really neat library. But in my situation, I need the ability to insert HTML snippets into PDFs, which is not supported functionality. See [QuestPDF issue 134](https://github.com/QuestPDF/QuestPDF/issues/134). To be fair, converting HTML to PDF is wildly complicated if you want to support every CSS feature. There are paid libraries from [Syncfusion](https://help.syncfusion.com/file-formats/pdf/converting-html-to-pdf), for example, that do this. But these libraries tend to have large dependency footprints and do way more than I need.

Fortunately, my requirements are pretty modest. This project represents my attempt at PDF conversion from HTML, supporting a very small feature set. There are two main things I needed to do:

- get an XML document (in the form of `XDocument` or `XElement`) from a string, with some forgiveness built-in for certain malformed tags. In my use case, the HTML is not perfectly XHTML-compliant. But I needed a valid XML document before attempting any conversion. This is the `XmlHelper.ToDocument` method below.
- provide an extension method that works with QuestPDF's `IContainer` that does the actual conversion.

See this in use in my [test method](https://github.com/adamfoneil/QuestPdfUtil/blob/master/Testing/Cases.cs#L69). Note that I couldn't seem to get a real [unit test working](https://github.com/adamfoneil/QuestPdfUtil/blob/master/Testing/Cases.cs#L22) with a binary comparison of expected and actual PDF outputs. It appears that there are always minor differences between outputs of the same PDF. I've seen this symptom in other PDF libraries, so I figure this is just how it is with PDFs. In any case, I was pretty happy to get this much working.

# Reference

## QuestPdfUtil.QuestPdfHelper [QuestPdfHelper.cs](https://github.com/adamfoneil/QuestPdfUtil/blob/master/QuestPdfUtil/QuestPdfHelper.cs#L8)
- void [Html](https://github.com/adamfoneil/QuestPdfUtil/blob/master/QuestPdfUtil/QuestPdfHelper.cs#L10)
 (this IContainer container, XDocument document)
- void [Html](https://github.com/adamfoneil/QuestPdfUtil/blob/master/QuestPdfUtil/QuestPdfHelper.cs#L13)
 (this IContainer container, XElement element)

## QuestPdfUtil.XmlHelper [XmlHelper.cs](https://github.com/adamfoneil/QuestPdfUtil/blob/master/QuestPdfUtil/XmlHelper.cs#L5)
- XDocument [ToDocument](https://github.com/adamfoneil/QuestPdfUtil/blob/master/QuestPdfUtil/XmlHelper.cs#L7)
 (string input, [ string defaultEnclosingTag ], [ Dictionary<string, string>? replacements ])
