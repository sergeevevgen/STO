﻿using STOBusinessLogic.OfficePackage.HelperEnums;
using STOBusinessLogic.OfficePackage.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STOBusinessLogic.OfficePackage
{
  /*  public abstract class AbstractSaveToPdf
    {
        public void CreateDocManager(PdfInfo info)
        {
            CreatePdf(info);
            CreateParagraph(new PdfParagraph
            {
                Text = info.Title,
                Style = "NormalTitle"
            });
            CreateParagraph(new PdfParagraph
            {
                Text = $"с { info.DateFrom.ToShortDateString() } по { info.DateTo.ToShortDateString() }",
                Style = "Normal"
            });
            CreateTable(new List<string> { "3cm", "6cm", "3cm", "3cm", "3cm" });
            CreateRow(new PdfRowParameters
            {
                Texts = new List<string> { "Дата валюты", "Название", "Кредитные программы", "Вклады" },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });
            foreach (var currency in info.Currencies)
            {
                CreateRow(new PdfRowParameters
                {

                    Texts = new List<string> { currency.DateAdding.ToShortDateString(),
                                                currency.CurrencyName,
                                                string.Join(", ",currency.LoanPrograms.Select(lp => lp.LoanProgramName).ToList()),
                                                string.Join(", ",currency.Deposits.Select(dep => dep.DepositName).ToList())},
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
            }
            SavePdf(info);
        }

        public void CreateDocClerk(PdfInfo info)
        {
            CreatePdf(info);
            CreateParagraph(new PdfParagraph
            {
                Text = info.Title,
                Style = "NormalTitle"
            });
            CreateParagraph(new PdfParagraph
            {
                Text = $"с { info.DateFrom.ToShortDateString() } по { info.DateTo.ToShortDateString() }",
                Style = "Normal"
            });
            CreateTable(new List<string> { "5cm", "3cm", "5cm", "4cm" });
            CreateRow(new PdfRowParameters
            {
                Texts = new List<string> { "ФИО", "Дата", "Вклады", "Валюты" },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });
            foreach (var client in info.Clients)
            {
                for (int i = 0; i < client.DepositCurrencies.Count; i++)
                {
                    if (i == 0)
                    {
                        CreateRow(new PdfRowParameters
                        {

                            Texts = new List<string> { client.ClientFIO,
                                                client.DateVisit.ToShortDateString(),
                                                client.DepositCurrencies[i].Item1.DepositName,
                                                string.Join(", ", client.DepositCurrencies[i].Item2.Select(cur => cur.CurrencyName).ToList())},
                            Style = "Normal",
                            ParagraphAlignment = PdfParagraphAlignmentType.Left
                        });
                    }
                    else
                    {
                        CreateRow(new PdfRowParameters
                        {

                            Texts = new List<string> { String.Empty, String.Empty,
                                                client.DepositCurrencies[i].Item1.DepositName,
                                                string.Join(", ", client.DepositCurrencies[i].Item2.Select(cur => cur.CurrencyName).ToList())},
                            Style = "Normal",
                            ParagraphAlignment = PdfParagraphAlignmentType.Left
                        });
                    }
                }
            }
            SavePdf(info);
        }

        /// <summary>/// Создание doc-файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void CreatePdf(PdfInfo info);
        /// <summary>
        /// Создание параграфа с текстом
        /// </summary>
        /// <param name="title"></param>
        /// <param name="style"></param>
        protected abstract void CreateParagraph(PdfParagraph paragraph);
        /// <summary>
        /// Создание таблицы
        /// </summary>
        /// <param name="title"></param>
        /// <param name="style"></param>
        protected abstract void CreateTable(List<string> columns);
        /// <summary>
        /// Создание и заполнение строки
        /// </summary>
        /// <param name="rowParameters"></param>
        protected abstract void CreateRow(PdfRowParameters rowParameters);
        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void SavePdf(PdfInfo info);
    }*/
}