namespace JSONSchemaHelper;

/// <summary>
///     Validate JSON file command.
/// </summary>
public class ValidateJSONFileCommand : CommandLineApplication, ICommand
{
    private readonly ValidateJSONFileService ValidateSchemaService;
    private readonly CommandOption SchemaFilePath;
    private readonly CommandOption JSONFilePath;

    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="_validateSchemaService">
    ///     The service with the logic.
    /// </param>
    public ValidateJSONFileCommand(ValidateJSONFileService _validateSchemaService)
    {
        this.ValidateSchemaService = _validateSchemaService;
        
        Name = "validate";
        Description = "Validate a JSON file with a JSON schema";
        SchemaFilePath = Option("-sp|--schemaFilePath <SCHEMA_PATH>", "Path to the schema file", CommandOptionType.SingleValue);
        JSONFilePath = Option("-jp|--jsonFilePath <JSON_FILE_PATH>", "Path to the JSON file to validate", CommandOptionType.SingleValue);
        HelpOption("-? | -h | --help");
        OnExecute(Execute);
    }

    /// <summary>
    ///     The execute method.
    /// </summary>
    /// <returns>
    ///     Returns a status int.
    /// </returns>
    public int Execute()
    {
        if (SchemaFilePath.HasValue() == false
            || JSONFilePath.HasValue() == false)
        {
            ShowHint();
            return 1;
        }

        ValidateSchemaService.Main(SchemaFilePath.Value(), JSONFilePath.Value());

        return 0;
    }
}
