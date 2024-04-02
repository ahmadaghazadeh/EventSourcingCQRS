namespace LoanApplications.Domain.Model.LoanApplications.States;

public class InProgress : LoanApplicationState
{
	public override bool CanAccept() => true;

	public override bool CanReject() => true;
        
}