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
            Bankkonto konto = new Privatkonto(1000.0, "VIP", new DateTime(2026, 3, 1));
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
            Bankkonto konto = new Privatkonto(-500.0, "VIP", new DateTime(2026, 3, 1));
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
            Bankkonto konto = new Privatkonto(1000.0, "VIP", new DateTime(2026, 3, 1));
            // Act
            double neuesGuthaben = konto.ZahleEin(500.0, new DateTime(2026, 3, 15));
            // Assert
            Assert.AreEqual(1500.0, neuesGuthaben);
        }
        
        [TestMethod]
        public void TestBeziehe()
        {
            // Arrange
            Bankkonto konto = new Privatkonto(1000.0, "VIP", new DateTime(2026, 3, 1));
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

        [TestMethod]
        // Grenze bei 0
        [DataRow(-0.04166667, "Standard", -10.0, DisplayName = "Unter 0 - Passivzins")]
        [DataRow(0.0, "Standard", 0.0, DisplayName = "Genau 0 - Aktivzins")]
        [DataRow(0.00001667, "Standard", 0.01, DisplayName = "Knapp über 0 - Aktivzins")]

        // Grenze bei 10'000
        [DataRow(16.66665, "Standard", 9999.99, DisplayName = "Knapp unter 10'000 - Aktivzins")]
        [DataRow(20.83333333, "Standard", 10000.0, DisplayName = "Genau 10'000 - Aktivzins + 0.5%")]
        [DataRow(20.83335417, "Standard", 10000.01, DisplayName = "Knapp über 10'000 - Aktivzins + 0.5%")]

        // Grenze bei 50'000
        [DataRow(104.16664583, "Standard", 49999.99, DisplayName = "Knapp unter 50'000 - Aktivzins + 0.5%")]
        [DataRow(114.58333333, "Standard", 50000.0, DisplayName = "Genau 50'000 - Standard + 0.75%")]
        [DataRow(145.83333333, "VIP", 50000.0, DisplayName = "Genau 50'000 - VIP + 1.5%")]
        [DataRow(145.83336250, "VIP", 50000.01, DisplayName = "Knapp über 50'000 - VIP + 1.5%")]

        // Obere Grenze bei 100'000
        [DataRow(229.16664375, "Standard", 99999.99, DisplayName = "Standard knapp unter 100'000")]
        [DataRow(291.66663750, "VIP", 99999.99, DisplayName = "VIP knapp unter 100'000")]
        public void BerechneZinsBis_berechnet_korrekte_Zinsen(double exp, string status, double guthaben)
        {
            // Arrange
            Bankkonto konto = new Privatkonto(guthaben, status, new DateTime(2026, 3, 1));

            // Act
            konto.BerechneZinsBis(new DateTime(2026, 3, 31)); // Berechne Zinsen bis zum 31. März 2026

            // Assert
            Assert.AreEqual(exp, konto.ZinsGuthaben, 0.01);

        }
    }
}
