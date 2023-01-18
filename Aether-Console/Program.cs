// See https://aka.ms/new-console-template for more information
using System.Text;
using Aether_Console.Terminal;


Console.OutputEncoding = Console.InputEncoding = Encoding.Unicode;
Basis.Lines();

/*

Voice_Recorder vs = new Voice_Recorder();
vs.Processor();

/*
using NAudio.Wave;
using System.Collections;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set the recording threshold (in dB)
            float threshold = -20.0f;

            // Create a new WaveInEvent to record audio
            WaveInEvent waveIn = new WaveInEvent();

            string outputFilePath = $"{Environment.CurrentDirectory}\\runtimes\\Audios\\system_recorded_audio.wav";

            var waveFile = new WaveFileWriter(outputFilePath, waveIn.WaveFormat);

            List<float> BytesAfterSpecificTime = new();
            bool start = true;
            int starterCount = 0;
            int silenceCounter = 0;
            int Counter = 0;

            waveIn.DataAvailable += (sender, e) =>
            {
               // Calculate the volume of the recorded audio
               float max = 0;
               for (int index = 0; index < e.BytesRecorded; index += 2)
                {
                    short sample = (short)((e.Buffer[index + 1] << 8) |
                                               e.Buffer[index + 0]);
                    float sample32 = sample / 32768f;
                    max = Math.Max(max, sample32);
                }

                // Convert the volume (between 0 and 1) to dB
                float decibels = 20 * (float)Math.Log10(max);

                BytesAfterSpecificTime.Add(decibels);

                
                if (start == true)
                {
                    if (starterCount > 0)
                    {
                        start = (BytesAfterSpecificTime[starterCount - 1] < threshold  && BytesAfterSpecificTime[starterCount] < threshold);
                    }
                    starterCount++;
                }
                else
                {
                    waveFile.Write(e.Buffer, 0, e.BytesRecorded);

                    // Stop recording if the volume falls below the threshold 10 times
                        if (silenceCounter == 40)
                        {
                            waveIn.StopRecording();
                            waveFile.Close();
                            Console.WriteLine("finish");
                        }
                        else if (BytesAfterSpecificTime[Counter] < threshold )
                        {
                            silenceCounter++;
                        }
                        else
                        {
                            silenceCounter = 0;
                        }
                    
                }
                Counter++;
            };

            // Start recording audio
            waveIn.StartRecording();
            Console.ReadLine();
        }

    }
}
*/