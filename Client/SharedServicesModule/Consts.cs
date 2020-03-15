namespace SharedServicesModule
{
    public static class Consts
    {
        /// <summary>
        /// Contains the current width of the main window.
        /// </summary>
        public static double Width { get; set; }
        /// <summary>
        /// Contains the current height of the main window.
        /// </summary>
        public static double Height { get; set; }
        /// <summary>
        /// Name of the user who is currently online
        /// </summary>
        public static string UserName { get; set; }
        /// <summary>
        /// Id of the project that the user went to
        /// </summary>
        public static int ProjectId { get; set; }
        /// <summary>
        /// Id of the task that the user went to
        /// </summary>
        public static int TaskId { get; set; }
        /// <summary>
        /// Id of the user who is currently online
        /// </summary>
        public static int UserId { get; set; }

        /// <summary>
        /// The Ip address that requests are sent to
        /// </summary>
        public const string BaseAddress = "https://localhost:44393/";
    }
}
