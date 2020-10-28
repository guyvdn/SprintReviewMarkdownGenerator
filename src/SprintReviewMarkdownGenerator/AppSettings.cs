using FluentOptionsValidator;
using FluentValidation;

namespace SprintReviewMarkdownGenerator
{
    public class AppSettings
    {
        public string VssUri { get; set; }
        public string PersonalAccessToken { get; set; }
        public string WorkItemsProject { get; set; }
        public string WorkItemsQuery { get; set; }
        public string WorkItemBaseUrl { get; set; }
        public string TeamCalendarUrl { get; set; }
        public string BacklogUrl { get; set; }
    }

    public class AppSettingsValidator : FluentOptionsValidator<AppSettings>
    {
        public AppSettingsValidator()
        {
            RuleFor(x => x.VssUri).NotEmpty();
            RuleFor(x => x.PersonalAccessToken).NotEmpty();
            RuleFor(x => x.WorkItemsProject).NotEmpty();
            RuleFor(x => x.WorkItemsQuery).NotEmpty();
            RuleFor(x => x.TeamCalendarUrl).NotEmpty();
            RuleFor(x => x.BacklogUrl).NotEmpty();
        }
    }
}