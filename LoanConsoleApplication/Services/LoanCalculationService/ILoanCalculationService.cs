using LoanConsoleApplication.Models;

namespace LoanConsoleApplication.Services.LoanCalculationService
{
    public interface ILoanCalculationService
    {
        LoanInformation CalculateLoanInformation(decimal loanSum, int loanDuration, decimal interestRate);
    }
}
