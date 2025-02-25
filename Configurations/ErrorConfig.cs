namespace SecureApiWithJwt.Configurations
{
    public class ErrorConfig
    {
        public static Dictionary<string, string[]> Msg(string key, string value)
        {
            return new Dictionary<string, string[]>
            {
                [key] = new[] { value }
            };
        }
    }
}
