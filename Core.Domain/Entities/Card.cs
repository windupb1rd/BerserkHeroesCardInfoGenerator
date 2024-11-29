namespace Core.Domain.Entities
{
    public class Card
    {
        /// <summary>
        /// Сила удара.
        /// </summary>
        public int? Attack { get; init; }

        /// <summary>
        /// Классы.
        /// </summary>
        public string CardClasses { get; init; }

        /// <summary>
        /// Стоимость розыгрыша.
        /// </summary>
        public int? Cost { get; init; }

        /// <summary>
        /// Стихии.
        /// </summary>
        public string Elements { get; init; }

        /// <summary>
        /// Здоровье.
        /// </summary>
        public int? Health { get; init; }

        /// <summary>
        /// Id карты на стороне API.
        /// </summary>
        public int ExternalId { get; init; }

        /// <summary>
        /// Фольгированная.
        /// </summary>
        public bool IsFoil { get; init; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Порядковый номер в сете.
        /// </summary>
        public int Number { get; init; }

        /// <summary>
        /// Редкость.
        /// </summary>
        public string Rarity { get; init; }

        /// <summary>
        /// Выпуск.
        /// </summary>
        public string Set { get; init; }

        /// <summary>
        /// Текст.
        /// </summary>
        public string Text { get; init; }

        /// <summary>
        /// Тип.
        /// </summary>
        public string Type { get; init; }

        /// <summary>
        /// Иллюстрация (regular, pf, ...)
        /// </summary>
        public string Variant { get; init; }
    }
}
