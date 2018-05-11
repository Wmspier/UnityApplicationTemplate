using UnityEngine;

public enum LogColor
{
    red,
    green,
    blue,
    yellow
}

public static class ConsoleLogger {

    public static void ColoredLog(string message, LogColor color)
    {
        Debug.Log("<color=" + color.ToString() + ">" + message + "</color>");
    }

    public static void LogObjectFields(object o)
    {
        var log = string.Format("<b>{0}:\n</b>", o);
        foreach (var field in o.GetType().GetFields())
        {
            log += string.Format("{0}: {1} \n", field.Name, field.GetValue(o));
        }
        Debug.Log(log);
    }
}
