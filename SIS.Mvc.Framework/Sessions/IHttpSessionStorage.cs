namespace SIS.WebServer.Sessions
{
    using SIS.HTTP.Sessions.Contracts;

    public interface IHttpSessionStorage
    {
        bool ContainsSession(string id);

        IHttpSession GetSession(string id);
    }
}