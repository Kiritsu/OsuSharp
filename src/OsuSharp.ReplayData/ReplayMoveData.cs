using System;

namespace OsuSharp.Domain
{
    public struct ReplayMoveData
    {
        public TimeSpan ElapsedTimeSincePreviousAction { get; }
        public float CursorX { get; }
        public float CursorY { get; }
        public OsuKey Keys { get; }

        internal ReplayMoveData(long elapsedTime, float posX, float posY, int pressedKeys)
        {
            ElapsedTimeSincePreviousAction = TimeSpan.FromMilliseconds(elapsedTime);
            CursorX = posX;
            CursorY = posY;
            Keys = (OsuKey)pressedKeys;
        }
    }
}
