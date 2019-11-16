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
        public int ProjectId { get; set; }
        public string TaskName { get; set; }
        public string ProjectName { get; set; }
        public string FinishDate { get; set; }
        public string Status { get; set; }
        public Brush Foreground { get; set; }

        public RecordListBoxTasks(int idTask, int idProject, string projectName,  string taskName, string finishDate, string status, Brush foreground)
        {
            TaskId = idTask;
            ProjectId = idProject;
            ProjectName = projectName;
            FinishDate = finishDate;
            TaskName = taskName;
            Status = status;
            Foreground = foreground;
        }
    }
}
