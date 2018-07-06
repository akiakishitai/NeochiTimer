
namespace Converter
{
  using System;
  using System.Text.RegularExpressions;

  public static class DateTimeConverter
  {
    /// <summary>
    /// 時間指定の入力を受け取り、時刻指定もしくは時間経過後の設定を行ったDateTimeを返す。
    /// </summary>
    /// <param name="input">24時間指定(H:mm)または/時分経過後指定(+3h, +24m,..)</param>
    /// <returns>DateTime variable of setting clock.</returns>
    /// <exception cref="System.FormatException">入力フォーマットが異なる場合。</exception>
    public static DateTime convert(string input)
    {
      switch(input)
      {
        case string x when Regex.IsMatch(x, @"^\+[0-9]+"):
          return later(ref x);
        case string x when Regex.IsMatch(input, @"[0-9]+:[0-9]+"):
          return date(ref x);
        case string x when x.Equals("now"):
          return DateTime.Now;
        default:
          throw new System.FormatException($"Wrong format of time. ==> {input}.");
      }
    }

    /// <summary>
    /// 時刻と日付の調整。
    /// 指定時刻が現在時刻より前ならば、明日の日付と解釈する。
    /// </summary>
    /// <param name="input">String of time format "H:mm".</param>
    /// <exception cref="System.FormatException">入力フォーマット（H:mm）と異なる場合。</exception>
    private static DateTime date(ref string input)
    {
      DateTime time;

      time = DateTime.ParseExact(input, "H:mm", null);
      if(time < DateTime.Now) { time = time.AddDays(1.0); }

      return time;
    }

    /// <summary>
    /// ○時間/分後のDateTimeを返す
    /// </summary>
    /// <param name="input">String of time format "+3h", "+10m".</param>
    /// <exception cref="System.FormatException">入力に数値が含まれない場合。</exception>
    private static DateTime later(ref string input)
    {
      Func<string, string, int> splitInt =
      (str, pattern) => {
        var match = Regex.Match(str, pattern);
        switch(match.Success)
        {
          case true:
            var matchStr = match.Value;
            return Int32.Parse( matchStr.Remove(matchStr.Length - 1) );
          default: return 0;
        }
      };

      int hour = splitInt(input, @"[0-9]+h");
      int minutes = splitInt(input, @"[0-9]+m");
      var time = hour * 60 + minutes;
      return DateTime.Now.Add(TimeSpan.FromMinutes(time));
    }
  }
}