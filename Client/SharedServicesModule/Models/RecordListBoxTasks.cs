using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace SharedServicesModule.Models
{
    public class RecordListBoxTasks
    {
        public RecordListBoxTasks()
        {
        }

        public int TaskId { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public string TaskName { get; set; }
        public string UserName { get; set; }
        public string ProjectName { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public string Status { get; set; }
        public Brush Foreground { get; set; }

        public RecordListBoxTasks(int idTask, int idUser, int idProject, string userName, string projectName,  string taskName, string startDate, string finishDate, string status, Brush foreground)
        {
            TaskId = idTask;
            UserId = idUser;
            ProjectId = idProject;
            ProjectName = projectName;
            StartDate = startDate;
            FinishDate = finishDate;
            TaskName = taskName;
            UserName = userName;
            Status = status;
            Foreground = foreground;
        }
    }
}
