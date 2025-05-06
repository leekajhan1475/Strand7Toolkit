using System;
using St7Toolkit.Element;

namespace St7Toolkit.Types
{
    /// <summary>
    /// Utility module that caches a number of oft-used types and type-ids for quick
    /// access. Do not change any of the values in this module or you'll wreck casting
    /// and conversion logic.
    /// </summary>
    public static class St7ObjectTypes
    {
        /// <summary>
        /// Type of St7Toolkit.Element.Node
        /// </summary>
        public static Type tNODE = typeof(Node);

        /// <summary>
        /// Type of St7Toolkit.Element.Beam
        /// </summary>
        public static Type tyBEAM = typeof(Beam);

        /// <summary>
        /// Type of St7Toolkit.Element.Plate
        /// </summary>
        public static Type tyPLATE = typeof(Plate);

        /// <summary>
        /// Type of St7Toolkit.Element.Plate
        /// </summary>
        public static Type tyMODEL = typeof(St7Model);

        /// <summary>
        /// Guid of type St7Toolkit.Element.Node
        /// </summary>
        public static Guid idNODE = tNODE.GUID;

        /// <summary>
        /// Guid of type St7Toolkit.Element.Beam
        /// </summary>
        public static Guid idBEAM = tyBEAM.GUID;

        /// <summary>
        /// Guid of type St7Toolkit.Element.Plate
        /// </summary>
        public static Guid idPLATE = tyPLATE.GUID;

        /// <summary>
        /// Guid of type St7Toolkit.Element.Plate
        /// </summary>
        public static Guid idMODEL = tyMODEL.GUID;
    }
}