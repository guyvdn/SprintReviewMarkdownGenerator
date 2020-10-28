namespace SprintReviewMarkdownGenerator.WorkItems
{
    public sealed record WorkItemDetail
    {
        public int? Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string State { get; set; }
        public string BoardLane { get; set; }
    }
}