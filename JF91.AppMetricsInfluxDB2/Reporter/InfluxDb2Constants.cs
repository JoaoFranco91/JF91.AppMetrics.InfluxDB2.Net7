﻿namespace JF91.AppMetricsInfluxDB2.Reporter;

internal static class InfluxDb2Constants
{
    public static readonly TimeSpan DefaultBackoffPeriod = TimeSpan.FromSeconds(30);
    public static readonly int DefaultFailuresBeforeBackoff = 3;
    public static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(30);
}