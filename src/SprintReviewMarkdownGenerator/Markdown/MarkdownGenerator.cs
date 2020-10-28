using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SprintReviewMarkdownGenerator.WorkItems;

namespace SprintReviewMarkdownGenerator.Markdown
{
    public class MarkdownGenerator
    {
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        public string Generate()
        {
            return _stringBuilder.ToString();
        }

        /// <summary>
        /// Add https://marp.app/ header
        /// </summary>
        /// <param name="theme"></param>
        /// <param name="class"></param>
        public MarkdownGenerator WithMarpHeader(string theme = "default", string @class = "invert")
        {
            _stringBuilder.AppendLine("---");
            _stringBuilder.AppendLine("marp: true");
            _stringBuilder.AppendLine($"theme: {theme}");
            _stringBuilder.AppendLine($"class: {@class}");
            _stringBuilder.AppendLine("---");
            _stringBuilder.AppendLine();

            return this;
        }

        public MarkdownGenerator WithTitle(string title, DateTime date)
        {
            _stringBuilder.AppendLine("<style scoped>");
            _stringBuilder.AppendLine("{");
            _stringBuilder.AppendLine("    text-align: center;");
            _stringBuilder.AppendLine("}");
            _stringBuilder.AppendLine("</style>");

            _stringBuilder.AppendLine($"# **{title}**");
            _stringBuilder.AppendLine($"#### {date:yyyy-MM-dd}");
            _stringBuilder.AppendBreak();

            return this;
        }

        public MarkdownGenerator WithAgenda(IEnumerable<IGrouping<string, WorkItemDetail>> groupedItems, params string[] additionalPages)
        {
            _stringBuilder.AppendTitle("Agenda");
            _stringBuilder.AppendBullet("Progress");

            foreach (var group in groupedItems)
            {
                _stringBuilder.AppendLine($"    * {group.Key}");
            }

            foreach (var additionalPage in additionalPages)
            {
                _stringBuilder.AppendBullet(additionalPage);
            }

            _stringBuilder.AppendBreak();

            return this;
        }

        public MarkdownGenerator WithWorkItems(IEnumerable<IGrouping<string, WorkItemDetail>> groupedItems)
        {
            foreach (var group in groupedItems)
            {
                _stringBuilder.AppendTitle(group.Key);
                AppendWorkItems(group);
                _stringBuilder.AppendBreak();
            }

            return this;
        }

        public MarkdownGenerator WithWorkItemsByStatus(IEnumerable<IGrouping<string, WorkItemDetail>> groupedItems, string activeState = "Active", string completedState = "Resolved")
        {
            foreach (var group in groupedItems)
            {
                _stringBuilder.AppendTitle(group.Key);

                var completedItems = group.Where(x => x.State == completedState).ToList();
                if (completedItems.Any())
                {
                    _stringBuilder.AppendItalicLine("Done");
                    AppendWorkItems(completedItems);
                    _stringBuilder.AppendLine();
                }

                var activeItems = group.Where(x => x.State == activeState).ToList();
                if (activeItems.Any())
                {
                    _stringBuilder.AppendItalicLine("In Progress");
                    AppendWorkItems(activeItems);
                    _stringBuilder.AppendLine();
                }

                _stringBuilder.AppendBreak();
            }

            return this;
        }

        private void AppendWorkItems(IEnumerable<WorkItemDetail> completedItems)
        {
            foreach (var workItem in completedItems)
            {
                _stringBuilder.AppendBulletWithLink($"{workItem.Id}. {workItem.Title}", workItem.Url);
            }
        }

        public MarkdownGenerator WithLinkPage(string pageTitle, string linkTitle, string uri)
        {
            _stringBuilder.AppendTitle(pageTitle);
            _stringBuilder.AppendLink(linkTitle, uri);
            _stringBuilder.AppendBreak();

            return this;
        }
    }
}