using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VMCReplaceAvatar
{
    public class Logger
    {
        private string _baseDir = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
        private string _logFilePath;
        public enum LogLevel
        {
            Debug,
            Warning,
            Error
        }

        public Logger()
        {
            var logDir = Path.Combine(_baseDir, "Log");
            if (!Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }
            _logFilePath = Path.Combine(logDir, "ReplaceAvatarLog" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt");
        }

        private void LocalLog(string message, LogLevel level = LogLevel.Debug)
        {
            File.AppendAllText(_logFilePath, "[" + level.ToString() + " @ " + DateTime.Now.ToString("yy/MM/dd HH:mm:ss") + " | " + "ReplaceAvatar] " + message + Environment.NewLine);
        }

        public void Log(string message, LogLevel level = LogLevel.Debug)
        {
            LocalLog(message, level);
            switch (level)
            {
                case LogLevel.Debug:
                    Debug.Log(message);
                    break;
                case LogLevel.Warning:
                    Debug.LogWarning(message);
                    break;
                case LogLevel.Error:
                    Debug.LogError(message);
                    break;
                default:
                    Debug.Log(message);
                    break;
            }
        }

        public void LogWarning(string message)
        {
            LocalLog(message, LogLevel.Warning);
            Debug.LogWarning(message);
        }

        public void LogError(string message)
        {
            LocalLog(message, LogLevel.Error);
            Debug.LogError(message);
        }
    }
}
