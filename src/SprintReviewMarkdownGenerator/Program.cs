using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SprintReviewMarkdownGenerator.WorkItems;
using SprintReviewMarkdownGenerator.WorkItems.Grouping;
using SprintReviewMarkdownGenerator.WorkItems.Grouping.Abstractions;
using System.IO;
using System.Threading.Tasks;
using FluentOptionsValidator;
using SprintReviewMarkdownGenerator.Markdown;

namespace SprintReviewMarkdownGenerator
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .AddUserSecrets<Program>(true)
                .Build();

            var serviceProvider = new ServiceCollection()
                .Configure<AppSettings>(appSettings => configuration.Bind(appSettings))
                .AddSingleton<WorkItemRepository>()
                .AddSingleton<WorkItemService>()
                .AddSingleton<WorkItemMapper>()
                .AddSingleton<MarkdownGenerator>()
                .AddSingleton<MarkdownWriter>()
                .AddSingleton<IGroupWorkItems, GroupWorkItemsByBoardLane>()
                .AddSingleton<GeneratorService>()
                .RegisterFluentOptionsValidators<Program>()
                .BuildServiceProvider();

            await serviceProvider.GetRequiredService<GeneratorService>().Run();
        }
    }
}