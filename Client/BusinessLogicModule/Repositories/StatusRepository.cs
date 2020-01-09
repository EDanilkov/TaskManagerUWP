using BusinessLogicModule.Interfaces;
using BusinessLogicModule.Services;
using Newtonsoft.Json;
using SharedServicesModule;
using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicModule.Repositories
{
    public class StatusRepository : IStatusRepository
    {

        public async Task<NewResponseModel> AddStatus(Status status)
        {
            try
            {
                string json = JsonConvert.SerializeObject(status);
                return await RequestService.Post(Consts.BaseAddress + "api/statuses/new", json);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Status>> GetStatuses()
        {
            try
            {
                return await RequestService.Get<List<Status>>(Consts.BaseAddress + "api/statuses/all");
            }
            catch
            {
                throw;
            }
        }

        public async Task<Status> GetStatus(string name = null, int? id = 0)
        {
            try
            {
                return await RequestService.Get<Status>(Consts.BaseAddress + "api/statuses?name=" + name + "&id=" + id);
            }
            catch
            {
                throw;
            }
        }
    }
}
