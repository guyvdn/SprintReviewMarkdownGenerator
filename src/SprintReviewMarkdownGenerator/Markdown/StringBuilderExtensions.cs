using System.Text;

namespace SprintReviewMarkdownGenerator.Markdown
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
    }
}