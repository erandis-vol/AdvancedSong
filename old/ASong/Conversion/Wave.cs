using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ASong.Conversion
{
    //////////////////////////////////////////////////////////////////////////////
    // Borrows code from wav2gba.cpp by Dark Fader                              //
    //////////////////////////////////////////////////////////////////////////////

    public static class Wave
    {
        public static SampleEditorForm.Sample wav2gba(string wave)
        {
            BinaryReader br = new BinaryReader(File.OpenRead(wave));
            try // Attempt to read the file
            {
                // Read Wave header
                WaveHeader hdr = new WaveHeader();
                if ((hdr.riff_id = Encoding.UTF8.GetString(br.ReadBytes(4))) != "RIFF")
                    throw new Exception("Bad RIFF header!");
                hdr.riff_len = br.ReadUInt32();

                if ((hdr.wave_id = Encoding.UTF8.GetString(br.ReadBytes(4))) != "WAVE")
                    throw new Exception("Bad WAVE header!");

                if ((hdr.fmt_id = Encoding.UTF8.GetString(br.ReadBytes(4))) != "fmt ")
                    throw new Exception("Bad fmt header!");
                hdr.fmt_len = br.ReadUInt32();
                if ((hdr.compression = br.ReadUInt16()) != 0x1)
                    throw new Exception("Cannot convert a compressed wave!");
                if ((hdr.channels = br.ReadUInt16()) != 1)
                    throw new Exception("Cannot convert a wave with more than one channel!");
                hdr.samplerate = br.ReadUInt32();
                hdr.byterate = br.ReadUInt32();
                hdr.blockalign = br.ReadUInt16();
                hdr.bits = br.ReadUInt16(); // 8 or 16 allowed for now
                //if ((hdr.bits = br.ReadUInt16()) != 8)
                //    throw new Exception("Cannot convert a wave that is not 8 bits!");

                if ((hdr.data_id = Encoding.UTF8.GetString(br.ReadBytes(4))) != "data")
                    throw new Exception("Bad data header!");
                hdr.data_len = br.ReadUInt32();
                hdr.wave_data = (uint)br.BaseStream.Position; // offset

                // We only convert single channel samples for now.
                SampleEditorForm.Sample sample = new SampleEditorForm.Sample();
                sample.Compressed = false;
                sample.Looped = false;
                sample.LoopStart = 0;
                sample.Pitch = hdr.samplerate * 1024;
                sample.OriginalSize = hdr.data_len / hdr.blockalign; // if 1 channel, should be data_len / bits
                sample.Data = new sbyte[sample.OriginalSize];

                for (int i = 0; i < hdr.data_len / hdr.blockalign; i++)
                {
                    if (hdr.bits == 8) // 8-bit unsigned
                    {
                        sample.Data[i] = (sbyte)(br.ReadByte() - 128);
                    }
                    else if (hdr.bits == 16) // 16-bit signed
                    {
                        byte bit8 = (byte)((br.ReadInt16() >> 8) + 128);
                        sample.Data[i] = (sbyte)(bit8 - 128);
                    }
                    else
                    {
                        throw new Exception("Illegal bit format!");
                    }
                }

                // Read the channels
                /*List<byte>[] channel_data = new List<byte>[hdr.channels];
                for (int i = 0; i < hdr.channels; i++) // Setup
                { channel_data[i] = new List<byte>(); }

                // Read the channel data into the lists
                br.BaseStream.Seek(hdr.wave_data, SeekOrigin.Begin);
                for (int i = 0; i < hdr.data_len / hdr.channels; i++)
                {
                    for (int channel = 0; channel < hdr.channels; channel++)
                    {
                        channel_data[channel].Add(br.ReadByte());
                        if (br.BaseStream.Position >= br.BaseStream.Length) break; // Keep in?
                    }
                }

                // Now, read the channels.
                List<SampleEditorForm.Sample> samples = new List<SampleEditorForm.Sample>();
                for (int channel = 0; channel < hdr.channels; channel++)
                {
                    // Setup the sample
                    SampleEditorForm.Sample sample = new SampleEditorForm.Sample();
                    sample.Compressed = false;
                    sample.Looped = false;
                    sample.LoopStart = 0;
                    sample.Pitch = hdr.samplerate << 10;
                    sample.OriginalSize = (uint)channel_data[channel].Count;
                    sample.Data = new sbyte[channel_data[channel].Count];

                    // And then, convert the 8 bit wave to gba
                    //br.BaseStream.Seek(hdr.wave_data + channel, SeekOrigin.Begin);
                    for (int i = 0; i < sample.Data.Length; i++)
                    {
                        //sample.Data[i] = (sbyte)(br.ReadByte() - 0x80);
                        //br.BaseStream.Seek(hdr.channels - 1, SeekOrigin.Current);
                        sample.Data[i] = (sbyte)(channel_data[channel][i] - 0x80);
                    }

                    // Done?
                    samples.Add(sample);
                } */

                br.Dispose();
                return sample;
            }
            catch (Exception ex)
            {
                br.Dispose();

                throw ex;
            }
        }

        public static void gba2wav(string wave, SampleEditorForm.Sample sample)
        {
            BinaryWriter bw = new BinaryWriter(File.Create(wave));
            
            // Write headers
            bw.Write(Encoding.UTF8.GetBytes("RIFF"));
            bw.Write(0); // file size - 8 -- set when finished
            bw.Write(Encoding.UTF8.GetBytes("WAVE"));
            bw.Write(Encoding.UTF8.GetBytes("fmt "));
            bw.Write((uint)16); // fmt size -- constant for us
            bw.Write((ushort)1);
            bw.Write((ushort)1);
            bw.Write((uint)(sample.Pitch / 1024));
            bw.Write((uint)((sample.Pitch / 1024) * 1)); // AvgBytesPerSec = SampleRate * BlockAlign
            bw.Write((ushort)/*(8 / 8 * 1)*/ 1); // BlockAlign = SignificantBitsPerSample / 8 * NumChannels
            bw.Write((ushort)8); // bits
            // that's all for now

            // Write channel
            bw.Write(Encoding.UTF8.GetBytes("data"));
            bw.Write((uint)sample.Data.Length); // this should be the one
            for (int i = 0; i < sample.Data.Length; i++)
            {
                bw.Write((byte)(sample.Data[i] + 128)); // Convert to byte from sbyte
            }

            // Fix header sizes
            bw.BaseStream.Seek(4L, SeekOrigin.Begin);
            bw.Write((uint)(bw.BaseStream.Length - 8));

            // All done~
            bw.Dispose();
        }
    }

    public struct WaveHeader
    {
        public string	riff_id; // "RIFF"
	    public uint		riff_len;

        public string wave_id; // "WAVE"

        public string fmt_id; // "fmt "
        public uint fmt_len;
        public ushort compression;
        public ushort channels; // 1=mono, 2=stereo
        public uint samplerate;
        public uint byterate;
        public ushort blockalign;
        public ushort bits; // 8/16 bits

        public string data_id; // "data"
        public uint data_len;

		//u8		padding;

        public uint wave_data;
    }
}
