namespace UnitTestSchool.Lib.Mocking;

public class Authentication(IUsersRepository usersRepository)
{
	private readonly IUsersRepository _usersRepository = usersRepository;

	public string Login(string user, string password)
		=> _usersRepository.Login(user, password)
			? string.Empty
			: "User or password is incorrect.";
}