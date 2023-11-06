namespace CakeOrder.Data
{
    public class Conifg
    {
        public static readonly Dictionary<CriteriaType, string> CriteriaNames = new Dictionary<CriteriaType, string>()
        {
            { CriteriaType.Shape, "Форма торта" },
            { CriteriaType.Count, "Кол-во коржей" },
            { CriteriaType.Taste, "Вкус коржей" },
            { CriteriaType.Size, "Размер торта" },
            { CriteriaType.Glaze, "Глазурь торта" },
        };

        public static string GetName(CriteriaType criteriaType)
        {
            CriteriaNames.TryGetValue(criteriaType, out var name);
            return name is null ? "undefined" : name;
        }

        private static readonly Dictionary<CriteriaType, CriteriaData[]> _criteries = new Dictionary<CriteriaType, CriteriaData[]>()
        {
            { CriteriaType.Shape, new CriteriaData[] { new CriteriaData("Круг", 200), new CriteriaData("Квадрат", 250), new CriteriaData("Сердце", 300), new CriteriaData("Куб", 500) } },
            { CriteriaType.Count, new CriteriaData[] { new CriteriaData("Одноярусный", 150), new CriteriaData("Двухрусный", 250), new CriteriaData("Трехярусный", 350), new CriteriaData("Четырехярусный", 450) } },
            { CriteriaType.Taste, new CriteriaData[] { new CriteriaData("Ванильный", 175), new CriteriaData("Фисташковый", 300), new CriteriaData("Шоколадный", 300), new CriteriaData("Маковый", 420) } },
            { CriteriaType.Size, new CriteriaData[] { new CriteriaData("Маленький", 200), new CriteriaData("Средний", 300), new CriteriaData("Большой", 400), new CriteriaData("Очень большой", 500) } },
            { CriteriaType.Glaze, new CriteriaData[] { new CriteriaData("Малиновая", 150), new CriteriaData("Шоколадная", 200), new CriteriaData("Ягодная", 300), new CriteriaData("Медовая", 400) } },
        };

        public static bool GetCriteries(CriteriaType criteriaType, out CriteriaData[]? criteriaDatas)
        {
            return _criteries.TryGetValue((CriteriaType)criteriaType, out criteriaDatas);
        }

        public static CriteriaType GetCriteriaTypeByIndex(int index)
        {
            var types = _criteries.Keys.ToList();
            return types.ElementAt(index);
        }
    }

    public class CriteriaData
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public CriteriaData(string name, int price)
        {
            Name = name;
            Price = price;
        }
    }

    public enum CriteriaType
    {
        None,
        Shape,
        Count,
        Taste,
        Size,
        Glaze
    }
}
