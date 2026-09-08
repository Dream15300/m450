using System;

namespace BankkontoUebung
{
    public abstract class Bankkonto
    {
        public string KontoNummer { get; } = Guid.NewGuid().ToString();
        public string Status { get; private set; }

        public double Guthaben { get; internal set; } = 0.0;

        // Aufgelaufene, aber noch nicht auf das Guthaben gebuchte Zinsen
        public double ZinsGuthaben { get; private set; } = 0.0;

        public DateTime ErstellungsDatum { get; }

        public DateTime LetzteZinsBuchung { get; private set; }

        // Beispiel:
        // 0.036 entspricht 3.6 %
        public static double AktivZins { get; set; } = 0.02;
        public static double PassivZins { get; set; } = 0.05;

        public bool istGeschlossen = false;


        // Konstruktor
        public Bankkonto(double guthaben, string status, DateTime erstellungsDatum)
        {
            Guthaben = guthaben;
            Status = status;

            ErstellungsDatum = erstellungsDatum.Date;
            LetzteZinsBuchung = erstellungsDatum.Date;
        }


        // Einzahlung
        public double ZahleEin(double betrag, DateTime datum)
        {
            if (istGeschlossen)
            {
                throw new InvalidOperationException(
                    "Das Konto ist geschlossen.");
            }

            if (betrag <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(betrag),
                    "Betrag muss größer als 0 sein.");
            }

            PruefeDatum(datum);

            // Zuerst Zinsen mit dem bisherigen Guthaben berechnen
            BerechneZinsBis(datum);

            // Danach Einzahlung durchführen
            Guthaben += betrag;

            return Guthaben;
        }


        // Auszahlung
        public virtual double Beziehe(double betrag, DateTime datum)
        {
            if (istGeschlossen)
            {
                throw new InvalidOperationException(
                    "Das Konto ist geschlossen.");
            }

            if (betrag <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(betrag),
                    "Betrag muss größer als 0 sein.");
            }

            PruefeDatum(datum);

            // Zuerst Zinsen mit dem bisherigen Guthaben berechnen
            BerechneZinsBis(datum);

            // Danach Auszahlung durchführen
            Guthaben -= betrag;

            return Guthaben;
        }


        // Überweisung
        public void Transferiere(
            Bankkonto zielKonto,
            double betrag,
            DateTime datum)
        {
            if (istGeschlossen)
            {
                throw new InvalidOperationException(
                    "Das Konto ist geschlossen.");
            }

            if (zielKonto == null)
            {
                throw new ArgumentNullException(
                    nameof(zielKonto),
                    "Zielkonto darf nicht null sein.");
            }

            if (zielKonto.istGeschlossen)
            {
                throw new InvalidOperationException(
                    "Das Zielkonto ist geschlossen.");
            }

            if (betrag <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(betrag),
                    "Betrag muss größer als 0 sein.");
            }

            PruefeDatum(datum);
            zielKonto.PruefeDatum(datum);

            Beziehe(betrag, datum);
            zielKonto.ZahleEin(betrag, datum);
        }

        public double SchreibeZinsGut(DateTime datum)
        {
            if (istGeschlossen)
            {
                throw new InvalidOperationException(
                    "Das Konto ist geschlossen.");
            }

            PruefeDatum(datum);

            BerechneZinsBis(datum);

            return ZinsGuthaben;
        }

        // Berechnet die Zinsen seit der letzten Zinsberechnung
        // bis zum angegebenen Datum.
        public void BerechneZinsBis(DateTime datum)
        {
            datum = datum.Date;

            int anzTage = (datum - LetzteZinsBuchung.Date).Days;

            // Am gleichen Tag fallen keine zusätzlichen Zinsen an
            if (anzTage == 0)
            {
                return;
            }

            double zins;

            if (Guthaben >= 0)
            {
                // 0 <= Guthaben < 10'000
                zins = AktivZins;

                // 10'000 <= Guthaben < 50'000
                if (Guthaben >= 10000 && Guthaben < 50000)
                {
                    zins = AktivZins + 0.005;
                }

                // 50'000 <= Guthaben < 100'000, Standard
                else if (Status == "Standard" && Guthaben >= 50000 && Guthaben < 100000)
                {
                    zins = AktivZins + 0.0075;
                }

                // 50'000 <= Guthaben < 100'000, VIP
                else if (Status == "VIP" && Guthaben >= 50000 && Guthaben < 100000)
                {
                    zins = AktivZins + 0.015;
                }
            }
            else
            {
                zins = PassivZins;
            }

            double zinsBetrag = Guthaben * zins * anzTage / 360.0;

            ZinsGuthaben += zinsBetrag;

            LetzteZinsBuchung = datum;
        }


        // Jahres-/Kontoabschluss
        // Die bisher aufgelaufenen Zinsen werden auf das Guthaben gebucht.
        public void KontoAbschluss(DateTime datum)
        {
            if (istGeschlossen)
            {
                throw new InvalidOperationException(
                    "Das Konto ist geschlossen.");
            }

            PruefeDatum(datum);

            // Zinsen bis zum Abschlussdatum berechnen
            BerechneZinsBis(datum);

            // Aufgelaufene Zinsen auf das Guthaben buchen
            Guthaben += ZinsGuthaben;

            // Zinsguthaben wurde nun verbucht
            ZinsGuthaben = 0.0;
        }


        // Konto tatsächlich schließen / kündigen
        public void SchliesseKonto()
        {
            if (istGeschlossen)
            {
                throw new InvalidOperationException(
                    "Das Konto ist bereits geschlossen.");
            }

            if (Guthaben != 0)
            {
                throw new InvalidOperationException(
                    "Das Konto kann nur mit einem Guthaben von 0 geschlossen werden.");
            }

            istGeschlossen = true;
        }


        // Prüft, ob eine Buchung zeitlich erlaubt ist
        public void PruefeDatum(DateTime datum)
        {
            datum = datum.Date;

            if (datum < ErstellungsDatum)
            {
                throw new ArgumentException(
                    "Das Datum darf nicht vor dem Erstellungsdatum des Kontos liegen.",
                    nameof(datum));
            }

            if (datum < LetzteZinsBuchung)
            {
                throw new ArgumentException(
                    "Das Datum darf nicht vor der letzten Buchung liegen.",
                    nameof(datum));
            }
        }
    }
}