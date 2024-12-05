using Core.Application.Abstractions;
using Core.Domain.Entities;
using IronXL;
using static System.Net.WebRequestMethods;

namespace Infrastructure.SpreadSheets
{
    /// <summary>
    /// Сервис сохранения карт в виде .xlsx документа.
    /// </summary>
    public class SpreadSheetStorageCreator : ICardsStorageCreator
    {
        /// <inheritdoc/>
        public Task Create(IEnumerable<Card> cards)
        {
            var workbook = WorkBook.Create(ExcelFileFormat.XLSX);
            var sheet = workbook.CreateWorkSheet("Коллекция карт");

            // шапка
            sheet["A1"].Value = "Название";
            sheet["B1"].Value = "Стихии";
            sheet["C1"].Value = "Классы";
            sheet["D1"].Value = "Тип";
            sheet["E1"].Value = "Стоимость";
            sheet["F1"].Value = "Атака";
            sheet["G1"].Value = "Здоровье";
            sheet["H1"].Value = "Фойл";
            sheet["I1"].Value = "Номер";
            sheet["J1"].Value = "Редкость";
            sheet["K1"].Value = "Выпуск";
            sheet["L1"].Value = "Текст";
            sheet["M1"].Value = "ПФ";
            sheet["N1"].Value = "Ссылка на карту на сайте berserkdeck.ru";

            var rowNumber = 2; // начинаем со второго после шапки
            foreach (var card in cards)
            {
                var classes = new List<string>();
                if (!string.IsNullOrEmpty(card.FirstClass))
                {
                    classes.Add(card.FirstClass);
                }
                if (!string.IsNullOrEmpty(card.SecondClass))
                {
                    classes.Add(card.SecondClass);
                }

                sheet["A" + rowNumber].Value = card.Name;
                sheet["B" + rowNumber].Value = card.Elements;
                sheet["C" + rowNumber].Value = string.Join(", ", classes);
                sheet["D" + rowNumber].Value = card.Type;
                sheet["E" + rowNumber].Value = card.Cost;
                sheet["F" + rowNumber].Value = card.Attack;
                sheet["G" + rowNumber].Value = card.Health;
                sheet["H" + rowNumber].Value = card.IsFoil ? "Да" : string.Empty;
                sheet["I" + rowNumber].Value = card.Number;
                sheet["J" + rowNumber].Value = card.Rarity;
                sheet["K" + rowNumber].Value = $"{card.SetName} ({card.SetNumber})";
                sheet["L" + rowNumber].Value = card.Text?
                                                   .Replace("{TAP}", "[Поворот]")
                                                   .Replace("{COIN}", "[Монета]");
                sheet["M" + rowNumber].Value = card.Variant == "pf" ? "Да" : string.Empty;

                sheet["N" + rowNumber].Value = "*тык*";
                sheet["N" + rowNumber].First().Hyperlink = $"https://berserkdeck.ru/cards/{card.ExternalId}";

                rowNumber++;
            }

            workbook.SaveAs("MyBerserkHeroesCollection.xlsx"); // настроить выходной путь

            return Task.CompletedTask;
        }
    }
}
