using TicketMaster.Authentication;

namespace TicketMaster.Objects
{
    public class User : AuthenticateUser
    {
        public new int Id { get => int.Parse(base.Id); set => base.Id = value.ToString(); }
    }
}
