namespace LoanApplications.Domain.Model.LoanApplications.States;

public class Accepted : LoanApplicationState
{
	public override bool CanAccept() => false;
	public override bool CanReject() => false;
        
}