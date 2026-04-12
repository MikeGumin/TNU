using System;
using System.Diagnostics;

namespace TNU.Core;

public class SystemStatic
{
    public static readonly string EntryFilePath = $"{DateTime.Now}.csv";

    static public Stopwatch GeneralStopwatch { get; set; } = new Stopwatch();
}