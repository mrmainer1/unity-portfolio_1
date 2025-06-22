using System;
using System.Text;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Debugs
{
    public static class EEDebug
    {
        private static StringBuilder builder = new StringBuilder();

        public static void Log(EEDebugTag tag, object message, LogType type = LogType.Normal)
        {
            #if DEBUG
                if (message == null) message = "[NULL]";
                builder.Clear();
                builder.Append("[");
                builder.Append(tag);
                builder.Append("] ");
                builder.Append(message);
                builder.Append(", Frame=");
                builder.Append(Time.frameCount);
                builder.Append(", Time=");
                builder.Append(Time.time);
                switch (type)
                {
                    case LogType.Normal:
                        Debug.Log(builder.ToString());
                        break;
                    case LogType.Warning:
                        Debug.LogWarning(builder.ToString());
                        break;
                    case LogType.Error:
                        Debug.LogError(builder.ToString());
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }

            #endif
        }
        
        public static void Log(object message, LogType type = LogType.Normal)
        {
            Log(EEDebugTag.Default, message, type);
        }

        public static void Ray(Vector3 pos, Vector3 dir = default, Color color = default)
        {
            if (dir == default) dir = Vector3.up;
            if (color == default) color = Color.magenta;
            dir *= 100;
            Debug.DrawRay(pos, dir, color, 9999);
        }

        public static void ShowProblemObject(GameObject gameObject, LogType type = LogType.Normal)
        {
            var id = gameObject.GetInstanceID();
            gameObject.name = id.ToString();
            Log(id, type);
        }

        public enum LogType
        {
            Normal,
            Warning,
            Error
        }
    }
}
