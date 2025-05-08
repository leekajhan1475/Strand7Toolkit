using St7API;
using System;
using System.Runtime.CompilerServices;

namespace St7Toolkit
{
    /// <summary>
    /// Measuring energy in units of joules, kilojoules, British thermal units, foot pounds-force or calories respectively.
    /// </summary>
    public enum EnergyUnitSystem
    {
        /// <summary>
        /// No length unit system is specified and model length unit system should be used.
        /// </summary>
        None = 0,

        /// <summary>
        /// Joules
        /// </summary>
        J = St7.euJOULE,

        /// <summary>
        /// Kilo-joules
        /// </summary>
        Kj = St7.euKILOJOULE,

        /// <summary>
        /// British thermal unit
        /// </summary>
        Btu = St7.euBTU,

        /// <summary>
        /// Foot pounds-force(lbf)
        /// </summary>
        FtLbF = St7.euFTLBF,

        /// <summary>
        /// Calories
        /// </summary>
        Cal = St7.euCALORIE,

        /// <summary>
        /// No unit system is set
        /// </summary>
        Unset = 255
    }
}
