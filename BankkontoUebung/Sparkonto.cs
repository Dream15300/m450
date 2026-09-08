using System;
using System.Collections.Generic;
using System.Text;

namespace BankkontoUebung
{
    public class Sparkonto : Bankkonto
    {
        public Sparkonto(double guthaben, string status, DateTime erstellungsDatum) : base(guthaben, status, erstellungsDatum)
        {
        }

        public override double Beziehe(double betrag, DateTime datum)
        {
            // Geschäftsregel R3:
            // Sparkonto darf nicht überzogen werden.
            if (Guthaben - betrag < 0)
            {
                throw new InvalidOperationException(
                    "Sparkonto darf nicht überzogen werden.");
            }
            return base.Beziehe(betrag, datum);
        }
    }
}
