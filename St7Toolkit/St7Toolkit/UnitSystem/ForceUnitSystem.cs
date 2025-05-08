using St7API;

namespace St7Toolkit
{
    /// <summary>
    /// Force in N, kN, mN, kgf, lbf, tf or kip
    /// </summary>
    public enum ForceUnitSystem
    {
        /// <summary>
        /// No Force unit system is specified
        /// </summary>
        None = 0,

        /// <summary>
        /// 
        /// </summary>
        N = St7.fuNEWTON,

        /// <summary>
        /// 
        /// </summary>
        KN = St7.fuKILONEWTON,

        /// <summary>
        /// 
        /// </summary>
        Kgf = St7.fuKILOFORCE,

        /// <summary>
        /// 
        /// </summary>
        Lbf = St7.fuPOUNDFORCE,

        /// <summary>
        /// 
        /// </summary>
        Tf = St7.fuTONNEFORCE,

        /// <summary>
        /// 
        /// </summary>
        Kip = St7.fuKIPFORCE,

        /// <summary>
        /// No unit system is set
        /// </summary>
        Unset = 255
    }
}
