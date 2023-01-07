namespace SystemUnderTest;

public class StateValidator
{
    public bool Validate(string code, (string expectedCode, bool isMobile, Uri redirect) knownState)
        => code == knownState.expectedCode;
}