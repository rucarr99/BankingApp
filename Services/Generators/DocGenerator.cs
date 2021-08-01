using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using BankingApp.DAL;
using BankingApp.DAL.Abstractions;
using BankingApp.DataModel.Models;
using Xceed.Document.NET;
using Xceed.Words.NET;
using Formatting = Xceed.Document.NET.Formatting;

namespace Services.Generators
{
    public class DocGenerator : IGenerator
    {
        private static readonly IRepositoryWrapper RepositoryWrapper = new RepositoryWrapper(new BankContext());
        private AccountService _accountService = new(RepositoryWrapper);
        private readonly CustomerService _customerService = new(RepositoryWrapper);
        public void Generate(IList<Transaction> transactions, Account account)
        {
            var fileName = $"TransactionReport{account.Iban}.docx";
            var doc = DocX.Create(fileName);

            var customer = _customerService.GetCustomerByAccount(account);

            var title = $"Transaction report for {customer.FirstName} {customer.LastName}";

            var titleFormat = new Formatting
            {
                FontFamily = new Font("Times new roman"),
                Size = 20D,
                Position = 40,
                FontColor = Color.Black,
                Italic = true
            };



            var paragraphTitle = doc.InsertParagraph(title, false, titleFormat);

            var textParagraph =
                $" Address: {customer.Address}\n CNP: {customer.Cnp} \n Phone Number: {customer.PhoneNumber}\n" +
                $" Iban: {account.Iban} \n Current balance: {account.Balance} \n Currency: {account.Currency}\n";

            var textParagraphFormat = new Formatting
            {
                FontFamily = new Font("Century Gothic"), Size = 12D, Spacing = 2
            };

            doc.InsertParagraph(textParagraph, false, textParagraphFormat);

            paragraphTitle.Alignment = Alignment.center;

            var n = transactions.Count;

            var t = doc.AddTable(n + 1, 4);
            t.Alignment = Alignment.center;
            t.Design = TableDesign.ColorfulList;

            t.Rows[0].Cells[0].Paragraphs.First().Append("Transaction type");
            t.Rows[0].Cells[1].Paragraphs.First().Append("Description");
            t.Rows[0].Cells[2].Paragraphs.First().Append("Date");
            t.Rows[0].Cells[3].Paragraphs.First().Append("Destination account");

            for (var i = 1; i < n + 1; i++)
            {
                t.Rows[i].Cells[0].Paragraphs.First().Append(transactions[i-1].TransactionType.ToString());
                t.Rows[i].Cells[1].Paragraphs.First().Append(transactions[i-1].Description);
                t.Rows[i].Cells[2].Paragraphs.First().Append(transactions[i-1].Date.ToString());
                t.Rows[i].Cells[3].Paragraphs.First().Append(transactions[i-1].DestinationAccount ?? "");
            }

            doc.InsertTable(t);

            doc.Save();


        }
    }
}
