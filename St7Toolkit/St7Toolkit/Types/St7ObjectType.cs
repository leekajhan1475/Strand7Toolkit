using System;
using St7Toolkit.Element;

namespace St7Toolkit.Types
{
    /// <summary>
    /// Utility module that caches a number of oft-used types and type-ids for quick access.<br />
    /// Do not change any of the values in this module or you'll wreck casting
    /// and conversion logic.
    /// </summary>
    public class St7ObjectType
    {
        /// <summary>
        /// Type of St7Toolkit.Element.Node
        /// </summary>
        public static Type tyNODE = typeof(St7Node);

        /// <summary>
        /// Type of St7Toolkit.Element.Beam
        /// </summary>
        public static Type tyBEAM = typeof(St7Beam);

        /// <summary>
        /// Type of St7Toolkit.Element.Plate
        /// </summary>
        public static Type tyPLATE = typeof(St7Plate);

        /// <summary>
        /// Type of St7Toolkit.St7CrossSection
        /// </summary>
        public static Type tyCROSSEC = typeof(St7CrossSection);

        /// <summary>
        /// Type of St7Toolkit.St7Joint
        /// </summary>
        public static Type tyJOINT = typeof(St7Joint);

        /// <summary>
        /// Type of St7Toolkit.St7Load
        /// </summary>
        public static Type tyLOAD = typeof(St7Load);

        /// <summary>
        /// Type of St7Toolkit.St7Model
        /// </summary>
        public static Type tyMODEL = typeof(St7Model);

        /// <summary>
        /// Type of St7Toolkit.St7Material
        /// </summary>
        public static Type tyMATERIAL = typeof(St7Material);

        /// <summary>
        /// Type of St7Toolkit.St7Support
        /// </summary>
        public static Type tySUPPORT = typeof(St7Support);

        /// <summary>
        /// Guid of type St7Toolkit.Element.Node
        /// </summary>
        public static Guid idNODE = tyNODE.GUID;

        /// <summary>
        /// Guid of type St7Toolkit.Element.Beam
        /// </summary>
        public static Guid idBEAM = tyBEAM.GUID;

        /// <summary>
        /// Guid of type St7Toolkit.Element.Plate
        /// </summary>
        public static Guid idPLATE = tyPLATE.GUID;

        /// <summary>
        /// Guid of type St7Toolkit.St7Model
        /// </summary>
        public static Guid idMODEL = tyMODEL.GUID;

        /// <summary>
        /// Guid of type St7Toolkit.St7CrossSection
        /// </summary>
        public static Guid idCROSSEC = tyCROSSEC.GUID;

        /// <summary>
        /// Guid of type St7Toolkit.St7Joint
        /// </summary>
        public static Guid idJOINT = tyJOINT.GUID;

        /// <summary>
        /// Guid of type St7Toolkit.St7Load
        /// </summary>
        public static Guid idLOAD = tyLOAD.GUID;

        /// <summary>
        /// Guid of type St7Toolkit.St7Material
        /// </summary>
        public static Guid idMATERIAL = tyMATERIAL.GUID;

        /// <summary>
        /// Guid of type St7Toolkit.St7Support
        /// </summary>
        public static Guid idSUPPORT = tySUPPORT.GUID;
    }
}