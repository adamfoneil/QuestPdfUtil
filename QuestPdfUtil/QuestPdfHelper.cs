using QuestPDF.Infrastructure;
using System.Xml.Linq;

namespace QuestPdfUtil
{
    public static class QuestPdfHelper
    {
        public static void AppendHtml(this IContainer container, XElement element, Dictionary<string, Action<IContainer, string>> actions)
        {
            foreach (var child in element.Elements())
            {
                if (child.HasElements)
                {
                    AppendHtml(container, child, actions);
                }
                else
                {
                    if (actions.TryGetValue(child.Name.ToString(), out var action))
                    {
                        action.Invoke(container, child.Value);
                    }
                }
            }
        }
    }
}
