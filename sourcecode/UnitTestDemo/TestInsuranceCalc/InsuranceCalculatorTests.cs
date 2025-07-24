using System.Reflection;
using UnitTestDemo;

namespace TestInsuranceCalc;

[TestClass]
public sealed class InsuranceCalculatorTests
{
    [AssemblyInitialize]
    public static void AssemblyInitialize(TestContext context)
    {
        // Any assembly-level setup code if needed
    }

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        // Any class-level setup code if needed
    }

    [TestInitialize]
    public void TestInitialize()
    {
        // Any test-level setup code if needed
    }
    
    [TestMethod]
    public void PremiumCalc_YoungDriverWithAccident_ReturnsHigherPremium()
    {
        // Arrange
        int age = 22;
        int accidents = 1;
        // Expected: base $500 * 2 for young driver + $100 for one accident = $1100
        double expected = 1100;

        // Act 
        double actual = InsuranceCalculator.CalculatePremium(age, accidents);

        // Assert
        Assert.AreEqual(expected, actual, 0.0001,
            "Premium for young driver with an accident should be high");
    }

    [TestMethod]
    public void PremiumCalc_SeniorDriverWithNoAccidents_ReturnsDiscountedPremium()
    {
        // Arrange
        int age = 72;
        int accidents = 0;
        // Expected: base $500 * 1.5 for senior driver - $200 for no accidents = $550
        double expected = 550;
        // Act 
        double actual = InsuranceCalculator.CalculatePremium(age, accidents);
        // Assert
        Assert.AreEqual(expected, actual, 0.0001,
            "Premium for senior driver with no accidents should be discounted to minimum");
    }

    [DataTestMethod]
    [DataRow(22, 1, 1100)]
    [DataRow(30, 0, 300)]
    [DataRow(72, 0, 550)]
    public void TestVariableCalculations(int age, int accidents, double expected) 
    {
        // Act 
        double actual = InsuranceCalculator.CalculatePremium(age, accidents);
        // Assert
        Assert.AreEqual(expected, actual, 0.0001,
            $"Premium for driver age {age} with {accidents} accidents should be {expected}");
    }



    public static IEnumerable<object[]> AdditionData
    {
        get
        {
            yield return new object[] { 22, 1, 1100 };
            yield return new object[] { 30, 0, 300 };
            yield return new object[] { 72, 0, 550 };
        }
    }

    public static string GetCustomDynamicDataDisplayName(MethodInfo methodInfo, object[] data)
    {
        return string.Format("DynamicDataTestMethod {0} with {1} parameters", methodInfo.Name, data.Length);
    }

    [TestMethod]
    [DynamicData(nameof(AdditionData), DynamicDataDisplayName = nameof(GetCustomDynamicDataDisplayName))]
    public void TestDynamicDataCalculations(int age, int accidents, double expected)
    {
        // Act 
        double actual = InsuranceCalculator.CalculatePremium(age, accidents);
        // Assert
        Assert.AreEqual(expected, actual, 0.0001,
            $"Premium for driver age {age} with {accidents} accidents should be {expected}");
    }





    [TestMethod, Timeout(2000)]
    public void TestTimeoutOfMethod()
    {
        // Arrange
        int age = 22;
        int accidents = 1;
        // Expected: base $500 * 2 for young driver + $100 for one accident = $1100
        double expected = 1100;

        Task.Delay(2100).Wait(); // Simulate some delay

        // Act 
        double actual = InsuranceCalculator.CalculatePremium(age, accidents);

        // Assert
        Assert.AreEqual(expected, actual, 0.0001,
            "Premium for young driver with an accident should be high");
    }





    [TestCleanup]
    public void TestCleanup()
    {
        // Runs after each test method
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        // Runs once after all tests in this class
    }

    [AssemblyCleanup]
    public static void AssemblyCleanup()
    {
        // Any assembly-level cleanup code if needed
    }
}
