namespace JSONSchemaHelper;

/// <summary>
///     Main class.
/// </summary>
public class Program
{
    /// <summary>
    ///     Entry method.
    /// </summary>
    /// <param name="args">
    ///     Input arguments.
    /// </param>
    public static void Main(string[] args)
    {
        IServiceProvider serviceProvider = Startup.Initialize();

        CommandLineApplicationWithIOC commandLineApp = new CommandLineApplicationWithIOC(serviceProvider);
        commandLineApp.HelpOption("-?|-h|--help");
        commandLineApp.VersionOption("--version", "1.0.0");
        // If no command
        commandLineApp.OnExecute(() =>
        {
            commandLineApp.ShowHelp();
            return 0;
        });

        try
        {
            commandLineApp.Execute(args);
        }
        catch (CommandParsingException ex)
        {
            commandLineApp.ShowHelp();
        }
    }
}
