﻿using PekaVMClient;
using System.Text;

namespace TestApp;

internal class Program
{
    static async Task Main(string[] args)
    {
        await using var client = new VMClient();
        await RunGetStopPoints(client, "Bał");
        //await RunGetLines(client, "14");
        //await RunGetTimes(client, "RKAP71");
    }

    private static async Task RunGetStopPoints(VMClient client, string pattern)
    {
        var points = await client.GetStopPoints(pattern);
        foreach (var point in points)
        {
            Console.WriteLine($"[{point.Symbol}] {point.Name}");
        }
    }


    private static async Task RunGetLines(VMClient client, string pattern)
    {
        var lines = await client.GetLinesAsync(pattern);
        Console.WriteLine($"Lines matching pattern '{pattern}': " + string.Join(", ", lines));
    }

    private static async Task RunGetTimes(VMClient client, string tag)
    {
        var times = await client.GetTimesAsync(tag);
        var sb = new StringBuilder();
        foreach (var time in times)
        {
            sb.Append(time.Line);
            sb.Append("; ");
            sb.Append(time.Direction);
            sb.Append("; ");
            if (time.IsRealTime)
            {
                sb.Append(time.Minutes);
                sb.Append("min");
            }
            else
            {
                sb.Append(time.Departure.ToShortTimeString());
            }
            Console.WriteLine(sb.ToString());
            sb.Clear();
        }
    }
}