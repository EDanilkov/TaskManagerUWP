using ServerAPI.Data.Models;
using System;

namespace ServerAPI.Data.ResponseModel
{
    public class UpdateTaskModel
    {
        public Task Task { get; set; }

        public string TaskName { get; set; }

        public string TaskDescription { get; set; }

        public int UserId { get; set; }

        public int StatusId { get; set; }

        public DateTime TaskFinishDate { get; set; }

    }
}
