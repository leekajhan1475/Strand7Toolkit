using St7API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace St7Toolkit
{
    public class UCSSetting
    {
        #region Class Fields
        private int _id = -1;

        private string _name = string.Empty;

        private UCSType _ucsType = UCSType.Cartesian;

        private double _torodalRadius = 1;

        private Rhino.Geometry.Plane _axisSystem = Rhino.Geometry.Plane.Unset;

        private static UCSSetting _xy = new UCSSetting(Rhino.Geometry.Plane.WorldXY);

        private static UCSSetting _yz = new UCSSetting(Rhino.Geometry.Plane.WorldYZ);

        private static UCSSetting _zx = new UCSSetting(Rhino.Geometry.Plane.WorldZX);
        #endregion

        #region Class Properties
        /// <summary>
        /// Default setting of UCS as Global XY-Plane
        /// </summary>
        public static UCSSetting GlobalXY { get => UCSSetting._xy; }

        /// <summary>
        /// Default setting of UCS as Global YZ-Plane
        /// </summary>
        public static UCSSetting GlobalYZ { get => UCSSetting._yz; }

        /// <summary>
        /// Default setting of UCS as Global ZX-Plane
        /// </summary>
        public static UCSSetting GlobalZX { get => UCSSetting._zx; }

        /// <summary>
        /// Gets or sets the entity number of this UCSSetting.
        /// </summary>
        public int EntityId
        {
            get => this._id;
            set => this._id = value;
        }

        /// <summary>
        /// Gets or sets the name of this UCS setting.
        /// </summary>
        public string Name
        {
            get => this._name;
            set => this._name = value;
        }

        /// <summary>
        /// True if this UCS setting is valid/legal, or false otherwise.<para/>
        /// For a UCS setting to be valid, the following properties should not be unset/empty/null:<br/>
        /// 1. Requires/Holds a defined axis system.<br/>
        /// 2. Owns an Id.<br/>
        /// 3. A given name.
        /// </summary>
        public bool IsValid
        {
            get => (this.AxisSystem != Rhino.Geometry.Plane.Unset && 
                    this.EntityId > 0 && 
                    this.Name != string.Empty);
        }

        /// <summary>
        /// User Coordinate System type
        /// </summary>
        internal UCSType SystemType
        {
            get => this._ucsType;
            set => this._ucsType = value;
        }

        /// <summary>
        /// Coordinate/Axis system as Cartesian plane
        /// </summary>
        public Rhino.Geometry.Plane AxisSystem
        {
            get => this._axisSystem;
            set => this._axisSystem = value;
        }
        #endregion

        #region Class Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="plane"></param>
        /// <param name="torosRadius"></param>
        public UCSSetting(Rhino.Geometry.Plane plane, double torosRadius = 1)
        {
            this._axisSystem = plane;
            this._torodalRadius = torosRadius;
        }
        #endregion

        #region Class Methods
        /// <summary>
        /// Sets the axisArray<br/>
        /// An 10 element array [0..kMaxUCSDoubles-1] defining the UCS axis system:<br/>
        /// [0,1,2] Origin point in global XYZ coordinates<br/>
        /// [3,4,5] First plane point in global XYZ coordinates<br/>
        /// [6,7,8] Second plane point in global XYZ coordinates<br/>
        /// [9] Toroidal radius
        /// </summary>
        internal void ToArray(out double[] arr)
        {
            // Sets the axisArray
            // An 10 element array [0..kMaxUCSDoubles-1] defining the UCS axis system:
            // [0,1,2] Origin point in global XYZ coordinates
            // [3,4,5] First plane point in global XYZ coordinates
            // [6,7,8] Second plane point in global XYZ coordinates
            // [9] Toroidal radius
            double[] ucsArr = new double[St7.kMaxUCSDoubles];

            if (!this.IsValid) { arr = new double[1] { -1 }; }

            // Sets coordinate system origin XYZ
            ucsArr[0] = this._axisSystem.Origin.X;
            ucsArr[1] = this._axisSystem.Origin.Y;
            ucsArr[2] = this._axisSystem.Origin.Z;
            // Sets coordinate system XAxis
            ucsArr[3] = this._axisSystem.XAxis.X;
            ucsArr[4] = this._axisSystem.XAxis.Y;
            ucsArr[5] = this._axisSystem.XAxis.Z;
            // Sets coordinate system YAxis
            ucsArr[6] = this._axisSystem.YAxis.X;
            ucsArr[7] = this._axisSystem.YAxis.Y;
            ucsArr[8] = this._axisSystem.YAxis.Z;
            // Set Toros radius by default 1 unit system
            ucsArr[9] = this._torodalRadius;

            arr = ucsArr;
        }
        #endregion
    }
}
