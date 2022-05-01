using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Xml;
using System.Xml.Linq;

namespace QuestPdfUtil
{
    public static class QuestPdfHelper
    {
        public static void Html(this IContainer container, XDocument document) =>
            Html(container, document?.Root ?? throw new Exception("Document may not be null"));

        public static void Html(this IContainer container, XElement element)
        {
            container.Column(col =>
            {
                foreach (var child in element.Elements())
                {
                    switch (child.Name.ToString().ToLower())
                    {
                        case "h1":
                            RenderText(col.Item(), child, spanAction: (span) => span.FontSize(15));
                            break;

                        case "div":
                            RenderText(col.Item(), child);
                            break;
                    }
                }
            });            
        }
        
        private static void RenderText(IContainer container, XElement element, Action<TextDescriptor> textAction = null, Action<TextSpanDescriptor> spanAction = null)
        {
            container.Text(text =>
            {
                var nodes = element.Nodes().ToArray();
                foreach (var node in nodes)
                {
                    TextSpanDescriptor span = null;

                    switch (node.NodeType)
                    {
                        case XmlNodeType.Text:
                            span = text.Span(node.ToString());
                            ApplyStyle(span, element);
                            break;

                        case XmlNodeType.Element:
                            var inner = node as XElement;
                            span = text.Span(inner.Value);
                            ApplyStyle(span, inner);
                            break;
                    }

                    if (span != null) spanAction?.Invoke(span);
                }                                        

                textAction?.Invoke(text);
            });
        }

        private static void ApplyStyle(TextSpanDescriptor span, XElement inner)
        {
            // fill this dictionary with css expressions that map to QuestPDF methods
            var supportedStyles = new Dictionary<string, Dictionary<string, Action<TextSpanDescriptor>>>()
            {
                ["font-weight"] = new Dictionary<string, Action<TextSpanDescriptor>>()
                {
                    ["bold"] = (span) => span.Bold(),
                    ["normal"] = (span) => span.NormalWeight(),
                },
                ["font-style"] = new Dictionary<string, Action<TextSpanDescriptor>>()
                {
                    ["italic"] = (span) => span.Italic(),
                    ["underline"] = (span) => span.Underline()
                }
            };

            var styles = inner.Attribute("style")?.Value.ToString();
            if (styles is null) return;
            
            var styleDictionary = styles.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(item =>
            {
                var parts = item.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries); return new
                {
                    Key = parts[0].Trim(),
                    Value = parts[1].Trim()
                };
            }).ToDictionary(item => item.Key, item => item.Value);
            
            foreach (var style in styleDictionary)
            {
                // do we support this style attribute?
                if (supportedStyles.TryGetValue(style.Key, out var attribute))
                {
                    // do we support this style attribute value?
                    if (attribute.TryGetValue(style.Value, out var value))
                    {
                        value.Invoke(span);
                    }
                }
            }
        }
    }
}
