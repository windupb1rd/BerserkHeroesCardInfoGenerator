namespace Infrastructure.WebApiClient.Models
{
    /// <summary>
    /// Модель информации о выпуске.
    /// </summary>
    public class SetInfo
    {
        /// <summary>
        /// Допускается к стандарту (проедположительно).
        /// </summary>
        public bool IsStandard { get; set; }

        /// <summary>
        /// Название выпуска.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Номер выпуска (предположительно).
        /// </summary>
        public int Ordinal { get; set; }

        /// <summary>
        /// Дата выпуска (Массив из трех чисел - год, месяц, число).
        /// </summary>
        public IEnumerable<int> Release { get; set; }
    }
}