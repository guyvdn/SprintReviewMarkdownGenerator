using System.Collections.Generic;
using System.Linq;
using SprintReviewMarkdownGenerator.WorkItems.Grouping.Abstractions;

namespace SprintReviewMarkdownGenerator.WorkItems.Grouping
{
    public class GroupWorkItemsByBoardLane: IGroupWorkItems
    {
        public IEnumerable<IGrouping<string, WorkItemDetail>> GroupWorkItems(IEnumerable<WorkItemDetail> workItems)
        {
            return workItems
                .GroupBy(x => x.BoardLane)
                .OrderByDescending(x => x.Count());
        }
    }
}