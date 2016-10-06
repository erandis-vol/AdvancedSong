using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio.Wave;

namespace ASong.Playback
{
    public class WaveformWaveProvider : IWaveProvider
    {
        private WaveFormat waveFormat;
        private byte[] waveformData;

        public WaveformWaveProvider(byte[] waveform)
        {
            // That may be good...
            waveFormat = new WaveFormat(16000, 8, 1);
            waveformData = new byte[32];
            for (int i = 0; i < 32; i++)
            {
                //waveformData[i] = (byte)(waveform[i] * 8); // This is not the right way to change 4 bit to 8 bit.
                waveformData[i] = (byte)((waveform[i] << 4) - 128); // This should hopefully work?
            }
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            // This is just a simplified version of the Sample Read.
            int i = offset % 32; // I think this will be good, but who knows?
            for (int k = 0; k < count; k++)
            {
                // Copy the waveform PCM to the thingy.
                buffer[k + offset] = waveformData[i]; // This SHOULD be in 8 bit PCM, but who knows

                // Increase position in waveform data.
                i++;
                if (i >= 32) i = 0;
            }
            return count;
        }

        public WaveFormat WaveFormat
        {
            get { return waveFormat; }
        }
    }
}
