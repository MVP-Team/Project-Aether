using System.Diagnostics;

namespace Aether_Fusion.Terminal
{
    public class TodoListOpen
    {

        public static void open() {
            string s = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf("Aether_Fusion"));

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = @$"/C dotnet todo.dll";
            startInfo.WorkingDirectory = $"{s}AetherTodo\\todo-tutorial\\Todo\\bin\\Debug\\net6.0";

            // Start the process with the info we specified.
            // Call WaitForExit and then the using statement will close.
            using (Process exeProcess = Process.Start(startInfo))
            {
                exeProcess.WaitForExit();
            }
        }

    }
}
