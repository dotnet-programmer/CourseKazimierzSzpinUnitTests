namespace UnitTestSchool.UnitTests.Basics;

internal class PersonTests
{
	// testowanie stringów
	[Test]
	public void ToString_WhenValidProperties_ShouldReturnFullName()
	{
		Person person = new() { FirstName = "1", LastName = "2" };

		var result = person.ToString();

		result.Should().Contain("1 2");
		//result.Should().Be("1 2.");
		//result.Should().NotBeNull();
		//result.Should().BeNull();
		//result.Should().BeEmpty();
		//result.Should().NotBeEmpty();
		//result.Should().HaveLength(4);
		//result.Should().BeEquivalentTo("1 2.");
		//result.Should().NotContain("3");
		//result.Should().StartWith("1");
	}
}