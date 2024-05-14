namespace FlowLauncherCommunity.Plugin.YouComSearch;

public class Settings {
    public EMode Mode { get; set; } = EMode.Default;
    public string KeywordDefault { get; set; } = string.Empty;
    public string KeywordAgent { get; set; } = string.Empty;
    public string KeywordResearch { get; set; } = string.Empty;
    public string KeywordCreate { get; set; } = string.Empty;
}
