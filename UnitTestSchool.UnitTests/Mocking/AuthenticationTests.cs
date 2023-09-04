﻿namespace UnitTestSchool.UnitTests.Mocking;

internal class AuthenticationTests
{
	#region Moq składnia imperatywna

	[TestCase("1", "2")]
	public void Login_WhenCorrectData_ShouldReturnEmptyString_Imperative(string user, string password)
	{
		Mock<IUsersRepository> mockUserRepository = new();

		// zwraca True dla każdego stringa podstawionego jako argument:
		//mockUserRepository.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

		// testy dla konkretnych stringów - w większości przypadków to lepsze rozwiązanie niż ogólne IsAny<string>
		mockUserRepository.Setup(x => x.Login(user, password)).Returns(true);

		Authentication authentication = new(mockUserRepository.Object);
		var result = authentication.Login(user, password);
		result.Should().BeEmpty();
	}

	[TestCase("1", "2")]
	public void Login_WhenIncorrectData_ShouldReturnCorrectMessage_Imperative(string user, string password)
	{
		Mock<IUsersRepository> mockUserRepository = new();
		mockUserRepository.Setup(x => x.Login(user, password)).Returns(false);
		Authentication authentication = new(mockUserRepository.Object);
		var result = authentication.Login(user, password);
		result.Should().Contain("User or password is incorrect.");
	}

	#endregion Moq składnia imperatywna

	#region Moq składnia deklaratywna

	[TestCase("1", "2")]
	public void Login_WhenCorrectData_ShouldReturnEmptyString_Declarative(string user, string password)
	{
		IUsersRepository mockUserRepository = Mock.Of<IUsersRepository>(x => x.Login(user, password) == true);
		Authentication authentication = new(mockUserRepository);
		var result = authentication.Login(user, password);
		result.Should().BeEmpty();
	}

	[TestCase("1", "2")]
	public void Login_WhenIncorrectData_ShouldReturnCorrectMessage_Declarative(string user, string password)
	{
		IUsersRepository mockUserRepository = Mock.Of<IUsersRepository>(x => x.Login(user, password) == false);
		Authentication authentication = new(mockUserRepository);
		var result = authentication.Login(user, password);
		result.Should().Contain("User or password is incorrect.");
	}

	#endregion Moq składnia deklaratywna

	#region Fake class

	//private readonly Authentication _authentication;

	//[SetUp]
	//public void SetUp()
	//{
	//	_authentication = new(new FakeUserRepository());
	//}

	//[Test]
	//public void Login_WhenCorrectData_ShouldReturnEmptyString()
	//{
	//	var result = _authentication.Login("1", "2");
	//	result.Should().BeEmpty();
	//}

	//[Test]
	//public void Login_WhenIncorrectData_ShouldReturnCorrectMessage()
	//{
	//	var result = _authentication.Login("2", "2");
	//	result.Should().Contain("User or password is incorrect.");
	//}

	#endregion Fake class
}