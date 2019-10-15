using SharedServicesModule.Models;
using System;

namespace SharedServicesModule.ResponseModel
{
    public class UpdateTaskModel
    {
        public Task Task { get; set; }

        public string TaskName { get; set; }

        public string TaskDescription { get; set; }

        public int UserId { get; set; }

        public DateTime TaskFinishDate { get; set; }

    }
}
