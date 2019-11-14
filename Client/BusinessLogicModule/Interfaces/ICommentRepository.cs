using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicModule.Interfaces
{
    public interface ICommentRepository
    {
        Task<NewResponseModel> AddComment(Comment comment);
        Task<List<Comment>> GetComment(int tasktId);
    }
}
