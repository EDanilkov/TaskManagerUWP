namespace SharedServicesModule.Models
{
    public class NavigateArgs
    {
        public NavigateArgs()
        {

        }

        public NavigateArgs(string url)
        {
            Url = url;
        }

        public string Url { get; set; }
    }
}

