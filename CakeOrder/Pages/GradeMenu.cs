using CakeOrder.Classes;

namespace CakeOrder.Pages
{
    public class GradeMenu : PageConstructor
    {
        public override void Open(int currentPosition)
        {
            try
            {
                Console.Clear();

                WriteLine("Спасибо за ваш заказ!");
                WriteLine("Мы очень рады, что вы выбрали именно нас");
                WriteLine("--------------------------------------------------");
                WriteLine("Оцените качество нашего приложения от 5 до 10");

                int star = Convert.ToInt32(Console.ReadLine());

                App.StreamWriter.WriteLine($"Оценка качества: {star}");
                App.StreamWriter.Close();

                Environment.Exit(0);
            }
            catch(Exception ex) { Console.WriteLine("Open: " + ex.ToString()); }
        }
    }
}
