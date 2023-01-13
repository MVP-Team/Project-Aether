using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aether_Console.Terminal
{
    class Installation
{
        public static void installation()
        {
            string s = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf("Aether-Console"));

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
            var scope = EnvironmentVariableTarget.Machine; // or User
            var oldValue = Environment.GetEnvironmentVariable(name, scope);
            if (!oldValue.Contains($"{s}\\Python") && !!oldValue.Contains($"{s}\\Python\\Scripts") && !!oldValue.Contains($"{s}\\Python\\ffmpeg\\bin"))
            {
                var newValue = oldValue + @$";{s}\Python;{s}\Python\Scripts;{s}\Python\ffmpeg\bin";
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
        }
}
}
