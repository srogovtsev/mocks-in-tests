namespace Tests;

internal class RepositoryStub : Dictionary<string, (string expectedCode, bool isMobile, Uri redirect)>, IRepository
{
    public (string expectedCode, bool isMobile, Uri redirect) GetState(string login)
        => TryGetValue(login, out var value) ? value : ("some", false, new Uri("http://some.con"));
}