using System.Text.Json;

namespace Ecommerce.Models.Optional
{
    public class FormatResponse
    {
        public List<JsonDocument> Data { get; set; }
        public int TotalItemCount { get; set; }
        public bool HasMoreRows { get; set; }
    }
}
