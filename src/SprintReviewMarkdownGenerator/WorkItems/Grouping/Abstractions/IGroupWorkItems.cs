using System.Collections.Generic;
using System.Linq;

namespace SprintReviewMarkdownGenerator.WorkItems.Grouping.Abstractions
{
    public interface IGroupWorkItems
    {
        public IEnumerable<IGrouping<string, WorkItemDetail>> GroupWorkItems(IEnumerable<WorkItemDetail> workItems);
    }
}