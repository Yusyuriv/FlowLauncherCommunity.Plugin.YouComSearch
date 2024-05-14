namespace FlowLauncherCommunity.Plugin.YouComSearch;

public enum EMode {
    Default,
    Agent,
    Research,
    Custom,
    Create,
}

public static class EModeExtensions {
    public static string ToFriendlyTitle(this EMode mode) {
        return mode switch {
            EMode.Default => "Smart",
            EMode.Agent => "Genius",
            EMode.Research => "Research",
            EMode.Custom => "Custom",
            EMode.Create => "Create",
            _ => "Unknown",
        };
    }
}
