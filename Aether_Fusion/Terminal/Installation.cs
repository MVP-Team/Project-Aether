using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using Microsoft.VisualBasic.FileIO;

namespace Aether_Console.Terminal
{
    class Installation
{
        public static string installation()
        {
            string s = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf("Aether_Fusion"));

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = @$"/C python-starter.exe /quiet Include_test=0 InstallAllUsers=1 TargetDir={s}\Python DefaultAllUsersTargetDir={s}\Python DefaultJustForMeTargetDir={s}\Python";
            startInfo.WorkingDirectory = $"{s}\\Python";

            // Start the process with the info we specified.
            // Call WaitForExit and then the using statement will close.
            using (Process exeProcess = Process.Start(startInfo))
            {
                exeProcess.WaitForExit();
            }

            System.IO.File.Move($"{s}\\Python\\python.exe", $"{s}\\Python\\python399.exe");

            var name = "PATH";
            var scope = EnvironmentVariableTarget.User; // or User
            var Value = Environment.GetEnvironmentVariable(name, scope);
            Value = Value ?? @$"{s}\Python";
            if (!Value.Contains($"{s}\\Python") && !Value.Contains($"{s}\\Python\\Scripts"))
            {
                Value = Value + @$";{s}\Python";
                Value = Value + @$";{s}\Python\Scripts";

                Environment.SetEnvironmentVariable(name, Value, scope);
            }

            startInfo.Arguments = "/C python399 -m pip install wget";

            // Start the process with the info we specified.
            // Call WaitForExit and then the using statement will close.
            using (Process exeProcess = Process.Start(startInfo))
            {
                exeProcess.WaitForExit();
            }

            startInfo.Arguments = @$"/C python399 -m wget https://github.com//BtbN/FFmpeg-Builds/releases/download/latest/ffmpeg-master-latest-win64-gpl.zip -o {s}\Python";

            // Start the process with the info we specified.
            // Call WaitForExit and then the using statement will close.
            using (Process exeProcess = Process.Start(startInfo))
            {
                exeProcess.WaitForExit();
            }

            var zipPath = @$"{s}/Python/ffmpeg-master-latest-win64-gpl.zip";
            var extractPath = @$"{s}/Python";

            ZipFile.ExtractToDirectory(zipPath, extractPath);

            //Directory.Move(@$"{s}\Python\ffmpeg-master-latest-win64-gpl", @$"{s}\Python\ffmpeg");

            //FileSystem.RenameDirectory(@$"{s}\Python\ffmpeg-master-latest-win64-gpl", "ffmpeg");

            System.IO.File.Delete(zipPath);

            Value = Environment.GetEnvironmentVariable(name, scope);
            Value = Value ?? @$"{s}\Python";
            if (!Value.Contains($"{s}\\Python\\ffmpeg-master-latest-win64-gpl\\bin"))
            {
                Value = Value + @$";{s}\ffmpeg-master-latest-win64-gpl\bin";
                Environment.SetEnvironmentVariable(name, Value, scope);
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

            return "finished";
        }
}
}
