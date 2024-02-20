using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace weChatService.Repositories.Interface
{
   public interface IChatRepository
    {
        Task<List<object>> GetAllMessages();
        Task AddMessage(object message);
    }
}
