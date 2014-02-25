using System;
using System.Text;

[assembly: Anotar.Custom.LogMinimalMessage]

public static class LoggerFactory
{
    public static Action<string> LogInfo { get; set; }
    public static Action<string> LogWarn { get; set; }
    public static Action<string> LogError { get; set; }

    public static Logger GetLogger<T>()
    {
        return new Logger();
    }
}

public class Logger
{
    public void Information(string format, params object[] args)
    {
        if (IsInformationEnabled)
            LoggerFactory.LogInfo(string.Format(format, args));
    }

    public void Information(Exception exception, string format, params object[] args)
    {
        if (IsInformationEnabled)
            LoggerFactory.LogInfo(string.Format(format, args) + Environment.NewLine + ExceptionToString(exception));
    }

    public bool IsInformationEnabled { get { return LoggerFactory.LogInfo != null; } }

    public void Warning(string format, params object[] args)
    {
        if (IsWarningEnabled)
            LoggerFactory.LogWarn(string.Format(format, args));
    }

    public void Warning(Exception exception, string format, params object[] args)
    {
        if (IsWarningEnabled)
            LoggerFactory.LogWarn(string.Format(format, args) + Environment.NewLine + ExceptionToString(exception));
    }

    public bool IsWarningEnabled { get { return LoggerFactory.LogWarn != null; } }

    public void Error(string format, params object[] args)
    {
        if (IsErrorEnabled)
            LoggerFactory.LogError(string.Format(format, args));
    }

    public void Error(Exception exception, string format, params object[] args)
    {
        if (IsErrorEnabled)
            LoggerFactory.LogError(string.Format(format, args) + Environment.NewLine + ExceptionToString(exception));
    }

    public bool IsErrorEnabled { get { return LoggerFactory.LogError != null; } }

    private string ExceptionToString(Exception exception)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(exception.GetType().FullName + ":");
        while (exception != null)
        {
            stringBuilder.AppendLine("   " + exception.Message);

            foreach (var i in exception.Data)
            {
                stringBuilder.AppendLine("Data:");
                stringBuilder.AppendLine(i.ToString());
            }

            if (exception.StackTrace != null)
            {
                stringBuilder.AppendLine("StackTrace:");
                stringBuilder.AppendLine(exception.StackTrace);
            }

            if (exception.Source != null)
            {
                stringBuilder.AppendLine("Source:");
                stringBuilder.AppendLine("   " + exception.Source);
            }

            if (exception.TargetSite != null)
            {
                stringBuilder.AppendLine("TargetSite:");
                stringBuilder.AppendLine("   " + exception.TargetSite.ToString());
            }

            exception = exception.InnerException;
        }

        return stringBuilder.ToString();
    }
}