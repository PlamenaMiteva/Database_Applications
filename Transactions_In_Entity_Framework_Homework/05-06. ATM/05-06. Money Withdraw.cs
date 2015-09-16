using System;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using _05_06.ATM;

namespace _05_06.ATM
{
    class Program
    {
        static void Main()
        {
            Console.Write("Please enter the amount you would like to withderaw:");
            string input = Console.ReadLine();
            decimal money;
            if (Decimal.TryParse(input, out money))
            {
                money = Decimal.Parse(input);
                Console.WriteLine();
                Console.Write("Please enter your card number:");
                string cardNumber = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Please enter your PIN:");
                string pin = Console.ReadLine();
                Console.WriteLine();
                WithdrawMoney(cardNumber, pin, money);
            }
            else
            {
                Console.WriteLine("You should enter a numeric value! Please try again!");
            }
        }

        public static void WithdrawMoney(string cardNumber, string cardPIN, decimal moneyToWithdraw)
        {
            bool WithdrawalAllowed = false;
            decimal changedAmount = 0;
            var context = new ATMEntities();
            var clientData = context.CardAccounts.Where(c => c.CardNumber == cardNumber && c.CardPIN == cardPIN).Select(c => new
            {
                c.CardNumber,
                c.CardPIN,
                c.CardCash
            });
            if (!clientData.Any())
            {
                Console.WriteLine("Wrong card number or PIN. Please try again!");
            }
            else
            {
                foreach (var client in clientData)
                {
                    if (client.CardCash < moneyToWithdraw)
                    {
                        Console.WriteLine(
                            "Sorry, the amount you would like to withdraw exceeds the balance of ypur account.");
                        continue;
                    }
                    else
                    {
                        WithdrawalAllowed = true;
                        changedAmount = client.CardCash - moneyToWithdraw;
                    }
                }
                if (WithdrawalAllowed)
                {
                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.Database.ExecuteSqlCommand(
                                    "UPDATE CardAccounts SET CardCash = " + changedAmount + " WHERE CardNumber = '" + cardNumber + "' AND CardPIN = '" + cardPIN + "'");
                             context.SaveChanges();
                            dbContextTransaction.Commit();
                            Console.WriteLine("Transaction completed successfully.");
                            
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback();
                        }
                        using (var dbContextTransaction2 = context.Database.BeginTransaction())
                        {
                            try
                            {
                                context.Database.ExecuteSqlCommand(
                                        "INSERT INTO TransactionHistory(CardNumber, TransactionDate, Amount) VALUES (" + cardNumber + "," + "GETDATE()" + "," + moneyToWithdraw + ")");
                                context.SaveChanges();
                                dbContextTransaction2.Commit();
                                Console.WriteLine("Transaction history has been updated.");

                            }
                            catch (Exception)
                            {
                                dbContextTransaction2.Rollback();
                            }
                            }
                    }
                }
            }
        }
    }
}

