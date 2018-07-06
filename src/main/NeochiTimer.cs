

namespace NeochiTimer
{
  using System;
  using System.Threading.Tasks;
  using McMaster.Extensions.CommandLineUtils;
  using Converter;

  class Command
  {
    static void Main(string[] args)
    {
      CommandLineApplication.Execute<Command>(args);
    }

    [Argument(0, Name = "TIME (+2h, +30m, 23:30, now, ...)", Description = "Set time to sleep. 'now' is to sleep at once.")]
    public string Time { get; }

    /// <summary>
    /// 実行時処理の実装。
    /// </summary>
    private void OnExecute()
    {
      try
      {
        var time = DateTimeConverter.convert(Time);
        var span = time - DateTime.Now;
        Console.WriteLine($"[Info] {time} に寝落ちしようと思います。");
        sleepAsysnc(span).Wait();
      }
      catch(System.FormatException e)
      {
        Console.Error.WriteLine("[Error] 時間の書式が間違っています。");
        Console.Error.WriteLine($"{e}");
      }
    }

    /// <summary>
    /// 指定時間が経過するまで待機した後、スリープモードへ移行する。
    /// </summary>
    /// <param name="delay">現在時刻からスリープするまでの待機時間</param>
    private async Task sleepAsysnc(TimeSpan delay)
    {
      if (delay > TimeSpan.Zero) {
        await Task.Delay(delay);
      }

      Console.WriteLine("[GoodBye] zzz...");
      bool failer =
#if TEST
        true;
#else
        !SetSuspendState(false, false, false);
#endif
      if(failer)
      {
        Console.Error.WriteLine("[Error] 寝落ちできませんでした。");
      }
    }

    /// <summary>
    /// PCのスリープ/ハイバネートモードを実行させるため、
    /// SetSuspendState()をPowrprof.dllからインポートして使う。
    /// </summary>
    /// <param name="hibernate">true: ハイバネートモード, false: スリープモード.</param>
    /// <param name="forceCritical">trueならば実行中アプリに通知せず強制的にスリープ/ハイバネートへ移行する。</param>
    /// <param name="disableWakeEvent">デバイス等からシステムの復帰を無効化する。</param>
    [System.Runtime.InteropServices.DllImport("Powrprof.dll", SetLastError = true)]
    public static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);
  }
}
