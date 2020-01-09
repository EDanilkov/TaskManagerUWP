using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicModule.Interfaces
{
    public interface ICommentRepository
    {
        Task<NewResponseModel> AddComment(Comment comment);

        Task<List<Comment>> GetComment(int tasktId);
    }
}
