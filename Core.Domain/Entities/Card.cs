namespace Core.Domain.Entities
{
    /// <summary>
    /// Сущность карточки.
    /// </summary>
    public class Card // TODO: Добавить все поля из API. Хранить номер сета и карты для получения картинки
    {
        /// <summary>
        /// Идентификатор карты в приложении.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Сила удара.
        /// </summary>
        public int? Attack { get; init; }

        /// <summary>
        /// Первый класс карты.
        /// </summary>
        public string? FirstClass { get; set; }

        /// <summary>
        /// Второй класс карты.
        /// </summary>
        public string? SecondClass { get; set; }

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
        /// Текст эрраты.
        /// </summary>
        public string? Errata { get; set; }

        /// <summary>
        /// Id карты на стороне API.
        /// </summary>
        public int ExternalId { get; init; }

        /// <summary>
        /// Актуальность для стандартных форматов.
        /// </summary>
        public bool IsActual { get; set; }

        /// <summary>
        /// Фольгированная.
        /// </summary>
        public bool IsFoil { get; init; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Название для поиска (без лишних символов, приведенное в нижнему регистру).
        /// Костыль, так как применение ToLowerInvariant не транслируется в SQL, а ToLower не работает.
        /// </summary>
        public string NameForSearching { get; init; }

        /// <summary>
        /// Порядковый номер в сете.
        /// </summary>
        public int Number { get; init; }

        /// <summary>
        /// Художник.
        /// </summary>
        public string? Painter { get; set; }

        /// <summary>
        /// Редкость.
        /// </summary>
        public string Rarity { get; init; }

        /// <summary>
        /// Выпуск.
        /// </summary>
        public string SetName { get; init; }

        /// <summary>
        /// Номер выпуска.
        /// </summary>
        public int SetNumber { get; init; }

        /// <summary>
        /// Текст.
        /// </summary>
        public string? Text { get; init; }

        /// <summary>
        /// Тип.
        /// </summary>
        public string Type { get; init; }

        /// <summary>
        /// Иллюстрация (regular, pf, "")
        /// </summary>
        public string Variant { get; init; }
    }
}
