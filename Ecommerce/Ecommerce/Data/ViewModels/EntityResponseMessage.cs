namespace Ecommerce.Data.ViewModels
{
    public class EntityResponseMessage
    {
        public string? Message { get; set; }
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
    }
}
