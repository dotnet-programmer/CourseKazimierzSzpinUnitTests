using System.Transactions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace OrdersManager.IntegrationTests.Helpers;

// klasa Isolated do wykonywania testów w transakcjach, czyli nowa transakcja, wprowadzenie zmian, wycofanie transakcji
[AttributeUsage(AttributeTargets.Method)]
internal class Isolated : Attribute, ITestAction
{
	private TransactionScope _transactionScope = default!;

	// ustawienie tej właściwości na ActionTargets.Test zapewnia, że BeforeTest wykona się przed każdym testem, a AfterTest po każdym teście
	public ActionTargets Targets
		=> ActionTargets.Test;

	public void AfterTest(ITest test)
		=> _transactionScope.Dispose();

	public void BeforeTest(ITest test)
		=> _transactionScope = new TransactionScope();
}