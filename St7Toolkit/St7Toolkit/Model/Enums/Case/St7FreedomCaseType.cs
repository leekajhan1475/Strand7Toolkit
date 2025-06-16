
using St7API;

namespace St7Toolkit
{
    /// <summary>
    /// </summary>
    public enum St7FreedomCaseType
    {
        None = -1,

        /// <summary>
        /// Normal freedom case - Default
        /// </summary>
        Normal = St7.fcNormalFreedom,

        /// <summary>
        /// Free body XYZ-space
        /// </summary>
        FreeXYZ = St7.fcFreeBodyInertiaRelief,

        /// <summary>
        /// Single-symmetry about XY-Plane
        /// </summary>
        Sym1XY = St7.fcSingleSymmetryInertiaXY,

        /// <summary>
        /// Single-symmetry about YZ-Plane
        /// </summary>
        Sym1YZ = St7.fcSingleSymmetryInertiaYZ,

        /// <summary>
        /// Single-symmetry about XZ-Plane
        /// </summary>
        Sym1XZ = St7.fcSingleSymmetryInertiaZX,

        /// <summary>
        /// Double-symmetry alonhg X-Axis
        /// </summary>
        Sym2X = St7.fcDoubleSymmetryInertiaX,

        /// <summary>
        /// Double-symmetry alonhg Y-Axis
        /// </summary>
        Sym2Y = St7.fcDoubleSymmetryInertiaY,

        /// <summary>
        /// Double-symmetry alonhg Z-Axis
        /// </summary>
        Sym2Z = St7.fcDoubleSymmetryInertiaZ,

        /// <summary>
        /// 
        /// </summary>
        Unset = 255
    }
}
