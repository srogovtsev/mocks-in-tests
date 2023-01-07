namespace SystemUnderTest;

public interface IRepository
{
    (string expectedCode, bool isMobile, Uri redirect) GetState(string login);
}

public class Controller
{
    private readonly IRepository _repository;

    public Controller(IRepository repository)
    {
        _repository = repository;
    }

    public string Complete(string state, string code)
    {
        var knownState = _repository.GetState(state);
        try
        {
            if (Validate(code, knownState))
            {
                if (knownState.isMobile)
                    return "{\"success\": true, \"redirect\": \"" + knownState.redirect + "\"}";
                else
                    return "302 Location: " + knownState.redirect;
            }
            else
            {
                if (knownState.isMobile)
                    return "{\"success\": false, \"redirect\": \"login\"}";
                else
                    return "302 Location: login";
            }
        }
        catch (Exception e)
        {
            if (knownState.isMobile)
                return "{\"error\": \"" + e.Message + "\"}";
            else
                return "500";
        }
    }

    private static bool Validate(string code, (string expectedCode, bool isMobile, Uri redirect) knownState)
    {
        return code == knownState.expectedCode;
    }
}