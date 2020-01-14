namespace SharedServicesModule
{
    public static class Consts
    {
        public static double Width { get; set; }
        public static double Height { get; set; }
        public static string UserName { get; set; }
        public static int ProjectId { get; set; }
        public static int TaskId { get; set; }
        public static int UserId { get; set; }
        
        public const string BaseAddress = "https://192.168.100.7:44393/";
        public const string Success = "#64DD17";
        public const string Error = "#d50000";
    }
}
