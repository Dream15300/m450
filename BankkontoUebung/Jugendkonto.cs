using System;
using System.Collections.Generic;
using System.Text;

namespace BankkontoUebung
{
    public class Jugendkonto : Bankkonto
    {
        public Jugendkonto(double guthaben, DateTime erstellungsDatum) : base(guthaben, erstellungsDatum)
        {
        }

        public override double Beziehe(double betrag, DateTime datum)
        {
            // Geschäftsregel R7:
            // Jugendkonto darf nicht überzogen werden.
            if (Guthaben - betrag < 0)
            {
                throw new InvalidOperationException(
                    "Jugendkonto darf nicht überzogen werden.");
            }

            // Geschäftsregel R13:
            // Jugendkonto darf nicht mehr als 800 Euro abheben.
            if (betrag > 800)
            {
                throw new InvalidOperationException(
                    "Jugendkonto darf nicht mehr als 800 Euro abheben.");
            }
            return base.Beziehe(betrag, datum);
        }
    }
}
