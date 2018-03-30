using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace PolyRents.helpers
{
    public class MessageEventArgs : EventArgs
    {
        private string datetime;
        private string level;
        private string message;

        public string DateTime
        {
            get
            {
                return datetime;
            }
        }

        public string Level
        {
            get
            {
                return level;
            }
        }

        public string Message
        {
            get
            {
                return message;
            }
        }


        public MessageEventArgs(string DateTime, string level, string message)
        {
            this.datetime = DateTime;
            this.level = level;
            this.message = message;
        }
    }

    public delegate void MessageChangedHandler(object sender, MessageEventArgs e);

    public class Logger
    {
        private string DatetimeFormat;
        private string Filename;
        private string message;
        
        public event MessageChangedHandler MessageChanged;

        private static Logger myInstance;

        /// <summary>
        /// Initialize a new instance of Logger class.
        /// Log file will be created automatically if not yet exists, else it can be either a fresh new file or append to the existing file.
        /// Default is create a fresh new log file.
        /// </summary>
        /// <param name="append">True to append to existing log file, False to overwrite and create new log file</param>
        private Logger(bool append = false)
        {
            DatetimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
            Filename = Assembly.GetExecutingAssembly().GetName().Name + ".log";

            // Log file header line
            string logHeader = Filename + " is created.";
            if (!File.Exists(Filename))
            {
                WriteLine(DateTime.Now.ToString(DatetimeFormat) + " " + logHeader, false);
            }
            else
            {
                if (append == false)
                    WriteLine(DateTime.Now.ToString(DatetimeFormat) + " " + logHeader, false);
            }
        }

        public static Logger getInstance(bool append = false)
        {
            if (myInstance == null)
            {
                myInstance = new Logger(append);
            }

            return myInstance;
        }

        protected virtual void OnMessageChanged(MessageEventArgs e)
        {
            MessageChangedHandler handler = MessageChanged;
            if (handler != null)
            {
                // Invokes the delegates.
                handler(this, e);
            }
        }

        /// <summary>
        /// Log a debug message
        /// </summary>
        /// <param name="text">Message</param>
        public void Debug(string text)
        {
            WriteFormattedLog(LogLevel.DEBUG, text);
        }

        /// <summary>
        /// Log an error message
        /// </summary>
        /// <param name="text">Message</param>
        public void Error(string text)
        {
            WriteFormattedLog(LogLevel.ERROR, text);
        }

        /// <summary>
        /// Log a fatal error message
        /// </summary>
        /// <param name="text">Message</param>
        public void Fatal(string text)
        {
            WriteFormattedLog(LogLevel.FATAL, text);
        }

        /// <summary>
        /// Log an info message
        /// </summary>
        /// <param name="text">Message</param>
        public void Info(string text)
        {
            WriteFormattedLog(LogLevel.INFO, text);
        }

        /// <summary>
        /// Log a trace message
        /// </summary>
        /// <param name="text">Message</param>
        public void Trace(string text)
        {
            WriteFormattedLog(LogLevel.TRACE, text);
        }

        /// <summary>
        /// Log a waning message
        /// </summary>
        /// <param name="text">Message</param>
        public void Warning(string text)
        {
            WriteFormattedLog(LogLevel.WARNING, text);
        }

        /// <summary>
        /// Format a log message based on log level
        /// </summary>
        /// <param name="level">Log level</param>
        /// <param name="text">Log message</param>
        private void WriteFormattedLog(LogLevel level, string text)
        {
            message = text;

            string pretext = "";
            string logLevel = "";
            string dateTimeString = DateTime.Now.ToString(DatetimeFormat);

            switch (level)
            {
                case LogLevel.TRACE:
                    logLevel = "TRACE";
                    pretext = dateTimeString + " [TRACE]   ";
                    break;
                case LogLevel.INFO:
                    logLevel = "INFO";
                    pretext = dateTimeString + " [INFO]    ";
                    break;
                case LogLevel.DEBUG:
                    logLevel = "DEBUG";
                    pretext = dateTimeString + " [DEBUG]   ";
                    break;
                case LogLevel.WARNING:
                    logLevel = "WARNING";
                    pretext = dateTimeString + " [WARNING] ";
                    break;
                case LogLevel.ERROR:
                    logLevel = "ERROR";
                    pretext = dateTimeString + " [ERROR]   ";
                    break;
                case LogLevel.FATAL:
                    logLevel = "FATAL";
                    pretext = dateTimeString + " [FATAL]   ";
                    break;
                default:
                    pretext = "";
                    break;
            }

            WriteLine(pretext + text);
            this.OnMessageChanged(new MessageEventArgs(dateTimeString, logLevel, message));
        }

        /// <summary>
        /// Write a line of formatted log message into a log file
        /// </summary>
        /// <param name="text">Formatted log message</param>
        /// <param name="append">True to append, False to overwrite the file</param>
        /// <exception cref="System.IO.IOException"></exception>
        private void WriteLine(string text, bool append = true)
        {
            try
            {
                using (StreamWriter Writer = new StreamWriter(Filename, append, Encoding.UTF8))
                {
                    if (text != "") Writer.WriteLine(text);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Supported log level
        /// </summary>
        [Flags]
        private enum LogLevel
        {
            TRACE,
            INFO,
            DEBUG,
            WARNING,
            ERROR,
            FATAL
        }
    }
}