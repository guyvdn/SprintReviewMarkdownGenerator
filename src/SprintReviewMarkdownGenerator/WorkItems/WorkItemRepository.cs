using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

namespace SprintReviewMarkdownGenerator.WorkItems
{
    public sealed class WorkItemRepository : IDisposable
    {
        private readonly AppSettings _appSettings;
        private readonly SemaphoreSlim _semaphoreSlim;

        public WorkItemRepository(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _semaphoreSlim = new SemaphoreSlim(initialCount: 4);
        }

        public async Task<IEnumerable<WorkItem>> GetAll()
        {
            var connection = new VssConnection(new Uri(_appSettings.VssUri), new VssCredentials(new VssBasicCredential("PAT", _appSettings.PersonalAccessToken)));
            var witClient = connection.GetClient<WorkItemTrackingHttpClient>();
            var query = await witClient.GetQueryAsync(_appSettings.WorkItemsProject, _appSettings.WorkItemsQuery);
            var queryResult = await witClient.QueryByIdAsync(query.Id);

            return await GetWorkItemDetails(witClient, queryResult);
        }

        private async Task<IEnumerable<WorkItem>> GetWorkItemDetails(WorkItemTrackingHttpClient witClient, WorkItemQueryResult queryResult)
        {
            var result = new ConcurrentBag<WorkItem>();

            var tasks = queryResult.WorkItems.Select(async workItem =>
            {
                await _semaphoreSlim.WaitAsync();
                result.Add(await witClient.GetWorkItemAsync(workItem.Id));
                _semaphoreSlim.Release();
            });

            await Task.WhenAll(tasks);
            return result;
        }

        public void Dispose()
        {
            _semaphoreSlim?.Dispose();
        }
    }
}