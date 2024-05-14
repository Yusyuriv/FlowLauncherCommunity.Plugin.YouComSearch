using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Windows.Controls;
using Flow.Launcher.Plugin;

namespace FlowLauncherCommunity.Plugin.YouComSearch;

public class Main : IPlugin, ISettingProvider {
    private const string IcoPath = "icon.png";
    private PluginInitContext _context = null!;
    private Settings _settings = null!;

    public void Init(PluginInitContext context) {
        _context = context;
        _settings = _context.API.LoadSettingJsonStorage<Settings>();
    }

    public List<Result> Query(Query query) {
        if (query.Search is not { Length: >= 3 }) {
            return new List<Result> {
                new Result {
                    Title = "Query is too short",
                    SubTitle = "Please enter at least 3 characters",
                    IcoPath = IcoPath,
                },
            };
        }

        var mode = _settings.Mode;

        return new List<Result> {
            new NonEqualResult {
                Title = "Search on You.com",
                SubTitle = $"Default mode specified in settings ({mode.ToFriendlyTitle()})",
                IcoPath = IcoPath,
                Action = GetOpenResultAction(query.Search, mode),
                Score = 100,
            },
            new NonEqualResult {
                Title = $"Search on You.com in {EMode.Default.ToFriendlyTitle()} mode",
                IcoPath = IcoPath,
                Action = GetOpenResultAction(query.Search, EMode.Default),
                Score = 50,
            },
            new NonEqualResult {
                Title = $"Search on You.com in {EMode.Agent.ToFriendlyTitle()} mode",
                IcoPath = IcoPath,
                Action = GetOpenResultAction(query.Search, EMode.Agent),
                Score = 40,
            },
            new NonEqualResult {
                Title = $"Search on You.com in {EMode.Research.ToFriendlyTitle()} mode",
                IcoPath = IcoPath,
                Action = GetOpenResultAction(query.Search, EMode.Research),
                Score = 30,
            },
            new NonEqualResult {
                Title = $"Search on You.com in {EMode.Create.ToFriendlyTitle()} mode",
                IcoPath = IcoPath,
                Action = GetOpenResultAction(query.Search, EMode.Create),
                Score = 20,
            },
            new NonEqualResult {
                Title = $"Search on You.com in {EMode.Custom.ToFriendlyTitle()} mode",
                IcoPath = IcoPath,
                Action = GetOpenResultAction(query.Search, EMode.Custom),
                Score = 10,
            },
        };
    }

    private Func<ActionContext, bool> GetOpenResultAction(string query, EMode mode) {
        return _ => {
            var url = $"https://you.com/search?tbm=youchat&q={HttpUtility.UrlEncode(query)}&mode={mode.ToString().ToLower()}";
            _context.API.OpenUrl(url);
            return true;
        };
    }

    public Control CreateSettingPanel() {
        return new SettingsPanel(_settings);
    }
}
