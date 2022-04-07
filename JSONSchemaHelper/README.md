# JSON schema helper

- Application with helper methods to work with JSON schema resources.


# Resolve references and merge to one file

- Take a root schema with references as input, merge it and write it back as single file.

- Execute `.\JSONSchemaHelper.exe resolve --schemaFilePath <root schema file path> --resultFilePath <output file path>` via a command line from the folder `.\application`

- Example: `.\JSONSchemaHelper.exe resolve --schemaFilePath "C:\xxx\JSONSchemaHelper\application\schemas\root-schema.json" --resultFilePath "C:\xxx\JSONSchemaHelper\application\schemas\root-schema-resolved.json"`


# Validate JSON file with schema

- Validate a JSON file with a JSON schema.

- Execute `.\JSONSchemaHelper.exe validate --schemaFilePath <root schema file path> --jsonFilePath <output file path>` via a command line from the folder `.\application`

- Example: `.\JSONSchemaHelper.exe validate --schemaFilePath "C:\xxx\JSONSchemaHelper\application\schemas\root-schema.json" --jsonFilePath "C:\xxx\JSONSchemaHelper\application\schemas\test-documents\test-file.json"`