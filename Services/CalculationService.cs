using Module11.Bot.Services.Interfaces;

namespace Module11.Bot.Services
{
    internal class CalculationService : ICalculator
    {
        string ICalculator.Add(string message)
        {
            try
            {
                var numbers = GetNumbers(message);

                double result = new double();

                foreach (var number in numbers)
                {
                    result += number;
                }

                return $"Сумма чисел равняется {result}";
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка:{e.Message}");
                return e.Message ;
            }
        }

        List<double> GetNumbers(string message)
        {
            var items = message.Split(' ').ToList();
            return items.Select(x => Double.Parse(x)).ToList(); ;
        }
    }
}
