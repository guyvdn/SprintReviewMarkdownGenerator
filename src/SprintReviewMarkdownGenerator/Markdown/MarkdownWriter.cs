using System.IO;
using System.Threading.Tasks;

namespace SprintReviewMarkdownGenerator.Markdown
{
    public class MarkdownWriter
    {
        public Task WriteToFile(string filename, string markdown)
        {
            return File.WriteAllTextAsync(filename, markdown);
        }
    }
}