using CakeOrder.Data;
using CakeOrder.Classes;

namespace CakeOrder.Pages
{
    public class MainMenu : PageConstructor
    {
        public override void Open(int currentPosition)
        {
            try
            {
                int lastPosition = currentPosition;

                Console.Clear();

                WriteLine("----------------------------------------");
                WriteLine("Торты на любой вкус и праздник: ваш идеальный выбор ждет вас здесь!");
                WriteLine("Кастомизация торта:");
                WriteLine("----------------------------------------");

                var criteriaNames = Conifg.CriteriaNames;
                foreach (var item in criteriaNames)
                {
                    string writeText = item.Value;
                    if (App.CurrentSettingsOfCriteriesCake.ContainsKey(item.Key))
                        writeText += " (готово)";

                    if (criteriaNames.Keys.ToList().IndexOf(item.Key) == currentPosition)
                    {
                        writeText = $"-> " + writeText;
                        WriteLine(writeText);
                    }
                    else
                        WriteLine(writeText, 3);
                }
                int totalPrice = App.CalculateTotalPrice();
                WriteLine($"\nИтоговая стоимость: {App.FormatPrice(totalPrice)}$");

                WriteLine($"Ваш торт:");
                foreach (var item in App.CurrentSettingsOfCriteriesCake)
                    WriteLine($"- {Conifg.GetName(item.Key)}: {item.Value.Name} - {App.FormatPrice(item.Value.Price)}$", 1);

                WriteLine("\nВыход (Backspace)\n");

                if (currentPosition >= 0) App.SetCriteria(currentPosition);

                int maxCountOfCategories = Conifg.CriteriaNames.Count;
                while (true)
                {
                    ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                    switch (consoleKeyInfo.Key)
                    {
                        case ConsoleKey.DownArrow:
                            if (currentPosition < maxCountOfCategories - 1)
                            {
                                currentPosition++;
                                App.SetCriteria(currentPosition);
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            if (currentPosition > 0)
                            {
                                currentPosition--;
                                App.SetCriteria(currentPosition);
                            }
                            break;
                        case ConsoleKey.Enter:
                            App.SetPage(nameof(CriteriesMenu), 0);
                            break;
                        case ConsoleKey.Backspace:
                            App.StreamWriter.WriteLine($"Дата - {DateTime.Now.ToString("M.mm.yyyy HH:mm")}");
                            foreach (var item in App.CurrentSettingsOfCriteriesCake)
                                App.StreamWriter.WriteLine($"{Conifg.GetName(item.Key)}: {item.Value.Name} - {App.FormatPrice(item.Value.Price)}$");

                            totalPrice = App.CalculateTotalPrice();
                            App.StreamWriter.WriteLine($"Итоговая стоимость: {App.FormatPrice(totalPrice)}$");

                            App.SetPage(nameof(GradeMenu), 0);
                            break;
                    }

                    if (lastPosition != currentPosition)
                        App.SetPage(nameof(MainMenu), currentPosition);
                }
            }
            catch (Exception ex) { Console.WriteLine("Open: " + ex.ToString()); }
        }
    }
}
