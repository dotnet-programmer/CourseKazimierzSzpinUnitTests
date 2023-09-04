namespace UnitTestSchool.Lib.Mocking;

public class FileReader : IFileReader
{
	public string Read(string fileName) => File.ReadAllText(fileName);
}