namespace JSONSchemaHelper;

/// <summary>
///     Logic to validate schemas.
/// </summary>
public class ValidateJSONFileService
{
    private ILogger<ValidateJSONFileService> LoggerInstance;

    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="_loggerInstance">
    ///     The ILogger instance.
    /// </param>
    public ValidateJSONFileService(
            ILogger<ValidateJSONFileService> _loggerInstance
        )
    {
        LoggerInstance = _loggerInstance;
    }

    /// <summary>
    ///     Main method
    /// </summary>
    /// <param name="schemaFilePath">
    ///     The path to the schema file.
    /// </param>
    /// <param name="jsonFilePath">
    ///     The path where to the JSON file to validate.
    /// </param>
    public void Main(
            string schemaFilePath,
            string jsonFilePath
        )
    {
        LoggerInstance.LogInformation("Got arguments.");

        // Get schema
        JSchema schema = GetSchema(schemaFilePath);
        LoggerInstance.LogInformation("Loaded schema.");

        // Get JSON file.
        JToken jsonFile = GetJSONFileToValidate(jsonFilePath);
        LoggerInstance.LogInformation("Loaded JSON file to validate.");

        // Write schema to file
        ValidateSchema(schema, jsonFile);
        LoggerInstance.LogInformation("Validated JSON file.");

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
    ///     Get JSON file to validate.
    /// </summary>
    /// <param name="jsonToValidatePath">
    ///     The path to the JSON file.
    /// </param>
    /// <returns>
    ///     Returns the file as JToken.
    /// </returns>
    private JToken GetJSONFileToValidate(
        string jsonToValidatePath
    )
    {
        JToken? json = null;
        using (StreamReader jsonFile = File.OpenText(jsonToValidatePath))
        using (JsonTextReader jsonReader = new JsonTextReader(jsonFile) { DateParseHandling = DateParseHandling.None })
        {
            json = JToken.ReadFrom(jsonReader);
        }

        return json;
    }

    /// <summary>
    ///     Validate JSON file.
    /// </summary>
    /// <param name="schema">
    ///     The schema ´for validation.
    /// </param>
    /// <param name="jsonToValidate">
    ///     The JSON file to validate.
    /// </param>
    private void ValidateSchema(
            JSchema schema,
            JToken jsonToValidate
        )
    {
        IList<ValidationError> errorMessages;
        jsonToValidate.IsValid(schema, out errorMessages);

        if (errorMessages.Any())
        {
            LoggerInstance.LogInformation(string.Join("\r\n", errorMessages.Select(error => string.Join("\r\n", error.ChildErrors.Select(ce => ce.Message)))));
        }
        else
        {
            LoggerInstance.LogInformation("No errors detected.");
        }
    }
}
