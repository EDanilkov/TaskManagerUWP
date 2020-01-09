﻿using BusinessLogicModule.Interfaces;
using BusinessLogicModule.Services;
using Newtonsoft.Json;
using SharedServicesModule;
using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System.Collections.Generic;
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
                return await RequestService.Post(Consts.BaseAddress + "api/comments/new", json);
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
                return await RequestService.Get<List<Comment>>(Consts.BaseAddress + "api/comments/" + tasktId.ToString());
            }
            catch
            {
                throw;
            }
        }
    }
}
