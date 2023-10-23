using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace Knitter.Utils;

public static class Time
{
    static Time()
    {
        _timer.Start();
    }

    private static Stopwatch _timer = new Stopwatch();
    private static long _lastUpdateMilliseconds;
    private static long _currentUpdateMilliseconds;

    public static float deltaTime;
    public static long SecondsFromStartup => _currentUpdateMilliseconds / 1000;
    public static long MillisecondsFromStartup => _currentUpdateMilliseconds;

    public static void Update()
    {
        _currentUpdateMilliseconds = _timer.ElapsedMilliseconds;
        deltaTime = (_currentUpdateMilliseconds - _lastUpdateMilliseconds) / 1000f;

        _lastUpdateMilliseconds = _currentUpdateMilliseconds;
    }
}
