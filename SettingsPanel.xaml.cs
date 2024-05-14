using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using Flow.Launcher.Plugin;

namespace FlowLauncherCommunity.Plugin.YouComSearch;

public partial class SettingsPanel : UserControl, INotifyPropertyChanged {
    private readonly Settings _settings;
    private readonly PluginInitContext _context;

    public SettingsModeDisplay Mode {
        get => new(_settings.Mode);
        set {
            _settings.Mode = value.Value;
            OnPropertyChanged();
        }
    }

    public string KeywordDefault {
        get => _settings.KeywordDefault;
        set {
            ReRegisterActionKeyword(_settings.KeywordDefault, value);
            _settings.KeywordDefault = value;
            OnPropertyChanged();
        }
    }

    public string KeywordAgent {
        get => _settings.KeywordAgent;
        set {
            ReRegisterActionKeyword(_settings.KeywordAgent, value);
            _settings.KeywordAgent = value;
            OnPropertyChanged();
        }
    }

    public string KeywordResearch {
        get => _settings.KeywordResearch;
        set {
            ReRegisterActionKeyword(_settings.KeywordResearch, value);
            _settings.KeywordResearch = value;
            OnPropertyChanged();
        }
    }

    public string KeywordCreate {
        get => _settings.KeywordCreate;
        set {
            ReRegisterActionKeyword(_settings.KeywordCreate, value);
            _settings.KeywordCreate = value;
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

    public SettingsPanel(Settings settings, PluginInitContext context) {
        _settings = settings;
        _context = context;
        InitializeComponent();
    }

    private void ReRegisterActionKeyword(string old, string @new) {
        if (!string.IsNullOrWhiteSpace(old))
            _context.API.RemoveActionKeyword(_context.CurrentPluginMetadata.ID, old);

        if (!string.IsNullOrWhiteSpace(@new))
            _context.API.AddActionKeyword(_context.CurrentPluginMetadata.ID, @new);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
