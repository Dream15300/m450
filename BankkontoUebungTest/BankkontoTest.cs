using BankkontoUebung;

namespace BankkontoUebungTest
{
    [TestClass]
    public sealed class BankkontoTest // Testklasse Bankkonto ist abstract, daher kann sie nicht direkt instanziiert werden. Stattdessen wird eine abgeleitete Klasse wie Privatkonto oder Sparkonto verwendet.
    {
        [TestMethod]
        public void TestAktivzinsCalculation()
        {
            // Arrange
            Bankkonto konto = new Privatkonto(1000.0, new DateTime(2026, 3, 1));
            Bankkonto.AktivZins = 0.036; // Set AktivZins to 3.6% annually

            // Act
            double zins = konto.SchreibeZinsGut(new DateTime(2026, 3, 31)); // Calculate interest for 30 days


            // Assert
            Assert.AreEqual(3.0, zins, 0.01); // Assuming AktivZins is 3.6% for 30 days
        }

        [TestMethod]
        public void TestPassivzinsCalculation()
        {
            // Arrange
            Bankkonto konto = new Privatkonto(-500.0, new DateTime(2026, 3, 1));
            Bankkonto.PassivZins = 0.072; // Set PassivZins to 7.2% annually
            // Act
            double zins = konto.SchreibeZinsGut(new DateTime(2026, 3, 31)); // Calculate interest for 30 days
            // Assert
            Assert.AreEqual(-3.0, zins, 0.01); // Assuming PassivZins is 7.2% for 30 days
        }

        [TestMethod]
        public void TestZahleEin()
        {
            // Arrange
            Bankkonto konto = new Privatkonto(1000.0, new DateTime(2026, 3, 1));
            // Act
            double neuesGuthaben = konto.ZahleEin(500.0, new DateTime(2026, 3, 15));
            // Assert
            Assert.AreEqual(1500.0, neuesGuthaben);
        }
        
        [TestMethod]
        public void TestBeziehe()
        {
            // Arrange
            Bankkonto konto = new Privatkonto(1000.0, new DateTime(2026, 3, 1));
            // Act
            double neuesGuthaben = konto.Beziehe(300.0, new DateTime(2026, 3, 15));
            // Assert
            Assert.AreEqual(700.0, neuesGuthaben);
        }
        /* durch Abstract kann bankkonto nicht mehr instanziert werden, daher ist der Test nicht mehr möglich
        [TestMethod]
        public void Kontoabschluss_bucht_korrekte_Zinsen()
        {
            // Arrange
            Bankkonto konto = new Bankkonto(1000.0, new DateTime(2026, 3, 1));
            Bankkonto.AktivZins = 0.036; // Set AktivZins to 3.6% annually
            Bankkonto.PassivZins = 0.072; // Set PassivZins to 7.2% annually

            // Act
            konto.ZahleEin(1000.0, new DateTime(2026, 7, 1)); // Einzahlen von 1000 am 1. Juli 2026
            konto.Beziehe(3000.0, new DateTime(2026, 8, 1)); // Abheben von 3000 am 1. August 2026
            konto.ZahleEin(2000.0, new DateTime(2026, 10, 1)); // Einzahlen von 2000 am 1. Oktober 2026
            konto.KontoAbschluss(new DateTime(2026, 12, 31)); // Kontoabschluss am 31. Dezember 2026

            // Assert
            Assert.AreEqual(1015.30, konto.Guthaben, 0.01); // Überprüfen des Guthabens nach Zinsbuchung
        }
        */
    }
}
