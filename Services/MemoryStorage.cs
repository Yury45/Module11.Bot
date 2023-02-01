using System.Collections.Concurrent;
using Module11.Bot.Models;
using Module11.Bot.Services.Interfaces;

namespace Module11.Bot.Services
{
    internal  class MemoryStorage : IStorage
    {
        private readonly ConcurrentDictionary<long, Session> _sessions;

        public  MemoryStorage()
        {
            _sessions = new ConcurrentDictionary<long, Session>();
        }

        Session IStorage.GetSession(long id)
        {
            if(_sessions.ContainsKey(id)) return _sessions[id];

            var newSession = new Session()
            {
                Position = "Default"
            };

            _sessions.TryAdd(id, newSession);
            return newSession;
        }
    }
}
