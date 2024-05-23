namespace UnitTestSchool.Lib.Mocking;

public class Authentication(IUsersRepository usersRepository)
{
	private readonly IUsersRepository _usersRepository = usersRepository;

	public string Login(string user, string password)
	{
		var isAuthenticated = _usersRepository.Login(user, password);

		return isAuthenticated
			? string.Empty
			: "User or password is incorrect.";
	}
}