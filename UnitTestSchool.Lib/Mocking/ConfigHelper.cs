using System.Text.Json;

namespace UnitTestSchool.Lib.Mocking;

public class ConfigHelper
{
	// klasa ConfigHelper przed refaktoringiem
	public string GetConnectionStringOld()
	{
		// zewnętrzna zależność - pobieranie danych z pliku, trzeba się pozbyć i tutaj podstawić Mocka żeby napisać test do takiej metody
		// żeby to zastąpić, trzeba wprowadzić interfejs, żeby w testach zastąpić go Mockiem
		var configFromFile = File.ReadAllText("config.txt");
		var config = JsonSerializer.Deserialize<Config>(configFromFile);
		return config.ConnectionString;
	}


	// klasa przygotowana do testów:

	private readonly IFileReader _fileReader;

	// implementacja interfejsu w klasie produkcyjnej za pomocą Dependency Injection
	public ConfigHelper(IFileReader fileReader)
		=> _fileReader = fileReader;

	public string GetConnectionString()
	{
		string configFromFile = _fileReader.Read("config.txt");

		Config config = JsonSerializer.Deserialize<Config>(configFromFile);

		return config is null
			? throw new Exception("Incorrect parsing config")
			: config.ConnectionString;
	}
}