namespace ScriptLib.Units
{
    public readonly struct Unit : IEquatable<Unit>
    {
        public const int MaxReasonablePower = 16;

        public readonly sbyte length;
        public readonly sbyte mass;
        public readonly sbyte time;
        public readonly sbyte electricCurrent;
        public readonly sbyte thermodynamicTemperature;
        public readonly sbyte amountOfSubstance;
        public readonly sbyte luminousIntensity;

        public Unit(
            sbyte length = 0,
            sbyte mass = 0,
            sbyte time = 0,
            sbyte electricCurrent = 0,
            sbyte thermodynamicTemperature = 0,
            sbyte amountOfSubstance = 0,
            sbyte luminousIntensity = 0)
        {
            this.length = length;
            this.mass = mass;
            this.time = time;
            this.electricCurrent = electricCurrent;
            this.thermodynamicTemperature = thermodynamicTemperature;
            this.amountOfSubstance = amountOfSubstance;
            this.luminousIntensity = luminousIntensity;
        }

        public static readonly Unit One = new Unit();

        public static bool SameDimensions(in Unit a, in Unit b) =>
            a.length == b.length &&
            a.mass == b.mass &&
            a.time == b.time &&
            a.electricCurrent == b.electricCurrent &&
            a.thermodynamicTemperature == b.thermodynamicTemperature &&
            a.amountOfSubstance == b.amountOfSubstance &&
            a.luminousIntensity == b.luminousIntensity;

        public static bool SameOrScalar(in Unit a, in Unit b) =>
            a == One|| b == One || SameDimensions(in a, in b);

        public static Unit Invert(in Unit a)
        {
            checked
            {
                return new Unit(
                    length: (sbyte)-a.length,
                    mass: (sbyte)-a.mass,
                    time: (sbyte)-a.time,
                    electricCurrent: (sbyte)-a.electricCurrent,
                    thermodynamicTemperature: (sbyte)-a.thermodynamicTemperature,
                    amountOfSubstance: (sbyte)-a.amountOfSubstance,
                    luminousIntensity: (sbyte)-a.luminousIntensity
                );
            }
        }

        public static Unit operator *(in Unit a, in Unit b)
        {
            checked
            {
                return new Unit(
                    length: (sbyte)(a.length + b.length),
                    mass: (sbyte)(a.mass + b.mass),
                    time: (sbyte)(a.time + b.time),
                    electricCurrent: (sbyte)(a.electricCurrent + b.electricCurrent),
                    thermodynamicTemperature: (sbyte)(a.thermodynamicTemperature + b.thermodynamicTemperature),
                    amountOfSubstance: (sbyte)(a.amountOfSubstance + b.amountOfSubstance),
                    luminousIntensity: (sbyte)(a.luminousIntensity + b.luminousIntensity)
                );
            }
        }

        public static Unit operator /(in Unit a, in Unit b)
        {
            checked
            {
                return new Unit(
                    length: (sbyte)(a.length - b.length),
                    mass: (sbyte)(a.mass - b.mass),
                    time: (sbyte)(a.time - b.time),
                    electricCurrent: (sbyte)(a.electricCurrent - b.electricCurrent),
                    thermodynamicTemperature: (sbyte)(a.thermodynamicTemperature - b.thermodynamicTemperature),
                    amountOfSubstance: (sbyte)(a.amountOfSubstance - b.amountOfSubstance),
                    luminousIntensity: (sbyte)(a.luminousIntensity - b.luminousIntensity)
                );
            }
        }
        public static Unit operator ^(in Unit a, int power)
        {
            if (power == 0) return One;
            if (power == 1) return a;
            if (power == -1) return Invert(in a);

            if ((uint)(power + MaxReasonablePower) > (uint)(2 * MaxReasonablePower))
                throw new ArgumentOutOfRangeException(
                    nameof(power),
                    power,
                    $"Power must be in range [-{MaxReasonablePower}, {MaxReasonablePower}]");

            checked
            {
                return new Unit(
                    length: (sbyte)(a.length * power),
                    mass: (sbyte)(a.mass * power),
                    time: (sbyte)(a.time * power),
                    electricCurrent: (sbyte)(a.electricCurrent * power),
                    thermodynamicTemperature: (sbyte)(a.thermodynamicTemperature * power),
                    amountOfSubstance: (sbyte)(a.amountOfSubstance * power),
                    luminousIntensity: (sbyte)(a.luminousIntensity * power)
                );
            }
        }

        public override string ToString()
        {
            return
                $"m^{length} " +
                $"kg^{mass} " +
                $"s^{time} " +
                $"A^{electricCurrent} " +
                $"K^{thermodynamicTemperature} " +
                $"mol^{amountOfSubstance} " +
                $"cd^{luminousIntensity}";
        }

        public bool Equals(Unit other)
        {
            return
                length == other.length &&
                mass == other.mass &&
                time == other.time &&
                electricCurrent == other.electricCurrent &&
                thermodynamicTemperature == other.thermodynamicTemperature &&
                amountOfSubstance == other.amountOfSubstance &&
                luminousIntensity == other.luminousIntensity;
        }

        public static bool operator ==(Unit left, Unit right) => left.Equals(right);
        public static bool operator !=(Unit left, Unit right) => !left.Equals(right);

        public override int GetHashCode()
        {
            return HashCode.Combine(
                length,
                mass,
                time,
                electricCurrent,
                thermodynamicTemperature,
                amountOfSubstance,
                luminousIntensity);
        }

        public override bool Equals(object? obj)
        {
            return obj is Unit && Equals((Unit)obj);
        }
    }
}