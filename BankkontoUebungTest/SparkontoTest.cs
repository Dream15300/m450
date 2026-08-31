using BankkontoUebung;

namespace BankkontoUebungTest;

[TestClass]
public class SparkontoTest
{
    [TestMethod]
    // Geschäftsregel R3:
    public void SparkontoDarfNichtÜberzogenWerden()
    {
        // Arrange
        Sparkonto konto = new Sparkonto(1000.0, new DateTime(2026, 3, 1));

        // Act & Assert
        Assert.ThrowsExactly<InvalidOperationException>(() => konto.Beziehe(1001.0, new DateTime(2026, 3, 2)));
    }
}
