using System.Text.RegularExpressions;

namespace JudgeBot.Application.Utils;

public static class TextUtils
{
    public static bool IsAlphaNumericSpecialString(string text)
    {
        var regex = new Regex(@"[a-zA-ZА-Яа-я0-9!,\.!@#\$%\^\&\*\(\)_\-\+=\\/]+", RegexOptions.Multiline);
        return regex.IsMatch(text);
    }
}