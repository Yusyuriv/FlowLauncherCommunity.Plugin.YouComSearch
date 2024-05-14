using Flow.Launcher.Plugin;

namespace FlowLauncherCommunity.Plugin.YouComSearch;

public class NonEqualResult : Result {
    public override bool Equals(object? obj) {
        return false;
    }

    public override int GetHashCode() {
        return base.GetHashCode();
    }
}
