using System;

namespace OsuSharp.Domain;

/// <summary>
/// Represents the available osu keys.
/// </summary>
[Flags]
public enum OsuKey
{
    /// <summary>
    /// Mouse button 1.
    /// </summary>
    M1 = 1,

    /// <summary>
    /// Mouse button 2.
    /// </summary>
    M2 = 2,

    /// <summary>
    /// Keyboard button 1.
    /// </summary>
    K1 = 4,

    /// <summary>
    /// Keyboard button 2.
    /// </summary>
    K2 = 8,

    /// <summary>
    /// Smoke button.
    /// </summary>
    Smoke = 16
}