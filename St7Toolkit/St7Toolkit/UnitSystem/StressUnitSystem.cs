using Rhino.DocObjects;
using St7API;

namespace St7Toolkit
{
    /// <summary>
    /// Stress in units of Pa, kPa, MPa, kgf/cm2, psi, ksi, and lb/ft2
    /// </summary>
    public enum StressUnitSystem
    {
        /// <summary>
        /// No stress unit system is specified
        /// </summary>
        None = 0,

        /// <summary>
        /// Pa - Pascal
        /// </summary>
        Pa = St7.suPASCAL,

        /// <summary>
        /// kPa - Kilopascal
        /// </summary>
        KPa = St7.suKILOPASCAL,

        /// <summary>
        /// MPa - Megapascal
        /// </summary>
        MPa = St7.suMEGAPASCAL,

        /// <summary>
        /// kgf/cm2 - kilograms-force per square centimeter
        /// </summary>
        Kgf_Sqcm = St7.suKSCm,

        /// <summary>
        /// psi - pounds per square inch
        /// </summary>
        Psi = St7.suPSI,

        /// <summary>
        /// ksi - kilopounds per square inch
        /// </summary>
        Ksi = St7.suKSI,

        /// <summary>
        /// lb/ft2 - Pounds per square foot
        /// </summary>
        Lb_Sqft = St7.suPSF,

        /// <summary>
        /// No unit system is set
        /// </summary>
        Unset = 255
    }
}
