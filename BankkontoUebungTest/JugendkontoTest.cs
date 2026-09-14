using BankkontoUebung;

namespace BankkontoUebungTest;

[TestClass]
public class JugendkontoTest
{
    [TestMethod]
    // Geschäftsregel R7:
    public void JugendkontoDarfNichtÜberzogenWerden()
    {
        // Arrange
        Jugendkonto konto = new Jugendkonto(1000.0, "Standard", new DateTime(2026, 3, 1));

        // Act & Assert
        Assert.ThrowsExactly<InvalidOperationException>(() => konto.Beziehe(1001.0, new DateTime(2026, 3, 2)));
    }

    [TestMethod]
    public void JugendkontoDarfNichtMehrAls800Beziehen()
    {
        // Arrange
        Jugendkonto konto = new Jugendkonto(1000.0, "Standard", new DateTime(2026, 3, 1));
        // Act & Assert
        Assert.ThrowsExactly<InvalidOperationException>(() => konto.Beziehe(801.0, new DateTime(2026, 3, 2)));
    }
}
