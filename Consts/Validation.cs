
namespace short_url.Consts
{
    public static class ValidationConst
    {
        public static string pathPattern = @"^(\/([a-zA-Z0-9])+)+";
        public static string[] reservedHostsPatterns = { "localhost.*", @"10\..*", @"172\..*", @"192.168\..*" };
        public static string denyAllPattern = @"!.*";
    }
}
