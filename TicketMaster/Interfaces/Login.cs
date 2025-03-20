namespace TicketMaster.Interfaces
{
    public interface Login
    {
        void login(string password, string key) { }
        void email2fa() { }
    }
}
