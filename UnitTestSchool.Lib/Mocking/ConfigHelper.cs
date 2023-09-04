using Newtonsoft.Json;

namespace UnitTestSchool.Lib.Mocking;

public class ConfigHelper
{
	private readonly IFileReader _fileReader;

	public ConfigHelper(IFileReader fileReader) => _fileReader = fileReader;

	public string GetConnectionString()
	{
		var configFromFile = _fileReader.Read("config.txt");
		//var config = JsonSerializer.Deserialize<Config>(configFromFile);
		var config = JsonConvert.DeserializeObject<Config>(configFromFile);

		return config is null ? throw new Exception("Incorrect paring config") : config.ConnectionString;
	}
}