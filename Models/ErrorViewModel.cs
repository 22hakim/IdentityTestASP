namespace RunWepApp_withIdentity_TeddySmith_Youtube.Models
{
    public class ErrorViewModel
    {
        public ErrorViewModel(string message)
        {
            Message = message;
        }
        //public string? RequestId { get; set; }

        //public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string? Message { get; set; } = null;

    }
}