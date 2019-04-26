using LoanConsoleApplication.Models;

namespace LoanConsoleApplication.Services.LoanInformationPrintingService
{
    public interface ILoanInformationPrintingService
    {
        void PrintLoanInformation(LoanInformation loanInformation);
    }
}
