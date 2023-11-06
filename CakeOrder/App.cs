using System.Text;
using CakeOrder.Data;
using CakeOrder.Pages;
using CakeOrder.Classes;

namespace CakeOrder
{
    public class App
    {
        public static CriteriaType SelectedCriteria = CriteriaType.None;
        public static Dictionary<CriteriaType, CriteriaData> CurrentSettingsOfCriteriesCake = new Dictionary<CriteriaType, CriteriaData>();
        public static StreamWriter StreamWriter = new StreamWriter("G:\\cakes.txt", true, Encoding.UTF8);

        public static Dictionary<string, PageConstructor> Pages = new Dictionary<string, PageConstructor>();

        public static void Main() 
        {
            try
            {
                var pages = typeof(PageConstructor).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(PageConstructor))).ToList();
                pages.ForEach(page =>
                {
                    var instance = Activator.CreateInstance(page);
                    if (instance != null)
                        Pages.TryAdd(page.Name, (PageConstructor)instance);
                });

                SetCriteria(0);
                SetPage(nameof(MainMenu), 0);
            }
            catch(Exception ex) { Console.WriteLine("Main: " + ex.ToString()); }
        }

        public static void SetCriteria(int index) => SelectedCriteria = Conifg.GetCriteriaTypeByIndex(index);

        public static void SetPage(string pageName, int cursorPosition)
        {
            try
            {
                if (!Pages.TryGetValue(pageName, out var instance)) throw new Exception($"[ERROR] Страница \"{pageName}\" - не найдена");
                instance?.Open(cursorPosition);
            }
            catch(Exception ex) { Console.WriteLine("SetPage: " + ex.ToString()); }
        }

        public static int CalculateTotalPrice()
        {
            int totalPrice = 0;
            foreach(var item in CurrentSettingsOfCriteriesCake.Values)
                totalPrice += item.Price;

            return totalPrice;
        }

        public static string FormatPrice(int price) => string.Format("{0:n0}", price);
    }
}