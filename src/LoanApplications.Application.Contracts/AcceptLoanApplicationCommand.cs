using Framework.Application;

namespace LoanApplications.Application.Contracts;

public class AcceptLoanApplicationCommand: ICommand
{
	public Guid LoanApplicationId { get; set; }
}