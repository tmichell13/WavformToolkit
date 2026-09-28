using System;
using System.Collections.Generic;
using System.Text;

namespace WaveformToolkit.Core
{
    /// <summary>A uniformily sampled, single-channel signal.</summary>
    public sealed class Waveform
    {
        /// <summary>Creates a waveform.</summary>
        public Waveform(double sampleRate, IEnumerable<double> samples)
        {
            if(!double.IsFinite(sampleRate) || sampleRate <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(sampleRate),
                    sampleRate,
                    "Sample rate must be a positive, finite number.");
            }
        }
    }
}
