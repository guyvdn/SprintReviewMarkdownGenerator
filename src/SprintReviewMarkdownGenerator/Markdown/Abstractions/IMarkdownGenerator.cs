using System;
using System.Collections.Generic;
using System.Linq;
using SprintReviewMarkdownGenerator.WorkItems;

namespace SprintReviewMarkdownGenerator.Markdown.Abstractions
{
    public interface IMarkdownGenerator
    {
        string Generate();
        IMarkdownGenerator Initialize(string theme);
        IMarkdownGenerator WithTitle(string title, DateTime date);
        IMarkdownGenerator WithAgenda(IEnumerable<IGrouping<string, WorkItemDetail>> groupedItems, params string[] additionalPages);
        IMarkdownGenerator WithWorkItemsByStatus(IEnumerable<IGrouping<string, WorkItemDetail>> groupedItems, string activeState = "Active", string completedState = "Resolved");
        IMarkdownGenerator WithLinkPage(string pageTitle, string linkTitle, string uri);
    }
}