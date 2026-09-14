using BankkontoUebung;

namespace BankkontoUebungTest;

[TestClass]
public class PrivatkontoTest
{
    [TestMethod]
    // Geschäftsregel R2:
    public void MaximalerÜberziehungsbetrag()
    {
        // Arrange
        Privatkonto konto = new Privatkonto(1000.0, "Standard", new DateTime(2026, 3, 1));

        // Act & Assert
        Assert.ThrowsExactly<InvalidOperationException>(() => konto.Beziehe(1501.0, new DateTime(2026, 3, 2)));
    }
}
