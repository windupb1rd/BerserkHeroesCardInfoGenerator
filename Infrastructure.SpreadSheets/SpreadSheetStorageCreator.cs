using ClosedXML.Excel;
using Core.Application.Abstractions;
using Core.Domain.Entities;
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
            var workbook = new XLWorkbook();
            var sheet = workbook.AddWorksheet("Коллекция карт");

            // шапка
            sheet.Cell("A1").Value = "Название";
            sheet.Cell("B1").Value = "Стихии";
            sheet.Cell("C1").Value = "Классы";
            sheet.Cell("D1").Value = "Тип";
            sheet.Cell("E1").Value = "Стоимость";
            sheet.Cell("F1").Value = "Атака";
            sheet.Cell("G1").Value = "Здоровье";
            sheet.Cell("H1").Value = "Фойл";
            sheet.Cell("I1").Value = "Номер";
            sheet.Cell("J1").Value = "Редкость";
            sheet.Cell("K1").Value = "Выпуск";
            sheet.Cell("L1").Value = "Текст";
            sheet.Cell("M1").Value = "ПФ";
            sheet.Cell("N1").Value = "Ссылка на карту на сайте berserkdeck.ru";

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

                sheet.Cell("A" + rowNumber).Value = card.Name;
                sheet.Cell("B" + rowNumber).Value = card.Elements;
                sheet.Cell("C" + rowNumber).Value = string.Join(", ", classes);
                sheet.Cell("D" + rowNumber).Value = card.Type;
                sheet.Cell("E" + rowNumber).Value = card.Cost;
                sheet.Cell("F" + rowNumber).Value = card.Attack;
                sheet.Cell("G" + rowNumber).Value = card.Health;
                sheet.Cell("H" + rowNumber).Value = card.IsFoil ? "Да" : string.Empty;
                sheet.Cell("I" + rowNumber).Value = card.Number;
                sheet.Cell("J" + rowNumber).Value = card.Rarity;
                sheet.Cell("K" + rowNumber).Value = $"{card.SetName} ({card.SetNumber})";
                sheet.Cell("L" + rowNumber).Value = card.Text?
                                                   .Replace("{TAP}", "[Поворот]")
                                                   .Replace("{COIN}", "[Монета]");
                sheet.Cell("M" + rowNumber).Value = card.Variant == "pf" ? "Да" : string.Empty;

                sheet.Cell("N" + rowNumber).Value = "*тык*";
                sheet.Cell("N" + rowNumber).SetHyperlink(new XLHyperlink($"https://berserkdeck.ru/cards/{card.ExternalId}"));

                rowNumber++;
            }

            workbook.SaveAs("MyBerserkHeroesCollection.xlsx"); // настроить выходной путь

            return Task.CompletedTask;
        }
    }
}
