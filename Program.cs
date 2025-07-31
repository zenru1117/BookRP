using Avalonia;
using System;
using System.Diagnostics;
using System.Threading;

namespace BookRP;

internal abstract class Program
{
    private const string MutexName = "BookRP_SingleInstance";
    private static Mutex? _mutex;
    
    // ReSharper disable once InconsistentNaming
    private const int SW_RESTORE = 9;
    
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    // public static void Main(string[] args) => BuildAvaloniaApp()
    //     .StartWithClassicDesktopLifetime(args);

    public static void Main(string[] args)
    {
        _mutex = new Mutex(true, MutexName, out var createdNew);

        if (!createdNew)
        {
            BringExistingInstanceToFront();
            return;
        }
        
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        GC.KeepAlive(_mutex);
    }

    private static void BringExistingInstanceToFront()
    {
        try
        {
            var current = Process.GetCurrentProcess();

            foreach (var process in Process.GetProcessesByName(current.ProcessName))
            {
                if (process.Id == current.Id) continue;
                
                var hWnd = process.MainWindowHandle;
                if (hWnd != IntPtr.Zero)
                {
                    NativeMethods.ShowWindow(hWnd, SW_RESTORE);
                    NativeMethods.SetForegroundWindow(hWnd);
                }

                break;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error brining instance to front: {ex.Message}");
        }
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    private static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}