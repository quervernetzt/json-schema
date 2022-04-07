namespace JSONSchemaHelper;

/// <summary>
///     Resolve References command.
/// </summary>
public class ResolveReferencesCommand : CommandLineApplication, ICommand
{
    private readonly ResolveReferencesService ResolveReferenceService;
    private readonly CommandOption SchemaRootFilePath;
    private readonly CommandOption ResultOutputFilePath;

    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="_resolveReferenceService">
    ///     The service with the logic.
    /// </param>
    public ResolveReferencesCommand(ResolveReferencesService _resolveReferenceService)
    {
        this.ResolveReferenceService = _resolveReferenceService;
        
        Name = "resolve";
        Description = "Resolve references in a JSON schema and merge to a single file";
        SchemaRootFilePath = Option("-sp|--schemaFilePath <SCHEMA_PATH>", "Path to the schema file", CommandOptionType.SingleValue);
        ResultOutputFilePath = Option("-rp|--resultFilePath <RESULT_FILE_PATH>", "Path to the resulting file", CommandOptionType.SingleValue);
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
        if (SchemaRootFilePath.HasValue() == false
            || ResultOutputFilePath.HasValue() == false)
        {
            ShowHint();
            return 1;
        }

        ResolveReferenceService.Main(SchemaRootFilePath.Value(), ResultOutputFilePath.Value());

        return 0;
    }
}
