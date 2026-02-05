using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptLib.Units.Catalogs
{
    public sealed class SiCatalog : UnitCatalogBase
    {
        public override UnitStandard Standard => UnitStandard.SI;

        static readonly Prefix[] _prefixes =
        [
            new("quetta", "Q", 1e30,  30),
            new("ronna",  "R", 1e27,  27),
            new("yotta",  "Y", 1e24,  24),
            new("zetta",  "Z", 1e21,  21),
            new("exa",    "E", 1e18,  18),
            new("peta",   "P", 1e15,  15),
            new("tera",   "T", 1e12,  12),
            new("giga",   "G", 1e9,    9),
            new("mega",   "M", 1e6,    6),
            new("kilo",   "k", 1e3,    3),
            new("hecto",  "h", 1e2,    2),
            new("deca",   "da",1e1,    1),

            new("",       "",  1e0,    0),

            new("deci",   "d", 1e-1,  -1),
            new("centi",  "c", 1e-2,  -2),
            new("milli",  "m", 1e-3,  -3),
            new("micro",  "µ", 1e-6,  -6),
            new("nano",   "n", 1e-9,  -9),
            new("pico",   "p", 1e-12,-12),
            new("femto",  "f", 1e-15,-15),
            new("atto",   "a", 1e-18,-18),
            new("zepto",  "z", 1e-21,-21),
            new("yocto",  "y", 1e-24,-24),
            new("ronto",  "r", 1e-27,-27),
            new("quecto", "q", 1e-30,-30),
        ];

        public override ReadOnlySpan<Prefix> Prefixes => _prefixes;

        static readonly UnitSpec[] _units =
        [
            // --- SI base
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "length", "metre", "m", L, 1, 0, Prefixable: true,  Aliases: ["meter","metre"]),
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "mass", "kilogram", "kg", M, 1, 0, Prefixable: false, Aliases: ["kilogram"]),
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "time", "second", "s", T, 1, 0, Prefixable: true,  Aliases: ["sec","second"]),
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "current", "ampere", "A", I, 1, 0, Prefixable: true),
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "temperature", "kelvin", "K", Th, 1, 0, Prefixable: true),
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "substance", "mole", "mol", N, 1, 0, Prefixable: true),
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "luminous intensity", "candela", "cd", J, 1, 0, Prefixable: true),

            // gram for prefixing mass (because kg is base but prefixes apply to g)
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "mass", "gram", "g", M, 1e-3, 0, Prefixable: true),

            // angles
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "plane angle", "radian", "rad", One, 1, 0, Prefixable: false),
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "solid angle", "steradian", "sr", One, 1, 0, Prefixable: false),
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "plane angle", "degree", "deg", One, Pi/180.0, 0, Prefixable: false, Aliases: ["°"]),
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "plane angle", "revolution", "rev", One, 2.0*Pi, 0, Prefixable: false),

            // time convenience
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "time", "minute", "min", T, 60.0, 0, Prefixable: false),
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "time", "hour", "h", T, 3600.0, 0, Prefixable: false, Aliases: ["hr"]),
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "time", "day", "d", T, 86400.0, 0, Prefixable: false),

            // affine temperature (still in SI catalog, because engineers use it)
            new(UnitStandard.SI, UnitBehavior.Affine, "temperature", "degree Celsius", "°C", Th, 1.0, 273.15, Prefixable: false, Aliases: ["degC","celsius"]),

            // SI named derived (core)
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "frequency", "hertz", "Hz", One / T, 1, 0, Prefixable: true),
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "force", "newton", "N", M * L * (T ^ -2), 1, 0, Prefixable: true),
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "pressure, stress", "pascal", "Pa", M * (L ^ -1) * (T ^ -2), 1, 0, Prefixable: true),
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "energy, work, heat", "joule", "J", M * (L ^ 2) * (T ^ -2), 1, 0, Prefixable: true),
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "power", "watt", "W", M * (L ^ 2) * (T ^ -3), 1, 0, Prefixable: true),

            new(UnitStandard.SI, UnitBehavior.Multiplicative, "charge", "coulomb", "C", I * T, 1, 0, Prefixable: true),
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "voltage", "volt", "V", M * (L ^ 2) * (T ^ -3) * (I ^ -1), 1, 0, Prefixable: true),
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "capacitance", "farad", "F", (M ^ -1) * (L ^ -2) * (T ^ 4) * (I ^ 2), 1, 0, Prefixable: true),
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "resistance", "ohm", "Ω", M * (L ^ 2) * (T ^ -3) * (I ^ -2), 1, 0, Prefixable: true, Aliases: ["ohm"]),
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "conductance", "siemens", "S", (M ^ -1) * (L ^ -2) * (T ^ 3) * (I ^ 2), 1, 0, Prefixable: true),

            new(UnitStandard.SI, UnitBehavior.Multiplicative, "magnetic flux", "weber", "Wb", M * (L ^ 2) * (T ^ -2) * (I ^ -1), 1, 0, Prefixable: true),
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "magnetic flux density", "tesla", "T", M * (T ^ -2) * (I ^ -1), 1, 0, Prefixable: true),
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "inductance", "henry", "H", M * (L ^ 2) * (T ^ -2) * (I ^ -2), 1, 0, Prefixable: true),

            // common accepted
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "volume", "litre", "L", L ^ 3, 1e-3, 0, Prefixable: true, Aliases: ["l","liter","litre"]),
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "pressure", "bar", "bar", M * (L ^ -1) * (T ^ -2), 1e5, 0, Prefixable: true),
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "pressure", "standard atmosphere", "atm", M * (L ^ -1) * (T ^ -2), 101325.0, 0, Prefixable: false),
            new(UnitStandard.SI, UnitBehavior.Multiplicative, "mass", "tonne", "t", M, 1000.0, 0, Prefixable: false, Aliases: ["tonne","metric ton","metric tonne"]),
        ];

        public override ReadOnlySpan<UnitSpec> Units => _units;
    }
}
