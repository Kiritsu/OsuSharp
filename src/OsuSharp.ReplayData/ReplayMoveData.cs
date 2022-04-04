using System;

namespace OsuSharp.Domain;

/// <summary>
/// Represents a frame of move data from a replay.
/// </summary>
public struct ReplayMoveData
{
    /// <summary>
    /// Gets the elapsed time since the previous move data.
    /// </summary>
    public TimeSpan ElapsedTimeSincePreviousAction { get; }

    /// <summary>
    /// Gets the position X of the cursor.
    /// </summary>
    public float CursorX { get; }

    /// <summary>
    /// Gets the position Y of the cursor.
    /// </summary>
    public float CursorY { get; }

    /// <summary>
    /// Gets the keys pressed during that move data.
    /// </summary>
    public OsuKey Keys { get; }

    internal ReplayMoveData(long elapsedTime, float posX, float posY, int pressedKeys)
    {
        ElapsedTimeSincePreviousAction = TimeSpan.FromMilliseconds(elapsedTime);
        CursorX = posX;
        CursorY = posY;
        Keys = (OsuKey)pressedKeys;
    }
}