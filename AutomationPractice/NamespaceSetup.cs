namespace AutomationPractice
{
    using AutomationPractice.Helpers;

    using NUnit.Framework;

    [TestFixture]
    public static class NamespaceSetup
    {
        [OneTimeSetUp]
        public static void ExecteForCreatingReportsNamespace(TestContext testContext)
        {
            Reporter.StartReporter();
        }
    }
}
