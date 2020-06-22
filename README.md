# NLog.SlackTarget

This is NLog target for Slack, check following configuration:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" autoReload="true">
  <extensions>
    <add assembly="NLog.SlackTarget"/>
  </extensions>
  <targets async="true">
    <target name="Slack" xsi:type="Slack"
            webhookUrl="https://hooks.slack.com/services/xxx/yyy/zzz"
            layout="${message}" />
  </targets>
  <rules>
    <logger name="*" minlevel="Debug" writeTo="Slack" />
  </rules>
</nlog>
```
