###### 1 - Add this section to your appsettings.json and modify it to your needs:

```
"MetricsOptions": {
    "DefaultContextLabel": "MyCustomContext",
    "Enabled": true
},
    "MetricsWebTrackingOptions": {
    "ApdexTrackingEnabled": true,
    "ApdexTSeconds": 0.1,
    "IgnoredHttpStatusCodes": [ 404 ],
    "IgnoredRoutesRegexPatterns": [],
    "OAuth2TrackingEnabled": true
},
    "MetricEndpointsOptions": {
    "MetricsEndpointEnabled": true,
    "MetricsTextEndpointEnabled": true,
    "PingEndpointEnabled": true,
    "EnvironmentInfoEndpointEnabled": true
},
    "MetricsReportingInfluxDb2Options": {
    "InfluxDb2": {
    "BaseUri": "http://127.0.0.1:8086",
    "Organization": "metrics",
    "Bucket": "metrics",
    "Token": "changeme"
},
    "HttpPolicy": {
    "BackoffPeriod": "0:0:30",
    "FailuresBeforeBackoff": 5,
    "Timeout": "0:0:40"
},
    "ReportInterval": "0:0:1"
}
```

<br>

###### 2: Add this before ```builder.Build()```;
```
builder.Services.AddMetricsServices();
builder.WebHost.AddInfluxDb2AppMetrics(builder.Configuration);
```

<br>

###### 3: Add this after ```builder.Build()```;
```
app.UseRequestsCounterMiddleware();
app.UseRequestsDurationMiddleware();
app.UseResponsesSizeMiddleware();
app.UseResponsesApdexMiddleware();
app.UseMetricsAllMiddleware();
app.UseMetricsAllEndpoints();
```

<br>

###### 4 Add this before ```app.Run()```;
```
app.UseHttpStatusCodesCounterMiddleware();
```