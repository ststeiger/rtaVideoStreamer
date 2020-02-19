
namespace rtaStreamingServer
{


    public class BrowserWrapper
    {

        // https://brockallen.com/2016/09/24/process-start-for-urls-on-net-core/
        public static void OpenBrowser(string url)
        {
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
                {
                    try
                    {
                        
                        try
                        {
                            // /opt/firefox/firefox
                            System.Diagnostics.Process.Start("google-chrome", url);
                        }
                        catch
                        {
                            System.Diagnostics.Process.Start("google-chrome", url);
                            System.Diagnostics.Process.Start("xdg-open", url);
                        }
                        
                        // System.Diagnostics.Process.Start("xdg-open", url);
                    }
                    catch
                    {
                        System.Diagnostics.Process.Start("openvt", url);
                    }
                    
                }
                else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX))
                {
                    System.Diagnostics.Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }


    }
}
