using System;

using NUnit.Framework;

namespace MPT.Time.UnitTests
{
    [TestFixture]
    public class TimeLibraryTests
    {
        [TestCase(null, ExpectedResult = 0)]
        [TestCase("", ExpectedResult = 0)]
        [TestCase(" ", ExpectedResult = 0)]
        [TestCase("-1", ExpectedResult = 0)]
        [TestCase("0", ExpectedResult = 0)]
        [TestCase("5", ExpectedResult = 5)]
        [TestCase("30", ExpectedResult = 30)]
        [TestCase("00", ExpectedResult = 0)]
        [TestCase("06", ExpectedResult = 6)]
        [TestCase("0:00", ExpectedResult = 0)]
        [TestCase("00:00", ExpectedResult = 0)]
        [TestCase("00:06", ExpectedResult = 6)]
        [TestCase("00:15", ExpectedResult = 15)]
        [TestCase("5:30", ExpectedResult = 5 * 60 + 30)]
        [TestCase("05:30", ExpectedResult = 5 * 60 + 30)]
        [TestCase("10:30", ExpectedResult = 10 * 60 + 30)]
        [TestCase("0:00:00", ExpectedResult = 0)]
        [TestCase("00:00:00", ExpectedResult = 0)]
        [TestCase("00:00:30", ExpectedResult = 30)]
        [TestCase("00:05:30", ExpectedResult = 5 * 60 + 30)]
        [TestCase("00:10:30", ExpectedResult = 10 * 60 + 30)]
        [TestCase("2:10:30", ExpectedResult = 2 * 3600 + 10 * 60 + 30)]
        [TestCase("02:10:30", ExpectedResult = 2 * 3600 + 10 * 60 + 30)]
        [TestCase("15:10:30", ExpectedResult = 15 * 3600 + 10 * 60 + 30)]
        [TestCase("115:10:30", ExpectedResult = 115 * 3600 + 10 * 60 + 30)]
        public double ConvertHHMMSSToSeconds(string value)
        {
            return TimeLibrary.ConvertHHMMSSToSeconds(value);
        }

        [TestCase(null, ExpectedResult = 0)]
        [TestCase("", ExpectedResult = 0)]
        [TestCase(" ", ExpectedResult = 0)]
        [TestCase("-1", ExpectedResult = 0)]
        [TestCase("0", ExpectedResult = 0)]
        [TestCase("5", ExpectedResult = 5 / 60d)]
        [TestCase("30", ExpectedResult = 30 / 60d)]
        [TestCase("00", ExpectedResult = 0)]
        [TestCase("06", ExpectedResult = 6 / 60d)]
        [TestCase("0:00", ExpectedResult = 0)]
        [TestCase("00:00", ExpectedResult = 0)]
        [TestCase("00:06", ExpectedResult = 6 / 60d)]
        [TestCase("00:15", ExpectedResult = 15 / 60d)]
        [TestCase("5:30", ExpectedResult = 5 + 30 / 60d)]
        [TestCase("05:30", ExpectedResult = 5 + 30 / 60d)]
        [TestCase("10:30", ExpectedResult = 10 + 30 / 60d)]
        [TestCase("0:00:00", ExpectedResult = 0)]
        [TestCase("00:00:00", ExpectedResult = 0)]
        [TestCase("00:00:30", ExpectedResult = 30 / 60d)]
        [TestCase("00:05:30", ExpectedResult = 5 + 30 / 60d)]
        [TestCase("00:10:30", ExpectedResult = 10 + 30 / 60d)]
        [TestCase("2:10:30", ExpectedResult = 2 * 60 + 10 + 30 / 60d)]
        [TestCase("02:10:30", ExpectedResult = 2 * 60 + 10 + 30 / 60d)]
        [TestCase("15:10:30", ExpectedResult = 15 * 60 + 10 + 30 / 60d)]
        [TestCase("115:10:30", ExpectedResult = 115 * 60 + 10 + 30 / 60d)]
        public double ConvertHHMMSSToMinutes(string value)
        {
            return TimeLibrary.ConvertHHMMSSToMinutes(value);
        }

        [TestCase(-1, ExpectedResult = "-00:00:01")]
        [TestCase(0, ExpectedResult = "00:00:00")]
        [TestCase(5, ExpectedResult = "00:00:05")]
        [TestCase(15, ExpectedResult = "00:00:15")]
        [TestCase(30, ExpectedResult = "00:00:30")]
        [TestCase(5 * 60 + 30, ExpectedResult = "00:05:30")]
        [TestCase(10 * 60 + 30, ExpectedResult = "00:10:30")]
        [TestCase(2 * 3600 + 10 * 60 + 30, ExpectedResult = "02:10:30")]
        [TestCase(15 * 3600 + 10 * 60 + 30, ExpectedResult = "15:10:30")]
        [TestCase(115 * 3600 + 10 * 60 + 30, ExpectedResult = "115:10:30")]
        public string ConvertSecondsToHHMMSS(double value)
        {
            return TimeLibrary.ConvertSecondsToHHMMSS(value);
        }

        [TestCase(-1/60d, ExpectedResult = "-00:00:01")]
        [TestCase(0, ExpectedResult = "00:00:00")]
        [TestCase(5/60d, ExpectedResult = "00:00:05")]
        [TestCase(15/60d, ExpectedResult = "00:00:15")]
        [TestCase(30 / 60d, ExpectedResult = "00:00:30")]
        [TestCase(5 + 30 / 60d, ExpectedResult = "00:05:30")]
        [TestCase(10 + 30 / 60d, ExpectedResult = "00:10:30")]
        [TestCase(2 * 60 + 10 + 30 / 60d, ExpectedResult = "02:10:30")]
        [TestCase(15 * 60 + 10 + 30 / 60d, ExpectedResult = "15:10:30")]
        [TestCase(115 * 60 + 10 + 30 / 60d, ExpectedResult = "115:10:30")]
        public string ConvertMinutesToHHMMSS(double value)
        {
            return TimeLibrary.ConvertMinutesToHHMMSS(value);
        }
    }
}
