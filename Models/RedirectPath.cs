namespace short_url.Models
{
    public class RedirectPath
    {
        public long Id { get; set; }
        public string path { get; set; }
        public string destination { get; set; }
    }
}