using Framework.Core.Events;
using LoanApplications.Domain.Contracts;
using LoanApplications.Projections.Sql.Handlers.Model;

namespace LoanApplications.Projections.Sql.Handlers
{
    public class LoanApplicationRejectedHandler : IEventHandler<LoanApplicationRejected>
    {
        private readonly LoanApplicationContext _context;
        public LoanApplicationRejectedHandler(LoanApplicationContext context)
        {
            _context = context;
        }
        public async Task Handle(LoanApplicationRejected @event)
        {
            var loanApplication = _context.LoanApplications.First(a => a.Id == @event.LoanApplicationId);
            loanApplication.State = LoanApplicationState.Rejected;
            //loanApplication.RejectedReason = @event.Reason;
            await _context.SaveChangesAsync();
        }
    }
}