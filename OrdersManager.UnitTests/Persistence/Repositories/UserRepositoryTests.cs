using Microsoft.EntityFrameworkCore;
using OrdersManager.UnitTests.Extensions;
using OrdersManager.WebApp.Persistence.Repositories;

namespace OrdersManager.UnitTests.Persistence.Repositories;

internal class UserRepositoryTests
{
	private Mock<IApplicationDbContext> _mockContext;
	private UserRepository _userRepository;
	private Mock<DbSet<ApplicationUser>> _mockUsers;
	private List<ApplicationUser> _users;

	[SetUp]
	public void Setup()
	{
		_mockUsers = new();
		_users = [
			new ApplicationUser { Id = "1" },
			new ApplicationUser { Id = "2" },
			new ApplicationUser { Id = "3" },
		];
		_mockUsers.SetSource(_users);

		_mockContext = new();
		_mockContext.Setup(x => x.Users).Returns(_mockUsers.Object);
		_userRepository = new(_mockContext.Object);
	}

	[Test]
	public void GetUser_WhenUserDoesntExists_ShouldThrowNullReferenceException()
	{
		string badUserId = "4";

		Action action = () => _userRepository.GetUser(badUserId);

		action.Should().ThrowExactly<NullReferenceException>().WithMessage("*User doesn't exists.*");
	}

	[Test]
	public void GetUser_WhenCalled_ShouldReturnUser()
	{
		var result = _userRepository.GetUser(_users.First().Id);

		result.Should().Be(_users.First());
	}
}