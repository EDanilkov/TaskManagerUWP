namespace SharedServicesModule.Models
{
    public class RecordListBox
    {
        public RecordListBox()
        {
        }

        public int Id { get; set; }
        public string ProjectName { get; set; }
        public int AdminId { get; set; }
        public string ChipRole { get; set; }

        public RecordListBox(int id, string projectName, int adminId, string chipRole)
        {
            Id = id;
            ProjectName = projectName;
            AdminId = adminId;
            ChipRole = chipRole;
        }
    }
}
