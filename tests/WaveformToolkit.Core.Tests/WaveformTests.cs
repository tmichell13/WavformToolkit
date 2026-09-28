using System;
using System.Collections.Generic;
using System.Text;

namespace WaveformToolkit.Core.Tests
{
    public class WaveformTests
    {
        private static readonly double[] TwoSamples = [0.0, 1.0];

        [Theory]
        [Trait("Requirement", "FR-01")]
        [InlineData(0.0)]
        [InlineData(-1.0)]
        [InlineData(double.NaN)]
        [InlineData(double.PositiveInfinity)]
        
        public void Constructor_SampleRateNotPositiveFinite_ThrowsArgumentOutOfRange(double sampleRate)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Waveform(sampleRate, TwoSamples));
        }
    }
}
