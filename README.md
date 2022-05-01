[QuestPDF](https://www.questpdf.com/documentation/#introduction) is a really neat library. But in my situation, I need the ability to insert HTML snippets into PDFs, which is not supported functionality. See [QuestPDF issue 134](https://github.com/QuestPDF/QuestPDF/issues/134). To be fair, converting HTML to PDF is wildly complicated if you want to support every CSS feature. There are paid libraries from [Syncfusion](https://help.syncfusion.com/file-formats/pdf/converting-html-to-pdf), for example, that do this. But these libraries have a large dependency footprint and do way more than I need.

Fortunately, my requirements are pretty modest. This project represents my attempt at PDF conversion from HTML, supporting a very small feature set.
