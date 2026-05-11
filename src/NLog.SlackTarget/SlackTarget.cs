using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using NLog.Config;
using NLog.Layouts;
using NLog.SlackTarget;

namespace NLog.Targets
{
    [Target("Slack")]
    public class SlackTarget : AsyncTaskTarget
    {
        private static readonly Uri ApiUri = new Uri("https://slack.com/api/chat.postMessage");

        private static readonly JsonSerializerOptions SlackJsonSettings = new JsonSerializerOptions
                                                                          {
                                                                              PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                                                                              IgnoreNullValues = true
                                                                          };

        [RequiredParameter]
        public string Token { get; set; }

        [RequiredParameter]
        public string Channel { get; set; }

        protected override async Task WriteAsyncTask(LogEventInfo logEvent, CancellationToken cancellationToken)
        {
            var hostName = Layout.FromString("${machinename}").Render(LogEventInfo.CreateNullEvent());
            var appName = Layout.FromString("${processname}").Render(LogEventInfo.CreateNullEvent());

            var emoji = GetEmoji(logEvent.Level);
            var renderedMessage = this.RenderLogEvent(this.Layout, logEvent);

            var header = $"{emoji} {logEvent.Level.ToString().ToUpper()} from {logEvent.LoggerName} in {appName} on {hostName}";

            var payload = new Payload
                          {
                              Channel = this.Channel,
                              Text = $"{header}\n{renderedMessage.SmsTruncate()}",
                              Blocks = new List<object>
                                       {
                                           new Payload.HeaderBlock { Text = new Payload.TextObject { Type = "plain_text", Text = header } },
                                           new Payload.MarkdownBlock { Text = $"```\n{renderedMessage}\n```" }
                                       }
                          };

            var json = JsonSerializer.Serialize(payload, SlackJsonSettings);

            var request = new HttpRequestMessage(HttpMethod.Post, ApiUri.PathAndQuery)
                          {
                              Content = new StringContent(json, Encoding.UTF8, "application/json")
                          };

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.Token);

            var httpClient = HttpClientFactory.Instance.CreateClient(new Uri(ApiUri.GetLeftPart(UriPartial.Authority)));

            await httpClient.SendAsync(request, cancellationToken);
        }

        private static string GetEmoji(LogLevel logLevel)
        {
            if (logLevel == LogLevel.Warn) return ":warning:";
            if (logLevel == LogLevel.Error) return ":rotating_light:";
            if (logLevel == LogLevel.Fatal) return ":fire:";

            return ":information_source:";
        }
    }
}
