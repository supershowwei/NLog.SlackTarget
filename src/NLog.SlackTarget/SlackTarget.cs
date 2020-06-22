using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using NLog.Config;
using NLog.SlackTarget;

namespace NLog.Targets
{
    [Target("Slack")]
    public class SlackTarget : AsyncTaskTarget
    {
        [RequiredParameter]
        public string WebhookUrl { get; set; }

        protected override async Task WriteAsyncTask(LogEventInfo logEvent, CancellationToken cancellationToken)
        {
            var hostName = Environment.MachineName;
            var appDomain = AppDomain.CurrentDomain;
            var renderedLog = this.RenderLogEvent(this.Layout, logEvent);

            var title = $"[{logEvent.Level.ToString().ToUpper()}] on {hostName}";

            var payload = JsonSerializer.Serialize(
                new
                {
                    Username = appDomain.FriendlyName,
                    Fallback = title,
                    Color = GetColor(logEvent.Level),
                    Fields = new[] { new { Title = title, Value = renderedLog, Short = false } }
                },
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            var webhook = new Uri(this.WebhookUrl);

            var request = new HttpRequestMessage(HttpMethod.Post, webhook)
                          {
                              Content = new StringContent(
                                  string.Concat("payload=", payload),
                                  Encoding.UTF8,
                                  "application/x-www-form-urlencoded")
                          };

            var httpClient = HttpClientFactory.Instance.CreateClient(new Uri("https://hooks.slack.com"));

            await httpClient.SendAsync(request, cancellationToken);
        }

        private static string GetColor(LogLevel logLevel)
        {
            if (logLevel == LogLevel.Warn) return "warning";
            if (logLevel == LogLevel.Error || logLevel == LogLevel.Fatal) return "danger";

            return "good";
        }
    }
}
