namespace PAWG1.Mvc.Models;

/// <summary>
/// Represents the application settings from the configuration.
/// </summary>
public class AppSettings
{
	/// <summary>
	/// Gets or sets the logging settings.
	/// </summary>
	public LoggingSettings Logging { get; set; }

	/// <summary>
	/// Gets or sets the allowed hosts for the application.
	/// </summary>
	public string AllowedHosts { get; set; }

	/// <summary>
	/// Gets or sets the base URL for the REST API.
	/// </summary>
	public string RestApi { get; set; }
}

/// <summary>
/// Represents the logging settings in the application.
/// </summary>
public class LoggingSettings
{
	/// <summary>
	/// Gets or sets the log level settings.
	/// </summary>
	public LogLevelSettings LogLevel { get; set; }
}

/// <summary>
/// Represents the log level settings.
/// </summary>
public class LogLevelSettings
{
	/// <summary>
	/// Gets or sets the default log level.
	/// </summary>
	public string Default { get; set; }

	/// <summary>
	/// Gets or sets the log level for Microsoft.AspNetCore logs.
	/// </summary>
	public string MicrosoftAspNetCore { get; set; }  // Note: Use PascalCase for property names
}
