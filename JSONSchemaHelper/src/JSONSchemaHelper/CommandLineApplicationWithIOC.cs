namespace JSONSchemaHelper;

/// <summary>
///     Set up command line application.
/// </summary>
public class CommandLineApplicationWithIOC : CommandLineApplication
{
    private readonly IServiceProvider serviceProvider;

    public CommandLineApplicationWithIOC(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
        RegisterCommands();
        Name = "JSONSchemaHelper.exe";
        FullName = "Resolve JSON Schema References to one file";
        Description = "Resolve schema references and merge to a single schema file";
    }

    private void RegisterCommands()
    {
        foreach (ICommand command in serviceProvider.GetServices<ICommand>())
        {
            CommandLineApplication commandLineApp = command as CommandLineApplication;

            if (commandLineApp == null)
            {
                throw new InvalidCastException("Commands must inherit from ICommand and CommandLineApplication");
            }

            Commands.Add(commandLineApp);
        }
    }
}
