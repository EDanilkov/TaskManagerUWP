using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicModule.Interfaces
{
    public interface ICommentRepository
    {
        /// <summary>
        /// Saves info about provided comment to the database.
        /// </summary>
        /// <param name="comment">The object to be added to the Comment</param>
        /// <returns>Retrieves response to the query</returns>
        Task<NewResponseModel> AddComment(Comment comment);

        /// <summary>
        /// Returns all comments with provided tasktId.
        /// </summary>
        /// <param name="tasktId"></param>
        /// <returns>A  List&lt;Comment> containing all the elements that match the conditions defined by the specified parameter if found; 
        /// otherwise, an empty  List&lt;Comment>.
        /// </returns>
        Task<List<Comment>> GetComment(int tasktId);
    }
}
