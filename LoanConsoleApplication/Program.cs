using System;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using LoanConsoleApplication.Services.LoanCalculationService;
using LoanConsoleApplication.Services.LoanInformationPrintingService;

namespace LoanConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var logger = serviceProvider.GetService<ILogger<Program>>();
            var loanCalculationService = serviceProvider.GetService<ILoanCalculationService>();
            var loanInformationPrintingService = serviceProvider.GetService<ILoanInformationPrintingService>();

            var priceRegex = new Regex(@"^\d+\.\d{2}?$");

            try
            {
                decimal loanSum;
                Console.WriteLine("Please write in amount of the loaned money");

                do
                {
                    Console.WriteLine("Price must be a positive number of decimal type with two numbers after decimal point.");
                } while (!decimal.TryParse(Console.ReadLine().Replace('.', ','), out loanSum) && !priceRegex.IsMatch(loanSum.ToString()) && loanSum <= 0);

                int loanDuration;
                Console.WriteLine("Please write in the duration of the loan (in months)");
                do
                {
                    Console.WriteLine("Loan duration must be a positive number of integer type.");
                } while (!int.TryParse(Console.ReadLine().Replace('.', ','), out loanDuration) && loanDuration <= 0);

                decimal interestRate;
                Console.WriteLine("Please write in the loan's interest rate (in percentage).");
                do
                {
                    Console.WriteLine(
                        "Loan's interest rate must be a positive number of decimal type with two numbers after decimal point.");
                } while (!decimal.TryParse(Console.ReadLine().Replace('.', ','), out interestRate) &&
                         priceRegex.IsMatch(interestRate.ToString()) && interestRate <= 0);
                
                var loanInformation = loanCalculationService.CalculateLoanInformation(loanSum, loanDuration, interestRate);
                loanInformationPrintingService.PrintLoanInformation(loanInformation);

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            } catch (Exception ex)
            {
                logger.LogError(ex.ToString());
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConsole())
                .AddSingleton<ILoanCalculationService, LoanCalculationService>()
                .AddSingleton<ILoanInformationPrintingService, LoanInformationPrintingService>()
                .BuildServiceProvider();
        }
    }
}
