namespace Infrastructure.Vk.Options
{
    /// <summary>
    /// Настройки приложения ВК.
    /// </summary>
    public class VkApplicationClientOptions
    {
        /// <summary>
        /// Токен приложения ВК (хранится в secrets.json).
        /// </summary>
        public string Token { get; init; }

        /// <summary>
        /// ID зарегистрированного приложения ВК.
        /// </summary>
        public ulong ApplicationId { get; init; }
    }
}
