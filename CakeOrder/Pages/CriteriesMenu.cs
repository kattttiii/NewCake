using CakeOrder.Classes;
using CakeOrder.Data;

namespace CakeOrder.Pages
{
    public class CriteriesMenu : PageConstructor
    {
        public override void Open(int currentPosition)
        {
            try
            {
                if (!Conifg.GetCriteries(App.SelectedCriteria, out CriteriaData[]? criteries) || criteries is null) return;

                int lastPosition = currentPosition;

                Console.Clear();

                WriteLine("----------------------------------------");
                WriteLine("Для того чтобы выйти нажмите ESCAPE");
                WriteLine("Выберите пункт из меню:");
                WriteLine("----------------------------------------");

                foreach (var item in criteries)
                {
                    string writeText = $"{item.Name} - {App.FormatPrice(item.Price)}$";
                    if (Array.IndexOf(criteries, item) == currentPosition)
                    {
                        writeText = $"-> " + writeText;
                        WriteLine(writeText);
                    }
                    else
                        WriteLine(writeText, left: 3);
                }

                while (true)
                {
                    ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                    switch (consoleKeyInfo.Key)
                    {
                        case ConsoleKey.DownArrow:
                            if (currentPosition < criteries.Length - 1)
                            {
                                currentPosition++;
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            if (currentPosition > 0)
                            {
                                currentPosition--;
                            }
                            break;
                        case ConsoleKey.Enter:
                            var selectedCriteria = criteries[currentPosition];
                            if (App.CurrentSettingsOfCriteriesCake.ContainsKey(App.SelectedCriteria))
                            {
                                App.CurrentSettingsOfCriteriesCake[App.SelectedCriteria] = selectedCriteria;
                            }
                            else
                            {
                                App.CurrentSettingsOfCriteriesCake.TryAdd(App.SelectedCriteria, selectedCriteria);
                            }

                            App.SetPage(nameof(MainMenu), 0);
                            return;
                    }

                    if (lastPosition != currentPosition)
                        App.SetPage(nameof(CriteriesMenu), currentPosition);
                }
            }
            catch (Exception ex) { Console.WriteLine("Open: " + ex.ToString()); }
        }
    }
}
