using Aether_Fusion.Terminal;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aether_Console.Terminal
{
    class Voice_Recorder
    {
        public void VoiceRecorder()
        {
            // Set the recording threshold (in dB)
            float threshold = -20.0f;

            // Create a new WaveInEvent to record audio
            WaveInEvent waveIn = new WaveInEvent();

            string outputFilePath = $"{Environment.CurrentDirectory}\\system_recorded_audio.wav";

            var waveFile = new WaveFileWriter(outputFilePath, waveIn.WaveFormat);

            List<float> BytesAfterSpecificTime = new();
            bool start = true;
            int starterCount = 0;
            int silenceCounter = 0;
            int Counter = 0;
            bool stop = false;

            waveIn.DataAvailable += (sender, e) =>
            {
                if (stop == false)
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

                    waveFile.Write(e.Buffer, 0, e.BytesRecorded);

                    if (start == true)
                    {
                        if (starterCount > 0)
                        {
                            start = (BytesAfterSpecificTime[starterCount - 1] < threshold && BytesAfterSpecificTime[starterCount] < threshold);
                        }
                        starterCount++;
                    }
                    else
                    {

                        // Stop recording if the volume falls below the threshold 10 times
                        if (silenceCounter == 20)
                        {
                            waveIn.StopRecording();
                            waveIn.Dispose();
                            waveFile.Close();
                            Console.WriteLine("finish");
                            stop = true;
                            //Console.WriteLine("finish");
                        }
                        else if (BytesAfterSpecificTime[Counter] < threshold)
                        {
                            silenceCounter++;
                        }
                        else
                        {
                            silenceCounter = 0;
                        }

                    }
                    Counter++;
                }
                    waveIn.RecordingStopped += (sender, e) =>
                    {
                        // Stop the Console.ReadLine() method when the recording stops
                        string text = Processor();
                    };
            };

            // Start recording audio
            waveIn.StartRecording();
            Console.ReadLine();
        }

        public string Processor()
        {
            string downloadsPath = KnownFolders.GetPath(KnownFolder.Downloads);
            if (File.Exists(@$"{Environment.CurrentDirectory}\system_recorded_audio.wav"))
            {
                File.Delete(@$"{Environment.CurrentDirectory}\system_recorded_audio.wav");
            }
            File.Move(@$"downloadsPath\system_recorded_audio.wav", @$"{Environment.CurrentDirectory}\system_recorded_audio.wav");
            bool file_exister = false;
            string s = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf("Aether_Fusion"));
            if (File.Exists($"{s}\\Aether-Console\\bin\\Debug\\net6.0\\audio_text.txt"))
            {
                File.Delete($"{s}\\Aether-Console\\bin\\Debug\\net6.0\\audio_text.txt");
            }
            // Console.WriteLine($"{s}Python\\python.exe");
            string arg = string.Format($"{s}\\Python\\app.py"); // Path to the Python code
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo($"{s}\\Python\\python399.exe", arg);
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = false; // Hide the command line window
            p.StartInfo.RedirectStandardOutput = false;
            p.StartInfo.RedirectStandardError = false;
            Process processChild = Process.Start(p.StartInfo);
            while (file_exister == false)
            {
                if (File.Exists($"{s}\\Aether-Console\\bin\\Debug\\net6.0\\audio_text.txt"))
                {
                    Thread.Sleep(3000);
                    file_exister = true; 
                }
            }
            string text = System.IO.File.ReadAllText($"{s}\\Aether-Console\\bin\\Debug\\net6.0\\audio_text.txt");
            return text;
        }
    }
}
