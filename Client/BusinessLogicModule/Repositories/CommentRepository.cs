using BusinessLogicModule.Interfaces;
using BusinessLogicModule.Services;
using Newtonsoft.Json;
using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicModule.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        public async Task<NewResponseModel> AddComment(Comment comment)
        {
            try
            {

                string json = JsonConvert.SerializeObject(comment);
                return await RequestService.Post("https://localhost:44393/api/comments/new", json);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Comment>> GetComment(int tasktId)
        {
            try
            {
                return await RequestService.Get<List<Comment>>("https://localhost:44393/api/comments/" + tasktId.ToString());
            }
            catch
            {
                throw;
            }
        }
    }
}
