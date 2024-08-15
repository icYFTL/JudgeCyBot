using System.Globalization;

namespace JudgeBot.Presentation.Utils;

public static class Utils
{
    public static void SetUserCulture(string language)
    {
        var culture = new CultureInfo(language);
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;
    }
    
    public static string SanitizeMarkdown(string text)
    {
        var specialChars = new[] { "_", "*", "[", "]", "(", ")", "~", "`", ">", "#", "+", "-", "=", "|", "{", "}", ".", "!" };
        foreach (var ch in specialChars)
        {
            text = text.Replace(ch, "\\" + ch);
        }
        return text;
    }
}