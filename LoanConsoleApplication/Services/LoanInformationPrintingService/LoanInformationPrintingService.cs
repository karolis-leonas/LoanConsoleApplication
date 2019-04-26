using System;
using LoanConsoleApplication.Models;

namespace LoanConsoleApplication.Services.LoanInformationPrintingService
{
    public class LoanInformationPrintingService : ILoanInformationPrintingService
    {
        public void PrintLoanInformation(LoanInformation loanInformation)
        {
            Console.WriteLine();
            Console.WriteLine("RESULTS:");
            Console.WriteLine($"Current loan: {Math.Round(loanInformation.LoanSum, 2)}");
            Console.WriteLine($"Amount to be returned from client: {Math.Round(loanInformation.LoanSumToReturn, 2)}");
            Console.WriteLine($"Monthly payment for the loan: {Math.Round(loanInformation.MonthlyPayment, 2)}");
            Console.WriteLine($"Interest rate: {Math.Round(loanInformation.InterestRate, 2) * 100}%");
            Console.WriteLine($"Effective annual interest rate (lt. BVKKMN): {Math.Round(loanInformation.EffectiveAnnualInterestRate, 2) * 100}%");
            Console.WriteLine();

            Console.WriteLine("LOAN PAYMENT TABLE:");
            Console.WriteLine("{0,8} {1,12} {2,18} {3,14} {4,14}", "Month", "Date",
                "Interest payment", "Loan payment", "Loan residue");

            for (var paymentId = 0; paymentId < loanInformation.Payments.Count; paymentId++)
            {
                Console.WriteLine("{0,8} {1,12} {2,18} {3,14} {4,14}", paymentId + 1, 
                    loanInformation.Payments[paymentId].PaymentDate.ToString("yyyy-MM-dd"),
                    Math.Round(loanInformation.Payments[paymentId].CurrentInterestPaymentAmount, 2),
                    Math.Round(loanInformation.Payments[paymentId].CurrentLoanPaymentAmount, 2),
                    Math.Round(loanInformation.Payments[paymentId].LoanLeftToPay, 2));
            }
            Console.WriteLine();
        }
    }
}
