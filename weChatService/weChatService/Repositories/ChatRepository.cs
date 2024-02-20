using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using weChatService.Repositories.Interface;

namespace weChatService.Repositories
{
    public class ChatRepository:IChatRepository
    {
        public Task<List<object>> GetAllMessages()
        {
            // Implement logic to retrieve all messages
            return null;
        }

        public Task AddMessage(object message)
        {
            // Implement logic to add a message
            return null;
        }
    }
}
