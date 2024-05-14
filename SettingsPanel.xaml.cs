using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace FlowLauncherCommunity.Plugin.YouComSearch;

public partial class SettingsPanel : UserControl, INotifyPropertyChanged {
    private readonly Settings _settings;

    public SettingsModeDisplay Mode {
        get => new(_settings.Mode);
        set {
            _settings.Mode = value.Value;
            OnPropertyChanged();
        }
    }

    public static SettingsModeDisplay[] Modes => new SettingsModeDisplay[] {
        new(EMode.Default),
        new(EMode.Agent),
        new(EMode.Research),
        new(EMode.Create),
        new(EMode.Custom),
    };

    public SettingsPanel(Settings settings) {
        _settings = settings;
        InitializeComponent();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public record SettingsModeDisplay(EMode Value) {
    public string Title => Value.ToFriendlyTitle();
}
