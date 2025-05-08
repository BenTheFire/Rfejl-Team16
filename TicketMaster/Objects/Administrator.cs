using TicketMaster.Authentication;

namespace TicketMaster.Objects
{
    public class Administrator : AuthenticateUser
    {
        public new int Id { get => int.Parse(base.Id); set => base.Id = value.ToString(); }
    }
}
