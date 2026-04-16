namespace TradingBotApp;

public static class CrashLogger
{
    private static string LogPath => Path.Combine(
        FileSystem.AppDataDirectory, "crash.log");

    public static void Inicializar()
    {
        AppDomain.CurrentDomain.UnhandledException += (_, args) =>
        {
            var ex = args.ExceptionObject as Exception;
            Escribir($"[UnhandledException] {ex?.Message}\n{ex?.StackTrace}");
        };

        TaskScheduler.UnobservedTaskException += (_, args) =>
        {
            Escribir($"[TaskException] {args.Exception?.Message}\n{args.Exception?.StackTrace}");
            args.SetObserved();
        };
    }

    public static void Escribir(string mensaje)
    {
        try
        {
            var linea = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {mensaje}\n";
            File.AppendAllText(LogPath, linea);
        }
        catch { }
    }

    public static string Leer()
    {
        try
        {
            return File.Exists(LogPath)
                ? File.ReadAllText(LogPath)
                : "Sin logs";
        }
        catch { return "Error leyendo log"; }
    }

    public static void Limpiar()
    {
        try { File.Delete(LogPath); } catch { }
    }
}
