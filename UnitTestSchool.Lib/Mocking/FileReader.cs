namespace UnitTestSchool.Lib.Mocking;

// klasa do używania w kodzie produkcyjnym, w miejscu występowania File.ReadAllText
public class FileReader : IFileReader
{
	public string Read(string fileName)
		=> File.ReadAllText(fileName);
}