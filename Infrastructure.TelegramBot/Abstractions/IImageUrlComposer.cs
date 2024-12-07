namespace Infrastructure.TelegramBot.Abstractions
{
    /// <summary>
    /// Абстракция сервиса, подготавливающего url для скачивания изображения карты.
    /// </summary>
    public interface IImageUrlComposer
    {
        /// <summary>
        /// Создает url изображения по имени карты.
        /// </summary>
        /// <returns></returns>
        string ComposeByCardName(string cardName);
    }
}
