namespace FlowLauncherCommunity.Plugin.YouComSearch;

public record SettingsModeDisplay(EMode Value) {
    public string Title => Value.ToFriendlyTitle();
}
