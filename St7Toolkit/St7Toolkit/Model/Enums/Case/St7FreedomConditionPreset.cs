
using St7API;

namespace St7Toolkit
{
    /// <summary>
    /// </summary>
    public enum St7FreedomConditionPreset
    {
        /// <summary>
        /// XYPlane freedom case Dofs preset - Default<br />
        /// Lock: tZ, rX, rY, rZ
        /// </summary>
        Default2DPlane = 0,

        /// <summary>
        /// XYPlane freedom case Dofs preset<br />
        /// Lock: tZ, rX, rY
        /// </summary>
        BeamXYPlane = 1,

        /// <summary>
        /// YZPlane freedom case Dofs preset<br />
        /// Lock: tX, rY, rZ
        /// </summary>
        BeamYZPlane = 2,

        /// <summary>
        /// ZXPlane freedom case Dofs preset<br />
        /// Lock: tY, rX, rZ
        /// </summary>
        BeamZXPlane = 3,

        /// <summary>
        /// Brick elements freedom case Dofs preset<br />
        /// Lock: rX, rY, rZ
        /// </summary>
        Brick3D = 4,

        /// <summary>
        /// No constraints on all axes.
        /// </summary>
        AllFree = 5,

        Unset = 255
    }
}
