namespace JSONSchemaHelper;

/// <summary>
///     Logic to resolve references.
/// </summary>
public class ResolveReferencesService
{
    private ILogger<ResolveReferencesService> LoggerInstance;

    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="_loggerInstance">
    ///     The ILogger instance.
    /// </param>
    public ResolveReferencesService(
            ILogger<ResolveReferencesService> _loggerInstance
        )
    {
        LoggerInstance = _loggerInstance;
    }

    /// <summary>
    ///     Main method
    /// </summary>
    /// <param name="schemaRootFilePath">
    ///     The path to the root schema file.
    /// </param>
    /// <param name="resultOutputFilePath">
    ///     The path where to store the result.
    /// </param>
    public void Main(
            string schemaRootFilePath,
            string resultOutputFilePath
        )
    {
        // Get schema
        JSchema schema = GetSchema(schemaRootFilePath);
        LoggerInstance.LogInformation("Loaded schema.");

        // Write schema to file
        WriteSchemaToSingleFile(schema, resultOutputFilePath);
        LoggerInstance.LogInformation("Schema written to file.");

        LoggerInstance.LogInformation("Done.");
    }

    /// <summary>
    ///     Load schema.
    /// </summary>
    /// <param name="schemaRootFilePath">
    ///     The path to the root schema file.
    /// </param>
    /// <returns>
    ///     Returns the loaded schema.
    /// </returns>
    private JSchema GetSchema(string schemaRootFilePath)
    {
        JSchemaUrlResolver resolver = new JSchemaUrlResolver();

        JSchema schemaLoaded = JSchema.Parse(
            File.ReadAllText(schemaRootFilePath),
            new JSchemaReaderSettings
            {
                Resolver = resolver,
                BaseUri = new Uri(schemaRootFilePath)
            });

        return schemaLoaded;
    }

    /// <summary>
    ///     Write the schema to a single file.
    /// </summary>
    /// <param name="schema">
    ///     The loaded JSON schema.
    /// </param>
    /// <param name="resultOutputPath">
    ///     The file path to write to.
    /// </param>
    private void WriteSchemaToSingleFile(
            JSchema schema,
            string resultOutputPath
        )
    {
        // https://www.newtonsoft.com/jsonschema/help/html/SaveJsonSchemaToFile.htm
        using (StreamWriter file = File.CreateText(resultOutputPath))
        using (JsonTextWriter writer = new JsonTextWriter(file))
        {
            schema.WriteTo(writer);
        }
    }
}
