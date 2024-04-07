using EventStore.ClientAPI;

namespace LoanApplications.Projections.Sql.Framework
{
    public interface ICursor
    {
        Position CurrentPosition();
        void MoveTo(Position position);

    }
    
}
