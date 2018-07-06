#define TEST

using NUnit.Framework;

namespace Tests
{
  using System;
  using System.Collections;
  using Converter;

  public class Tests
  {
    [SetUp]
    public void Setup()
    {
    }

    [Test, TestCaseSource(typeof(TestDataClass), "timeInputs")]
    public void convertTest(string input)
    {
      var now = DateTime.Now;
      Assert.That(DateTimeConverter.convert(input), Is.GreaterThanOrEqualTo(now));
    }
  }

  /// For test case source class.
  public class TestDataClass
  {
    public static IEnumerable timeInputs
    {
      get
      {
        yield return new TestCaseData("+0m");
        yield return new TestCaseData("-10m");
        yield return new TestCaseData("+150m");
        yield return new TestCaseData("+0h");
        yield return new TestCaseData("+10h");
        yield return new TestCaseData("+40h");
        yield return new TestCaseData("+20");
        yield return new TestCaseData("+0a");
        yield return new TestCaseData("00:20");
        yield return new TestCaseData("1:30");
        yield return new TestCaseData("5:9");
        yield return new TestCaseData("22:00");
        yield return new TestCaseData("26:00");
        yield return new TestCaseData("+h");
      }
    }
  }
}