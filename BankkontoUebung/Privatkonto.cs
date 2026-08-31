using System;
using System.Collections.Generic;
using System.Text;

namespace BankkontoUebung
{
    public class Privatkonto : Bankkonto
    {
        public const double MaximalerÜberziehungsbetrag = 500.0;
        public Privatkonto(double guthaben, DateTime erstellungsDatum) : base(guthaben, erstellungsDatum)
        {
        }

        public override double Beziehe(double betrag, DateTime datum)
        {
            // Geschäftsregel R2:
            // Privatkonto darf maximal bis -500 überzogen werden.
            if (Guthaben - betrag < -MaximalerÜberziehungsbetrag)
            {
                throw new InvalidOperationException(
                    "Privatkonto darf nicht mehr als 500 Euro überzogen werden.");
            }

            return base.Beziehe(betrag, datum);
        }
    }
}
