using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoanConsoleApplication.Models;

namespace LoanConsoleApplication.Services.LoanCalculationService
{
    public class LoanCalculationService : ILoanCalculationService
    {
        public LoanInformation CalculateLoanInformation(decimal loanSum, int loanDuration, decimal interestRate)
        {
            var decimalInterestRate = interestRate / 100;
            var monthlyInterestRate = (double) decimalInterestRate / 12;

            var loanMonthlyPayment = CalculateMonthlyPayment(loanSum, loanDuration, monthlyInterestRate);
            var effectiveAnnualInterestRate = CalculateEffectiveAnnualInterestRate(monthlyInterestRate);

            var loanInformation = new LoanInformation()
            {
                LoanSum = loanSum,
                InterestRate = decimalInterestRate,
                MonthlyPayment = loanMonthlyPayment,
                EffectiveAnnualInterestRate = effectiveAnnualInterestRate,
                LoanSumToReturn = loanMonthlyPayment * loanDuration,
                Payments = CalculateLoanPayments(loanSum, loanDuration, monthlyInterestRate, loanMonthlyPayment)
            };
            
            return loanInformation;
        }

        private decimal CalculateMonthlyPayment(decimal loanSum, int loanDuration, double monthlyInterestRate)
        {
            var rateResult = Math.Pow(1 + monthlyInterestRate, loanDuration);
            var rateCalculationResult = ((monthlyInterestRate * rateResult) / (rateResult - 1));

            var monthlyPaymentDecimal = loanSum * (decimal) rateCalculationResult;

            return monthlyPaymentDecimal;
        }

        private decimal CalculateEffectiveAnnualInterestRate(double monthlyInterestRate)
        {
            var effectiveAnnualInterestRate = (decimal) Math.Pow((1 + monthlyInterestRate), 12) - 1;

            return effectiveAnnualInterestRate;
        }

        private List<LoanMonthlyPayment> CalculateLoanPayments(decimal loanSum, int loanDuration, double monthlyInterestRate, decimal monthlyPayment)
        {
            var loanPayments = new List<LoanMonthlyPayment>();
            var loanLeftToPay = loanSum;
            var currentPaymentDate = DateTime.Now;
            var decimalMonthlyInterestRate = (decimal) monthlyInterestRate;

            for (var paymentMonth = 0; paymentMonth < loanDuration; paymentMonth++)
            {
                var currentInterestPaymentAmount = (!loanPayments.Any() || loanPayments.Count < paymentMonth)
                        ? loanSum * decimalMonthlyInterestRate
                        : loanPayments[paymentMonth - 1].LoanLeftToPay * decimalMonthlyInterestRate;

                var currentLoanPaymentAmount = monthlyPayment - currentInterestPaymentAmount;

                loanLeftToPay -= currentLoanPaymentAmount;
                var paymentDate = currentPaymentDate.AddMonths(paymentMonth + 1);

                var loanPayment = new LoanMonthlyPayment
                {
                    CurrentInterestPaymentAmount = currentInterestPaymentAmount,
                    CurrentLoanPaymentAmount = currentLoanPaymentAmount,
                    LoanLeftToPay = loanLeftToPay,
                    PaymentDate = Convert.ToDateTime(paymentDate.ToString("yyyy-MM-dd"))
                };

                loanPayments.Add(loanPayment);
            }

            return loanPayments;
        }
    }
}
