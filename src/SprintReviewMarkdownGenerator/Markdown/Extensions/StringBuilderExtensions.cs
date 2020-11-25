using System.Text;

namespace SprintReviewMarkdownGenerator.Markdown.Extensions
{
    public static class StringBuilderExtensions
    {
        internal static void AppendBreak(this StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("---");
            stringBuilder.AppendLine();
        }

        internal static void AppendTitle(this StringBuilder stringBuilder, string title)
        {
            stringBuilder.AppendLine($"### {title}");
        }

        internal static void AppendLink(this StringBuilder stringBuilder, string title, string url)
        {
            stringBuilder.AppendLine($"[{title}]({url})");
        }
        
        internal static void AppendBullet(this StringBuilder stringBuilder, string value)
        {
            stringBuilder.AppendLine($"* {value}");
        }

        internal static void AppendBulletWithLink(this StringBuilder stringBuilder, string title, string url)
        {
            stringBuilder.AppendLine($"* [{title}]({url})");
        }

        internal static void AppendItalicLine(this StringBuilder stringBuilder, string value)
        {
            stringBuilder.AppendLine($"*{value}*");
        }

        internal static void AppendBoldLine(this StringBuilder stringBuilder, string value)
        {
            stringBuilder.AppendLine($"**{value}**");
        }
        internal static void StartUnorderedList(this StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("<ul>");
        }

        internal static void EndUnorderedList(this StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("</ul>");
        }
        internal static void AddFragmentListItem(this StringBuilder stringBuilder, string value)
        {
            stringBuilder.AppendLine($"<li class=\"fragment fade-in-then-semi-out\">{value}</li>");
        }

        internal static void AddFragmentListItemWithLink(this StringBuilder stringBuilder, string title, string url)
        {
            stringBuilder.AppendLine($"<li class=\"fragment fade-in-then-semi-out\"><a href=\"{url}\" target=\"_blank\">{title}</a></li>");
        }

        internal static void AppendFragmentLine(this StringBuilder stringBuilder, string value)
        {
            stringBuilder.AppendLine($"{value} <!-- .element class=\"fragment fade-in-then-semi-out\" -->");
        }
    }
}