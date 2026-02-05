using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptLib.Units
{
    /// <summary>
    /// Canonical dimensional definitions of common physical quantities
    /// expressed using SI base dimensions.
    /// </summary>
    public static class UnitStandard
    {
        #region Base
        /// <summary>Length or distance in space.</summary>
        public static readonly Unit Length = new Unit(length: 1);8

        /// <summary>Mass of a body.</summary>
        public static readonly Unit Mass = new Unit(mass: 1);

        /// <summary>Time duration.</summary>
        public static readonly Unit Time = new Unit(time: 1);

        /// <summary>Electric current.</summary>
        public static readonly Unit Current = new Unit(electricCurrent: 1);

        /// <summary>Thermodynamic temperature.</summary>
        public static readonly Unit Temperature = new Unit(thermodynamicTemperature: 1);

        /// <summary>Amount of substance.</summary>
        public static readonly Unit AmountOfSubstance = new Unit(amountOfSubstance: 1);

        /// <summary>Luminous intensity.</summary>
        public static readonly Unit LuminousIntensity = new Unit(luminousIntensity: 1);

        /// <summary>Dimensionless scalar quantity.</summary>
        public static readonly Unit Scalar = Unit.One;
        #endregion Base

        #region Geometry & kinematics
        /// <summary>Two-dimensional extent.</summary>
        public static readonly Unit Area = Length ^ 2;

        /// <summary>Three-dimensional extent.</summary>
        public static readonly Unit Volume = Length ^ 3;

        /// <summary>Rate of change of position.</summary>
        public static readonly Unit Velocity = Length / Time;

        /// <summary>Rate of change of velocity.</summary>
        public static readonly Unit Acceleration = Velocity / Time;

        /// <summary>Rate of change of acceleration.</summary>
        public static readonly Unit Jerk = Acceleration / Time;

        /// <summary>Rate of angular displacement.</summary>
        public static readonly Unit AngularVelocity = Scalar / Time;

        /// <summary>Rate of change of angular velocity.</summary>
        public static readonly Unit AngularAcceleration = AngularVelocity / Time;

        /// <summary>Number of occurrences per unit time.</summary>
        public static readonly Unit Frequency = Scalar / Time;

        /// <summary>Duration of one cycle.</summary>
        public static readonly Unit Period = Time;
        #endregion Geometry & kinematics

        #region Dynamics
        /// <summary>Interaction that causes acceleration.</summary>
        public static readonly Unit Force = Mass * Length * Time ^ -2;

        /// <summary>Quantity of motion.</summary>
        public static readonly Unit Momentum = Mass * Velocity;

        /// <summary>Change of momentum over time.</summary>
        public static readonly Unit Impulse = Force * Time;

        /// <summary>Rotational effect of a force.</summary>
        public static readonly Unit Moment = Force * Length;

        /// <summary>Rotational analog of momentum.</summary>
        public static readonly Unit AngularMomentum = Momentum * Length;

        /// <summary>Capacity to perform work.</summary>
        public static readonly Unit Energy = Force * Length;

        /// <summary>Rate of energy transfer.</summary>
        public static readonly Unit Power = Energy / Time;

        /// <summary>Power per unit volume.</summary>
        public static readonly Unit PowerDensity = Power / Volume;

        /// <summary>Energy per unit volume.</summary>
        public static readonly Unit EnergyDensity = Energy / Volume;
        #endregion Dynamics

        #region Mass distribution
        /// <summary>Mass per unit volume.</summary>
        public static readonly Unit Density = Mass / Volume;

        /// <summary>Mass per unit length.</summary>
        public static readonly Unit LinearDensity = Mass / Length;

        /// <summary>Mass per unit area.</summary>
        public static readonly Unit SurfaceDensity = Mass / Area;

        /// <summary>Volume occupied per unit mass.</summary>
        public static readonly Unit SpecificVolume = Volume / Mass;

        /// <summary>Energy per unit mass.</summary>
        public static readonly Unit SpecificEnergy = Energy / Mass;

        /// <summary>Power per unit mass.</summary>
        public static readonly Unit SpecificPower = Power / Mass;
        #endregion Mass distribution

        #region Stress & strain
        /// <summary>Force applied per unit area.</summary>
        public static readonly Unit Pressure = Force / Area;

        /// <summary>Internal force per unit area.</summary>
        public static readonly Unit Stress = Pressure;

        /// <summary>Relative deformation.</summary>
        public static readonly Unit Strain = Scalar;

        /// <summary>Rate of deformation.</summary>
        public static readonly Unit StrainRate = Strain / Time;

        /// <summary>Elastic stiffness in tension.</summary>
        public static readonly Unit YoungModulus = Pressure;

        /// <summary>Elastic stiffness in shear.</summary>
        public static readonly Unit ShearModulus = Pressure;

        /// <summary>Elastic stiffness under uniform compression.</summary>
        public static readonly Unit BulkModulus = Pressure;

        /// <summary>Inverse measure of stiffness.</summary>
        public static readonly Unit Compliance = Pressure ^ -1;
        #endregion Stress & strain

        #region Thermodynamics
        /// <summary>Thermal energy transfer.</summary>
        public static readonly Unit Heat = Energy;

        /// <summary>Rate of heat transfer.</summary>
        public static readonly Unit HeatFlowRate = Power;

        /// <summary>Heat transfer per unit area.</summary>
        public static readonly Unit HeatFlux = Power / Area;

        /// <summary>Ability to conduct heat.</summary>
        public static readonly Unit ThermalConductivity = Power / (Length * Temperature);

        /// <summary>Energy required to change temperature.</summary>
        public static readonly Unit HeatCapacity = Energy / Temperature;

        /// <summary>Heat capacity per unit mass.</summary>
        public static readonly Unit SpecificHeatCapacity = HeatCapacity / Mass;

        /// <summary>Measure of thermal disorder.</summary>
        public static readonly Unit Entropy = Energy / Temperature;

        /// <summary>Entropy per unit mass.</summary>
        public static readonly Unit SpecificEntropy = Entropy / Mass;

        /// <summary>Total heat content.</summary>
        public static readonly Unit Enthalpy = Energy;

        /// <summary>Enthalpy per unit mass.</summary>
        public static readonly Unit SpecificEnthalpy = Energy / Mass;

        /// <summary>Energy available to do work at constant temperature.</summary>
        public static readonly Unit GibbsFreeEnergy = Energy;

        /// <summary>Energy available to do work at constant volume.</summary>
        public static readonly Unit HelmholtzFreeEnergy = Energy;

        /// <summary>Resistance to heat flow.</summary>
        public static readonly Unit ThermalResistance = Temperature / Power;

        /// <summary>Ability to conduct heat.</summary>
        public static readonly Unit ThermalConductance = Power / Temperature;

        /// <summary>Rate of thermal diffusion.</summary>
        public static readonly Unit ThermalDiffusivity = Area / Time;
        #endregion Thermodynamics

        #region Fluid mechanics
        /// <summary>Volume passing per unit time.</summary>
        public static readonly Unit VolumetricFlowRate = Volume / Time;

        /// <summary>Mass passing per unit time.</summary>
        public static readonly Unit MassFlowRate = Mass / Time;

        /// <summary>Resistance to flow.</summary>
        public static readonly Unit DynamicViscosity = Pressure * Time;

        /// <summary>Viscosity normalized by density.</summary>
        public static readonly Unit KinematicViscosity = Area / Time;

        /// <summary>Ability of a medium to transmit fluids.</summary>
        public static readonly Unit Permeability = Area;

        /// <summary>Flow velocity through porous media.</summary>
        public static readonly Unit HydraulicConductivity = Velocity;

        /// <summary>Equivalent fluid column height.</summary>
        public static readonly Unit PressureHead = Length;

        /// <summary>Total hydraulic energy per unit weight.</summary>
        public static readonly Unit HydraulicHead = Length;
        #endregion Fluid mechanics

        #region Electricity & magnetism
        /// <summary>Electric charge.</summary>
        public static readonly Unit Charge = Current * Time;

        /// <summary>Charge per unit volume.</summary>
        public static readonly Unit ChargeDensity = Charge / Volume;

        /// <summary>Current per unit area.</summary>
        public static readonly Unit CurrentDensity = Current / Area;

        /// <summary>Electric potential difference.</summary>
        public static readonly Unit Voltage = Power / Current;

        /// <summary>Electric force per unit charge.</summary>
        public static readonly Unit ElectricField = Voltage / Length;

        /// <summary>Opposition to electric current.</summary>
        public static readonly Unit Resistance = Voltage / Current;

        /// <summary>Ability to conduct electric current.</summary>
        public static readonly Unit Conductance = Resistance ^ -1;

        /// <summary>Material resistance per unit length.</summary>
        public static readonly Unit Resistivity = Resistance * Length;

        /// <summary>Material ability to conduct electricity.</summary>
        public static readonly Unit Conductivity = Resistivity ^ -1;

        /// <summary>Ability to store electric charge.</summary>
        public static readonly Unit Capacitance = Charge / Voltage;

        /// <summary>Ability to store magnetic energy.</summary>
        public static readonly Unit Inductance = Voltage * Time / Current;

        /// <summary>Total magnetic field passing through an area.</summary>
        public static readonly Unit MagneticFlux = Voltage * Time;

        /// <summary>Magnetic flux per unit area.</summary>
        public static readonly Unit MagneticFluxDensity = MagneticFlux / Area;

        /// <summary>Magnetic field intensity.</summary>
        public static readonly Unit MagneticFieldStrength = Current / Length;

        /// <summary>Material response to magnetic field.</summary>
        public static readonly Unit MagneticPermeability = MagneticFlux / (Current * Length);

        /// <summary>Opposition to magnetic flux.</summary>
        public static readonly Unit MagneticReluctance = MagneticFlux ^ -1;
        #endregion Electricity & magnetism

        #region Dimensionless numbers
        /// <summary>Ratio of inertial to viscous forces.</summary>
        public static readonly Unit ReynoldsNumber = Scalar;

        /// <summary>Ratio of momentum to thermal diffusivity.</summary>
        public static readonly Unit PrandtlNumber = Scalar;

        /// <summary>Ratio of convective to conductive heat transfer.</summary>
        public static readonly Unit NusseltNumber = Scalar;

        /// <summary>Ratio of flow speed to wave speed.</summary>
        public static readonly Unit MachNumber = Scalar;

        /// <summary>Ratio of oscillatory to steady flow effects.</summary>
        public static readonly Unit StrouhalNumber = Scalar;

        /// <summary>Ratio of internal to surface resistance.</summary>
        public static readonly Unit BiotNumber = Scalar;

        /// <summary>Ratio of diffusive transport rate.</summary>
        public static readonly Unit FourierNumber = Scalar;

        /// <summary>Ratio of molecular to continuum length scales.</summary>
        public static readonly Unit KnudsenNumber = Scalar;
        #endregion Dimensionless numbers
    }
}
