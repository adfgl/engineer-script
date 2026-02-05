using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptLib.Units.Catalogs
{
    public sealed class UsCatalog : UnitCatalogBase
    {
        public override UnitStandard Standard => UnitStandard.US;

        const double InchToMeter = 0.0254;
        const double FootToMeter = 0.3048;
        const double YardToMeter = 0.9144;
        const double MileToMeter = 1609.344;
        const double PoundToKg = 0.45359237;
        const double StandardGravity = 9.80665;
        const double PsiToPa = 6894.757293168361;
        const double KsiToPa = PsiToPa * 1000.0;
        const double UsGallonToM3 = 0.003785411784;

        static readonly UnitSpec[] _units =
        [
            new(UnitStandard.US, UnitBehavior.Multiplicative, "length", "inch", "in", L, InchToMeter, 0, Prefixable: false),
        new(UnitStandard.US, UnitBehavior.Multiplicative, "length", "foot", "ft", L, FootToMeter, 0, Prefixable: false),
        new(UnitStandard.US, UnitBehavior.Multiplicative, "length", "yard", "yd", L, YardToMeter, 0, Prefixable: false),
        new(UnitStandard.US, UnitBehavior.Multiplicative, "length", "mile", "mi", L, MileToMeter, 0, Prefixable: false),

        new(UnitStandard.US, UnitBehavior.Multiplicative, "mass", "pound", "lb", M, PoundToKg, 0, Prefixable: false, Aliases: ["lbm"]),
        new(UnitStandard.US, UnitBehavior.Multiplicative, "force", "pound-force", "lbf", M * L * (T ^ -2), PoundToKg * StandardGravity, 0, Prefixable: false),

        new(UnitStandard.US, UnitBehavior.Multiplicative, "pressure, stress", "pound per square inch", "psi", M * (L ^ -1) * (T ^ -2), PsiToPa, 0, Prefixable: false),
        new(UnitStandard.US, UnitBehavior.Multiplicative, "pressure, stress", "kip per square inch", "ksi", M * (L ^ -1) * (T ^ -2), KsiToPa, 0, Prefixable: false),

        new(UnitStandard.US, UnitBehavior.Multiplicative, "volume", "US gallon", "gal", L ^ 3, UsGallonToM3, 0, Prefixable: false),

        // Fahrenheit affine: K = °F * 5/9 + 255.372222...
        new(UnitStandard.US, UnitBehavior.Affine, "temperature", "degree Fahrenheit", "°F", Th, 5.0/9.0, 255.3722222222222, Prefixable: false, Aliases: ["degF","fahrenheit"]),
    ];

        public override ReadOnlySpan<UnitSpec> Units => _units;
    }
}
