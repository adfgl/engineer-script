using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptLib.Units
{
    public abstract class UnitCatalogBase
    {
        protected static readonly Unit L = new Unit(length: 1);
        protected static readonly Unit M = new Unit(mass: 1);
        protected static readonly Unit T = new Unit(time: 1);
        protected static readonly Unit I = new Unit(electricCurrent: 1);
        protected static readonly Unit Th = new Unit(thermodynamicTemperature: 1);
        protected static readonly Unit N = new Unit(amountOfSubstance: 1);
        protected static readonly Unit J = new Unit(luminousIntensity: 1);
        protected static readonly Unit One = Unit.One;

        protected const double Pi = Math.PI;

        public abstract UnitStandard Standard { get; }

        /// <summary>Prefixes relevant to this catalog. (Only SI uses these.)</summary>
        public virtual ReadOnlySpan<Prefix> Prefixes => ReadOnlySpan<Prefix>.Empty;

        /// <summary>Base unit specs only (no auto-prefixed derivatives here).</summary>
        public abstract ReadOnlySpan<UnitSpec> Units { get; }
    }
}
