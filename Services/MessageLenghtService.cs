namespace Module11.Bot.Services
{
    internal class MessageLenghtService
    {
        internal string MessageLenght(string s)
        {
            try
            {
                return $"Сообщение состоит из {s.Length} символа(-ов)";
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
                return $"{e.Message}";
            }
        }
    }
}
