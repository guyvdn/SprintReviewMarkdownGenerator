using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SprintReviewMarkdownGenerator.WorkItems.Grouping.Abstractions;

namespace SprintReviewMarkdownGenerator.WorkItems
{
    public class WorkItemService
    {
        private readonly WorkItemRepository _workItemRepository;
        private readonly WorkItemMapper _workItemMapper;
        private readonly IGroupWorkItems _groupWorkItems;

        public WorkItemService(WorkItemRepository workItemRepository, WorkItemMapper workItemMapper, IGroupWorkItems groupWorkItems)
        {
            _workItemRepository = workItemRepository;
            _workItemMapper = workItemMapper;
            _groupWorkItems = groupWorkItems;
        }

        public async Task<IEnumerable<WorkItemDetail>> GetWorkItems()
        {
            var workItems = await _workItemRepository.GetAll();
            return workItems.Select(_workItemMapper.MapWorkItem);
        }

        public async Task<IEnumerable<IGrouping<string, WorkItemDetail>>> GetGroupedWorkItems()
        {
            var workItems = await GetWorkItems();
            return _groupWorkItems.GroupWorkItems(workItems);
        }
    }
}