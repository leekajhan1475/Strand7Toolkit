
using Rhino.Geometry;
using System.Runtime.CompilerServices;

namespace St7Toolkit.Element
{
    public class St7Node
    {
        #region Class Fields
        private int _id = -1;

        private Rhino.Geometry.Point3d _origin = Rhino.Geometry.Point3d.Unset;

        private bool _isConstraint = false;

        private UCSSetting _localAxisSystem = UCSSetting.GlobalXY;

        private static St7Node _unset = new St7Node();
        #endregion

        #region Class Properties
        /// <summary>
        /// Gets or sets the local UCS setting, which is set as Plane.WorldXY by default
        /// </summary>
        public UCSSetting UCSSetting
        {
            get => this._localAxisSystem;
            set => this._localAxisSystem = value;
        }

        /// <summary>
        /// Get or set the entity number of the Node element.
        /// </summary>
        public int EntityId
        { 
            get => this._id; 
            set => this._id = value; 
        }

        /// <summary>
        /// True if this Node owns a local axis that is not Global XY-Plane.
        /// </summary>
        public bool HasLocalAxis { get => (this.UCSSetting != UCSSetting.GlobalXY); }

        /// <summary>
        /// Get or set the origin of the Node element.
        /// </summary>
        public Point3d Origin 
        { 
            get => this._origin; 
            set => this._origin = value; 
        }

        /// <summary>
        /// For a Node element to be valid:<br />
        /// 1. The enity number must be greater than 0<br />
        /// 2. It must have X, Y, and Z coordinates
        /// </summary>
        public bool IsValid
        {
            get
            {
                if (this.EntityId < 1) return false;
                if (!this.Origin.IsValid) return false;
                return true;
            }
        }

        /// <summary>
        /// Set this Node element translation or rotation constrain condition.
        /// </summary>
        public bool Constrained 
        {
            get => this._isConstraint;
            set => this._isConstraint = value; 
        }

        /// <summary>
        /// Gets the unset Node.
        /// </summary>
        public static St7Node Unset { get => St7Node._unset; }
        #endregion

        #region Class Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public St7Node()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public St7Node(double x, double y, double z)
        {
            this.Origin = new Point3d(x,y,z);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityNumber"></param>
        /// <param name="origin"></param>
        public St7Node(int entityNumber, Point3d origin) : 
            this(origin.X, origin.Y, origin.Z)
        {
            this.EntityId = entityNumber;
        }
        #endregion

        #region Class methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal double[] GetOriginAsArray()
        {
            return new double[] { this.Origin.X, this.Origin.Y, this.Origin.Z };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = $"Node {this.EntityId.ToString()}:\n" + 
                         $"Origin-{{{this.Origin.X.ToString()},{this.Origin.Y.ToString()},{this.Origin.Z.ToString()}}}" +
                         $"Constrained-{{{this.Constrained}}}";
            return str;
        }
        #endregion
    }
}
