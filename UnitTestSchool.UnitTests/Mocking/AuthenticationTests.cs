namespace UnitTestSchool.UnitTests.Mocking;

internal class AuthenticationTests
{
	#region Moq składnia imperatywna

	[TestCase("1", "2")]
	public void Login_WhenCorrectData_ShouldReturnEmptyString_Imperative(string user, string password)
	{
		// inicjalizacja nowego Mock-a, najczęściej takie inicjalizacje przenosi się do metody SetUp albo własnej metody inicjalizującej
		Mock<IUsersRepository> mockUserRepository = new();

		// zdefiniowanie, jak ma wyglądać metoda login:
		// w metodzie Setup trzeba podać lambdę, gdzie określa się mockowaną metodę wraz z konkretnymi parametrami oraz w Returns() wartość zwracaną z tej metody
		// czyli w tym przypadku, dla parametrów 1 i 2 zostanie zwrócona wartość true
		//mockUserRepository.Setup(x => x.Login("1", "2")).Returns(true);

		// zwraca True dla każdego stringa podstawionego jako argument:
		//mockUserRepository.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

		// testy dla konkretnych stringów - w większości przypadków to lepsze rozwiązanie niż ogólne IsAny<string>
		mockUserRepository.Setup(x => x.Login(user, password)).Returns(true);

		// w miejsce zastępowanej zależności trzeba przekazać Object przygotowanego wcześniej Mocka
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
		// Mock.Of<T> - tworzy obiekt zastępowanej klasy
		// w lambda jest wyrażenie, gdzie najpierw podaje się zastępowaną metodę z jej konkretnymi parametrami, a to porównuje się z wartością zwracaną z tej metody
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

	// złe rozwiązanie - użycie sztucznej klasy FakeUserRepository zamiast Moq

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