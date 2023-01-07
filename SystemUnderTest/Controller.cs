namespace SystemUnderTest;

public interface IRepository
{
    (string expectedCode, bool isMobile, Uri redirect) GetState(string login);
}

public class Controller
{
    private readonly IRepository _repository;
    private readonly StateValidator _stateValidator;

    public Controller(IRepository repository)
    {
        _repository = repository;
        _stateValidator = new StateValidator();
    }

    public string Complete(string state, string code)
    {
        var knownState = _repository.GetState(state);
        try
        {
            if (_stateValidator.Validate(code, knownState))
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
}