namespace JSONSchemaHelper;

/// <summary>
///     Inversion of Control class.
/// </summary>
public class Startup
{
    /// <summary>
    ///     The IoC initialization method.
    /// </summary>
    /// <returns>
    ///     Returns a Service Provider.
    /// </returns>
    public static IServiceProvider Initialize()
    {
        ServiceCollection serviceCollection = new ServiceCollection();

        // Build configuration
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Logging configuration
        serviceCollection.AddLogging(loggingBuilder => loggingBuilder
            .AddConsole()
            .AddConfiguration(configuration.GetSection("Logging"))
        );

        serviceCollection.AddScoped<ResolveReferencesService>();
        serviceCollection.AddScoped<ValidateJSONFileService>();

        serviceCollection.AddScoped<ICommand, ResolveReferencesCommand>();
        serviceCollection.AddScoped<ICommand, ValidateJSONFileCommand>();

        return serviceCollection.BuildServiceProvider();
    }
}

