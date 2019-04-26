using System;

namespace LoanConsoleApplication.Models
{
    public class LoanMonthlyPayment
    {
        public decimal CurrentInterestPaymentAmount { get; set; }
        public decimal CurrentLoanPaymentAmount { get; set; }
        public decimal LoanLeftToPay { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
