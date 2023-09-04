namespace UnitTestSchool.Lib.Mocking;

public interface IEmailSender
{
	void Send(string title, string body, string toEmail);
}