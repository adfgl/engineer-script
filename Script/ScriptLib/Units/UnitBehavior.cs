namespace ScriptLib.Units
{
    public enum UnitBehavior : byte
    {
        Multiplicative, // vSI = v * Scale
        Affine          // vSI = v * Scale + Offset  (°C, °F)
    }
}
