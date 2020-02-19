
namespace rtaStreamingServer
{


    public class BrowserWrapper
    {

        private class Syscalls
        {

            private const string LIBC = "libc";

            [System.Runtime.InteropServices.DllImport(LIBC, SetLastError = true)]
            public static extern uint getuid();

            [System.Runtime.InteropServices.DllImport(LIBC, SetLastError = true)]
            public static extern uint geteuid();

        }



        private class Cmd
        {
            internal string FileName;
            internal string Arguments;
            internal bool CreateNoWindow;
        }



        private static System.Collections.Generic.IEnumerable<Cmd> GetOpenUrlCommands(string url)
        {
            string additionalFlags = "";

            if (System.Environment.OSVersion.Platform == System.PlatformID.Unix)
            {
                try
                {
                    if (Syscalls.getuid() == 0 || Syscalls.geteuid() == 0)
                        additionalFlags += " --no-sandbox ";
                }
                catch { }
            } // End if (System.Environment.OSVersion.Platform == System.PlatformID.Unix)
            

            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
            {
                // https://brockallen.com/2016/09/24/process-start-for-urls-on-net-core/
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                yield return new Cmd() { FileName = "cmd", Arguments = $"/c start {url.Replace("&", "^&")}", CreateNoWindow = true };
                yield return new Cmd() { FileName = "msedge.exe", Arguments = url };
            }

            yield return new Cmd() { FileName = "google-chrome", Arguments = additionalFlags + url };
            yield return new Cmd() { FileName = "chromium-browser", Arguments = additionalFlags + url };
            yield return new Cmd() { FileName = "chrome", Arguments = additionalFlags + url };
            yield return new Cmd() { FileName = "chromium", Arguments = additionalFlags + url };
            yield return new Cmd() { FileName = "firefox", Arguments = url };

            if (System.Environment.OSVersion.Platform == System.PlatformID.Unix)
            {
                yield return new Cmd() { FileName = "xdg-open", Arguments = url };
                yield return new Cmd() { FileName = "openvt", Arguments = url };
            } // End if (System.Environment.OSVersion.Platform == System.PlatformID.Unix) 

            yield return new Cmd() { FileName = "open", Arguments = url };
            yield return new Cmd() { FileName = "start", Arguments = url };
        } // End Function GetCommands
        

        public static void OpenBrowser(string url)
        {
            System.Collections.Generic.IEnumerable<Cmd> urlOpenCommands = GetOpenUrlCommands(url);

            foreach (Cmd thisCommand in urlOpenCommands)
            {
                try
                {
                    System.Diagnostics.Process.Start(
                        new System.Diagnostics.ProcessStartInfo(thisCommand.FileName, thisCommand.Arguments)
                            { CreateNoWindow = thisCommand.CreateNoWindow }
                    );
                    break;
                }
                catch
                { }
            } // Next cmd 

        } // End Sub OpenBrowser 


        private static System.Threading.Tasks.Task<int> RunProcessAsync(System.Diagnostics.ProcessStartInfo startInfo)
        {
            System.Threading.Tasks.TaskCompletionSource<int> tcs = new System.Threading.Tasks.TaskCompletionSource<int>();

            System.Diagnostics.Process process = new System.Diagnostics.Process
            {
                StartInfo = startInfo,
                EnableRaisingEvents = true
            };

            process.Exited += delegate(object sender, System.EventArgs args)
            {
                tcs.SetResult(process.ExitCode);
                process.Dispose();
            };

            try
            {
                process.Start();
            }
            catch (System.Exception ex)
            {
                tcs.SetException(ex);
            }
            
            return tcs.Task;
        } // End Task RunProcessAsync 


    } // End Class BrowserWrapper 


} // End Namespace rtaStreamingServer 
