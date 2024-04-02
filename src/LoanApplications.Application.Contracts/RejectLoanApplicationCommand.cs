
using Framework.Application;

namespace LoanApplications.Application.Contracts
{
    public class RejectLoanApplicationCommand : ICommand
{
        public Guid LoanApplicationId { get; set; }
        public string Reason { get; set; }
    }
}
