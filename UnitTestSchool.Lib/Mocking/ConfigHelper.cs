using System.Text.Json;

namespace UnitTestSchool.Lib.Mocking;

public class ConfigHelper
{
	public string GetConnectionString()
	{
		var configFromFile = File.ReadAllText("config.txt");
		var config = JsonSerializer.Deserialize<Config>(configFromFile); // JsonConvert.DeserializeObject<Config>(configFromFile);
		return config.ConnectionString;
	}
}
