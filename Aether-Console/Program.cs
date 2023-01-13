// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using Nancy.Json;
using Newtonsoft.Json;
using Aether_Console.Classes_JSON;
using System.Text;
using Aether_Console.Terminal;
using System.Speech.Recognition;
using System.Globalization;
using System.Diagnostics;

/*
string s = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf("Aether-Console"));

ProcessStartInfo startInfo = new ProcessStartInfo();
startInfo.CreateNoWindow = true;
startInfo.UseShellExecute = false;
startInfo.WindowStyle = ProcessWindowStyle.Hidden;
startInfo.FileName = "cmd.exe";
startInfo.Arguments = @"/C python-starter.exe /quiet Include_test=0 InstallAllUsers=1 TargetDir=C:\HTL\3BHIF\Aether\Project-Aether\Python_test DefaultAllUsersTargetDir=C:\HTL\3BHIF\Aether\Project-Aether\Python_test DefaultJustForMeTargetDir=C:\HTL\3BHIF\Aether\Project-Aether\Python_test";
startInfo.WorkingDirectory = $"{s}\\Python_test";

// Start the process with the info we specified.
// Call WaitForExit and then the using statement will close.
using (Process exeProcess = Process.Start(startInfo))
{
    exeProcess.WaitForExit();
}

System.IO.File.Move($"{s}\\Python_test\\python.exe", $"{s}\\Python_test\\python399.exe");

var name = "PATH";
var scope = EnvironmentVariableTarget.Machine; // or User
var oldValue = Environment.GetEnvironmentVariable(name, scope);
if (!oldValue.Contains($"{s}\\Python_test") && !!oldValue.Contains($"{s}\\Python_test\\Scripts") && !!oldValue.Contains($"{s}\\Python_test\\ffmpeg\\bin"))
{
    var newValue = oldValue + @$";{s}\Python_test;{s}\Python_test\Scripts;{s}\Python_test\ffmpeg\bin";
    Environment.SetEnvironmentVariable(name, newValue, scope);
}

startInfo.Arguments = "/C python399 -m pip install torch torchvision torchaudio";

// Start the process with the info we specified.
// Call WaitForExit and then the using statement will close.
using (Process exeProcess = Process.Start(startInfo))
{
    exeProcess.WaitForExit();
}

startInfo.Arguments = "/C python399 -m pip install setuptools-rust";

// Start the process with the info we specified.
// Call WaitForExit and then the using statement will close.
using (Process exeProcess = Process.Start(startInfo))
{
    exeProcess.WaitForExit();
}

startInfo.Arguments = "/C python399 -m pip install git+https://github.com/openai/whisper.git";

// Start the process with the info we specified.
// Call WaitForExit and then the using statement will close.
using (Process exeProcess = Process.Start(startInfo))
{
    exeProcess.WaitForExit();
}

*/


Console.OutputEncoding = Console.InputEncoding = Encoding.Unicode;
Basis.Lines();


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