namespace Infrastructure.WebApiClient.Models
{
    /// <summary>
    /// Модель карты.
    /// </summary>
    public class Content
    {
        /// <summary>
        /// Сила удара.
        /// </summary>
        public int? Attack { get; set; }

        /// <summary>
        /// Классы.
        /// </summary>
        public IEnumerable<string> CardClasses { get; set; }

        /// <summary>
        /// Стоимость розыгрыша.
        /// </summary>
        public int? Cost { get; set; }

        /// <summary>
        /// Стихии.
        /// </summary>
        public IEnumerable<string> Elements { get; set; }

        /// <summary>
        /// Текст эрраты.
        /// </summary>
        public string? Errata { get; set; }

        /// <summary>
        /// Класс карты, идущий первым в массиве <see cref="CardClasses"/>
        /// </summary>
        public string? FirstClass { get; set; }

        /// <summary>
        /// Здоровье.
        /// </summary>
        public int? Health { get; set; }

        /// <summary>
        /// Id карты на стороне API.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Актуальность для стандартных форматов.
        /// </summary>
        public bool IsActual { get; set; }

        /// <summary>
        /// Фольгированная.
        /// </summary>
        public bool IsFoil { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Порядковый номер в сете.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Художник.
        /// </summary>
        public string Painter { get; set; }

        /// <summary>
        /// Редкость.
        /// </summary>
        public string Rarity { get; set; }

        /// <summary>
        /// Класс карты, идущий вторым в массиве <see cref="CardClasses"/>
        /// </summary>
        public string? SecondClass { get; set; }

        /// <summary>
        /// Выпуск.
        /// </summary>
        public string Set { get; set; }

        /// <summary>
        /// Информация о выпуске.
        /// </summary>
        public SetInfo SetInfo { get; set; }

        /// <summary>
        /// Текст.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Тип.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Иллюстрация (regular, pf, ...)
        /// </summary>
        public string Variant { get; set; }
    }
}