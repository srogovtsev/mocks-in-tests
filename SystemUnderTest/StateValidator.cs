namespace SystemUnderTest;

public interface IStateValidator
{
    bool Validate(string code, (string expectedCode, bool isMobile, Uri redirect) knownState);
}

public class StateValidator : IStateValidator
{
    public bool Validate(string code, (string expectedCode, bool isMobile, Uri redirect) knownState)
        => code == knownState.expectedCode;
}