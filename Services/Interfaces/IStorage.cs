using Module11.Bot.Models;

namespace Module11.Bot.Services.Interfaces
{
    internal interface IStorage
    {
        internal Session GetSession(long id);
    }
}
