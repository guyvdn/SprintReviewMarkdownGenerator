using Microsoft.Extensions.Options;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;

namespace SprintReviewMarkdownGenerator.WorkItems
{
    public class WorkItemMapper
    {
        private readonly AppSettings _appSettings;

        public WorkItemMapper(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public WorkItemDetail MapWorkItem(WorkItem item)
        {
            return new WorkItemDetail
            {
                Id = item.Id,
                Url =  $"{_appSettings.WorkItemBaseUrl.TrimEnd('/')}/{item.Id}",
                State = (string)item.Fields["System.State"],
                Title = (string)item.Fields["System.Title"],
                BoardLane = (string)item.Fields.GetValueOrDefault("System.BoardLane", "Other")
            };
        }
    }
}