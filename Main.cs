using System;
using System.Collections.Generic;
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

        var results = new List<Result>();
        var mode = _settings.Mode;

        var addAll = string.IsNullOrEmpty(query.ActionKeyword) || _context.CurrentPluginMetadata.ActionKeyword is "*" || query.ActionKeyword == _context.CurrentPluginMetadata.ActionKeyword;
        if (addAll) {
            results.Add(GetResult(
                query.Search,
                mode,
                100,
                "Search on You.com",
                $"Default mode specified in settings ({mode.ToFriendlyTitle()})"
            ));
        }
        if (addAll || query.ActionKeyword == _settings.KeywordDefault) {
            results.Add(GetResult(query.Search, EMode.Default, 50));
        }
        if (addAll || query.ActionKeyword == _settings.KeywordAgent) {
            results.Add(GetResult(query.Search, EMode.Agent, 40));
        }
        if (addAll || query.ActionKeyword == _settings.KeywordResearch) {
            results.Add(GetResult(query.Search, EMode.Research, 30));
        }
        if (addAll || query.ActionKeyword == _settings.KeywordCreate) {
            results.Add(GetResult(query.Search, EMode.Create, 20));
        }
        if (addAll) {
            results.Add(GetResult(query.Search, EMode.Custom, 10));
        }

        return results;
    }

    private Result GetResult(string search, EMode mode, int score, string? title = null, string? subtitle = null) {
        return new Result {
            Title = title ?? $"Search on You.com in {mode.ToFriendlyTitle()} mode",
            SubTitle = subtitle ?? string.Empty,
            CopyText = search,
            IcoPath = IcoPath,
            Action = GetOpenResultAction(search, mode),
            Score = score,
        };
    }

    private Func<ActionContext, bool> GetOpenResultAction(string query, EMode mode) {
        return _ => {
            var url = $"https://you.com/search?tbm=youchat&q={HttpUtility.UrlEncode(query)}&chatMode={mode.ToString().ToLower()}";
            _context.API.OpenUrl(url);
            return true;
        };
    }

    public Control CreateSettingPanel() {
        return new SettingsPanel(_settings, _context);
    }
}
