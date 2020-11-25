using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SprintReviewMarkdownGenerator.Markdown.Abstractions;
using SprintReviewMarkdownGenerator.Markdown.Writers;
using SprintReviewMarkdownGenerator.WorkItems;

namespace SprintReviewMarkdownGenerator
{
    public class GeneratorService
    {
        private readonly WorkItemService _workItemService;
        private readonly IMarkdownGenerator _markdownGenerator;
        private readonly MarkdownWriter _markdownWriter;
        private readonly AppSettings _appSettings;

        public GeneratorService(WorkItemService workItemService, IMarkdownGenerator markdownGenerator, MarkdownWriter markdownWriter, IOptions<AppSettings> appSettings)
        {
            _workItemService = workItemService;
            _markdownGenerator = markdownGenerator;
            _markdownWriter = markdownWriter;
            _appSettings = appSettings.Value;
        }

        public async Task Run()
        {
            var workItems = (await _workItemService.GetGroupedWorkItems()).ToList();

            var markDown = _markdownGenerator
                .Initialize("black")
                .WithTitle("Sprint Review", DateTime.Today)
                .WithAgenda(workItems, "Team Availability", "What's next")
                .WithWorkItemsByStatus(workItems)
                .WithLinkPage("Team Availability", "Team calendar", _appSettings.TeamCalendarUrl)
                .WithLinkPage("What's next", "Backlog", _appSettings.TeamCalendarUrl)
                .Generate();

            await _markdownWriter.WriteToFile($"{DateTime.Today:yyyy-MM-dd} Sprint Review.md", markDown);
        }
    }
}