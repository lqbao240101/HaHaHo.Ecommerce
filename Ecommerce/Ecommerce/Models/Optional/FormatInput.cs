using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Ecommerce.Models.Optional
{
    public enum SortBy
    {
        Name,
        Price
    }
    public class Filter
    {
        public string SearchText { get; set; }
        public Guid CategoryId { get; set; }
    }
    public class Sort
    {
        public SortBy SortBy { get; set; }
        public bool IsAscending { get; set; }
    }
    public class Page
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
    public class FormatInput
    {
        public string? Method { get; set; }
        public string? Data { get; set; } = null;
        public string? Filter { get; set; } = null;
        public string? Sort { get; set; } = null;
        public string? Page { get; set; } = null;
    }
}
