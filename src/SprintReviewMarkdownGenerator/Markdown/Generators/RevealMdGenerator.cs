using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SprintReviewMarkdownGenerator.Markdown.Abstractions;
using SprintReviewMarkdownGenerator.Markdown.Extensions;
using SprintReviewMarkdownGenerator.WorkItems;

namespace SprintReviewMarkdownGenerator.Markdown.Generators
{
    public class RevealMdGenerator : IMarkdownGenerator
    {
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        public string Generate()
        {
            return _stringBuilder.ToString();
        }

        /// <summary>
        /// Add https://github.com/webpro/reveal-md header
        /// </summary>
        /// <param name="theme"></param>
        public IMarkdownGenerator Initialize(string theme)
        {
            _stringBuilder.AppendLine("---");
            _stringBuilder.AppendLine("title: Sprint Review");
            _stringBuilder.AppendLine($"theme: {theme}");
            _stringBuilder.AppendLine($"controls: true");
            _stringBuilder.AppendLine("---");
            _stringBuilder.AppendLine();

            return this;
        }

        public IMarkdownGenerator WithTitle(string title, DateTime date)
        {
            _stringBuilder.AppendLine($"# **{title}**");
            _stringBuilder.AppendLine($"#### {date:dd-MM-yyyy}");
            _stringBuilder.AppendBreak();

            return this;
        }

        public IMarkdownGenerator WithAgenda(IEnumerable<IGrouping<string, WorkItemDetail>> groupedItems, params string[] additionalPages)
        {
            _stringBuilder.AppendTitle("Agenda");
            _stringBuilder.StartUnorderedList();
            _stringBuilder.AddFragmentListItem("Progress");

            _stringBuilder.StartUnorderedList();
            foreach (var group in groupedItems)
            {
                _stringBuilder.AddFragmentListItem(group.Key);
            }
            _stringBuilder.EndUnorderedList();

            foreach (var additionalPage in additionalPages)
            {
                _stringBuilder.AddFragmentListItem(additionalPage);
            }

            _stringBuilder.EndUnorderedList();
            _stringBuilder.AppendBreak();

            return this;
        }

        public IMarkdownGenerator WithWorkItemsByStatus(IEnumerable<IGrouping<string, WorkItemDetail>> groupedItems, string activeState = "Active", string completedState = "Resolved")
        {
            foreach (var group in groupedItems)
            {
                _stringBuilder.AppendTitle(group.Key);

                var completedItems = group.Where(x => x.State == completedState).ToList();
                if (completedItems.Any())
                {
                    _stringBuilder.AppendFragmentLine("Done");
                    AppendWorkItems(completedItems);
                    _stringBuilder.AppendLine();
                }

                var activeItems = group.Where(x => x.State == activeState).ToList();
                if (activeItems.Any())
                {
                    _stringBuilder.AppendFragmentLine("In Progress");
                    AppendWorkItems(activeItems);
                    _stringBuilder.AppendLine();
                }

                _stringBuilder.AppendBreak();
            }

            return this;
        }

        private void AppendWorkItems(IEnumerable<WorkItemDetail> completedItems)
        {
            _stringBuilder.StartUnorderedList();
            foreach (var workItem in completedItems)
            {
                _stringBuilder.AddFragmentListItemWithLink($"{workItem.Id}. {workItem.Title}", workItem.Url);
            }
            _stringBuilder.EndUnorderedList();
        }

        public IMarkdownGenerator WithLinkPage(string pageTitle, string linkTitle, string uri)
        {
            _stringBuilder.AppendTitle(pageTitle);
            _stringBuilder.AppendLink(linkTitle, uri);
            _stringBuilder.AppendBreak();

            return this;
        }
    }
}