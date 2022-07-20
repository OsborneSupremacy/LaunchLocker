using System.Diagnostics;
using System.Text;
using LaunchLocker.Interface;

namespace LaunchLocker.Library;

public class Launcher : ILauncher
{
    public IConfiguration Configuration { get; set; }

    public Launcher(IConfiguration configuration)
    {
        Configuration = configuration ?? throw new System.ArgumentException(null, nameof(configuration));
    }

    public void Run()
    {
        var args = new StringBuilder();
        foreach (var cla in Configuration.TargetClas)
            args.Append($" {cla}");

        var process = new Process()
        {
            StartInfo = new ProcessStartInfo()
            {
                FileName = Configuration.TargetFileInfo.FullName,
                UseShellExecute = true,
                Arguments = args.ToString()
            }
        };

        process.Start();
        process.WaitForExit();
    }

}
