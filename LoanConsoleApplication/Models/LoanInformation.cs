using System;
using System.Collections.Generic;

namespace LoanConsoleApplication.Models
{
    public class LoanInformation
    {
        public decimal LoanSum { get; set; }
        public decimal LoanSumToReturn { get; set; }
        public decimal MonthlyPayment { get; set; }
        public decimal InterestRate { get; set; }
        public decimal EffectiveAnnualInterestRate { get; set; }
        public List<LoanMonthlyPayment> Payments { get; set; }
    }
}
