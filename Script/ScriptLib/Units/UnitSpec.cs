namespace ScriptLib.Units
{
    public readonly record struct UnitSpec(
     UnitStandard Standard,
     UnitBehavior Behavior,
     string Quantity,
     string Name,
     string Symbol,
     Unit Dim,
     double ScaleToSI,
     double OffsetToSI,
     bool Prefixable,
     string[]? Aliases = null
 );
}
