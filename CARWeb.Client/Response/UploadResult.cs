namespace CARWeb.Client.Response
{
    public class UploadResult
    {
        public string Message { get; set; } = string.Empty;
        public List<string> Files { get; set; } = new();
    }
}
