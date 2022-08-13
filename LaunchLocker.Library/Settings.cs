namespace LaunchLocker.Library;

public record Settings
{
    public Settings()
    {
        ProblemIndicators = new List<string>();
    }

    public List<string> ProblemIndicators { get; set; }
}

