using UnitTestDemo;

namespace TestInsuranceCalc;

[TestClass]
public sealed class InsuranceCalculatorTests
{
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
        Assert.AreEqual(expected, actual, 0.001,
            "Premium for young driver with an accident should be high");
    }
}
