using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptLib.Units.Catalogs
{
    public sealed class ImperialCatalog : UnitCatalogBase
    {
        public override UnitStandard Standard => UnitStandard.Imperial;

        // Length: same exact definitions as US customary
        const double InchToMeter = 0.0254;
        const double FootToMeter = 0.3048;
        const double YardToMeter = 0.9144;
        const double MileToMeter = 1609.344;

        // Mass: pound is same exact definition used internationally
        const double PoundToKg = 0.45359237;
        const double StandardGravity = 9.80665;

        // Pressure: psi same definition (lbf / in^2)
        const double PsiToPa = 6894.757293168361;

        // Imperial volumes (UK): exact in litres by definition
        // 1 imp gal = 4.54609 L exactly
        const double LiterToM3 = 1e-3;
        const double ImpGallonToM3 = 4.54609 * LiterToM3;          // 0.00454609
        const double ImpQuartToM3 = ImpGallonToM3 / 4.0;
        const double ImpPintToM3 = ImpGallonToM3 / 8.0;
        const double ImpFluidOunceToM3 = ImpGallonToM3 / 160.0;

        static readonly UnitSpec[] _units =
        [
            // ---------- Length ----------
            new(UnitStandard.Imperial, UnitBehavior.Multiplicative, "length", "inch", "in", L, InchToMeter, 0, Prefixable: false),
        new(UnitStandard.Imperial, UnitBehavior.Multiplicative, "length", "foot", "ft", L, FootToMeter, 0, Prefixable: false),
        new(UnitStandard.Imperial, UnitBehavior.Multiplicative, "length", "yard", "yd", L, YardToMeter, 0, Prefixable: false),
        new(UnitStandard.Imperial, UnitBehavior.Multiplicative, "length", "mile", "mi", L, MileToMeter, 0, Prefixable: false),

        // ---------- Mass ----------
        new(UnitStandard.Imperial, UnitBehavior.Multiplicative, "mass", "ounce", "oz", M, PoundToKg / 16.0, 0, Prefixable: false),
        new(UnitStandard.Imperial, UnitBehavior.Multiplicative, "mass", "pound", "lb", M, PoundToKg, 0, Prefixable: false, Aliases: ["lbm"]),
        new(UnitStandard.Imperial, UnitBehavior.Multiplicative, "mass", "stone", "st", M, 14.0 * PoundToKg, 0, Prefixable: false),
        // long ton (Imperial ton) = 2240 lb
        new(UnitStandard.Imperial, UnitBehavior.Multiplicative, "mass", "long ton", "long ton", M, 2240.0 * PoundToKg, 0, Prefixable: false, Aliases: ["imperial ton","ton (imp)"]),

        // ---------- Force ----------
        new(UnitStandard.Imperial, UnitBehavior.Multiplicative, "force", "pound-force", "lbf", M * L * (T ^ -2), PoundToKg * StandardGravity, 0, Prefixable: false),

        // ---------- Pressure / stress ----------
        new(UnitStandard.Imperial, UnitBehavior.Multiplicative, "pressure, stress", "pound per square inch", "psi", M * (L ^ -1) * (T ^ -2), PsiToPa, 0, Prefixable: false),

        // ---------- Volume (Imperial / UK) ----------
        new(UnitStandard.Imperial, UnitBehavior.Multiplicative, "volume", "imperial gallon", "imp gal", L ^ 3, ImpGallonToM3, 0, Prefixable: false, Aliases: ["gal (imp)","UK gal"]),
        new(UnitStandard.Imperial, UnitBehavior.Multiplicative, "volume", "imperial quart", "imp qt", L ^ 3, ImpQuartToM3, 0, Prefixable: false, Aliases: ["qt (imp)"]),
        new(UnitStandard.Imperial, UnitBehavior.Multiplicative, "volume", "imperial pint", "imp pt", L ^ 3, ImpPintToM3, 0, Prefixable: false, Aliases: ["pt (imp)"]),
        new(UnitStandard.Imperial, UnitBehavior.Multiplicative, "volume", "imperial fluid ounce", "imp fl oz", L ^ 3, ImpFluidOunceToM3, 0, Prefixable: false, Aliases: ["fl oz (imp)"]),

        // ---------- Temperature (affine) ----------
        // Imperial usage still relies on Fahrenheit a lot; keep it here too.
        // K = °F * 5/9 + 255.372222...
        new(UnitStandard.Imperial, UnitBehavior.Affine, "temperature", "degree Fahrenheit", "°F", Th, 5.0/9.0, 255.3722222222222, Prefixable: false, Aliases: ["degF","fahrenheit"]),
    ];

        public override ReadOnlySpan<UnitSpec> Units => _units;
    }
}
