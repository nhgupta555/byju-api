namespace ByjuAPI.Models
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object ReturnValue { get; set; }
        public string AuthToken  { get; set; }
    }
}