using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio.Wave;

namespace ASong.Playback
{
    public class SampleWaveProvider : IWaveProvider
    {
        private WaveFormat waveFormat;
        private sbyte[] pcmData;

        public SampleWaveProvider(SampleEditorForm.Sample sample)
        {
            waveFormat = new WaveFormat((int)sample.Pitch / 1024, 8, 1);
            pcmData = sample.Data;
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            int i = offset; // I think this will be good, but who knows?
            for (int k = 0; k < count; k++)
            {
                // Safecheck the PCM data position
                if (i + k >= pcmData.Length) i = 0;

                // Copy the waveform PCM to the thingy.
                buffer[k + offset] = (byte)(pcmData[k + i] + 0x80);

                // Increase position in PCM data.
            }
            return count;
        }

        public WaveFormat WaveFormat
        {
            get { return waveFormat; }
        }
    }
}
