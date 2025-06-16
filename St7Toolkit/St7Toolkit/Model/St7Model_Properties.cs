using System;
using System.Collections.Generic;
using System.Text;
using St7Toolkit.Element;
using St7API;
using Rhino;


namespace St7Toolkit
{
    /// <summary>
    /// Implements all of the Strand7 API methods.
    /// </summary>
    public partial class St7Model
    {
        private static double _defaultTolerance = 0.01d;

        private double _absoluteTolerance = St7Model.DEFAULTTOLERANCE;

        private int _uId;

        private bool _isChanged = false;

        private List<St7Node> _nodes;

        private List<St7Beam> _beams;

        private List<St7Plate> _plates;

        private List<St7Support> _supports;

        private List<St7Joint> _joints;

        private List<St7Load> _loads;

        private List<St7Material> _materials;

        private List<St7FreedomCase> _freedomCases;

        private List<St7LoadCase> _loadCases;

        /// <summary>
        /// 
        /// </summary>
        private LengthUnitSystem _lengthU;

        /// <summary>
        /// 
        /// </summary>
        private ForceUnitSystem _forceU;

        /// <summary>
        /// 
        /// </summary>
        private StressUnitSystem _stressU;

        /// <summary>
        /// 
        /// </summary>
        private MassUnitSystem _massU;

        /// <summary>
        /// 
        /// </summary>
        private TemperatureUnitSystem _tempU;

        /// <summary>
        /// 
        /// </summary>
        private EnergyUnitSystem _energyU;

        /// <summary>
        /// 
        /// </summary>
        private AngleUnitSystem _angleU;

        
        private readonly LengthUnitSystem _defaultUnits = LengthUnitSystem.Millimeters;

        private readonly AngleUnitSystem _defaultAngleUnit = AngleUnitSystem.Radians;


        /*
        private readonly St7Model _default = new St7Model(-1);
        */

        /*
        /// <summary>
        /// 
        /// </summary>
        public St7Model DEFAULT { get => this._default; }
        

        /// <summary>
        /// 
        /// </summary>
        public LengthUnitSystem DEFAULTLENGTH { get => this._defaultUnits; }

        /// <summary>
        /// 
        /// </summary>
        public AngleUnitSystem DEFAULTANGLE { get => this._defaultAngleUnit; }
        */

        public static double DEFAULTTOLERANCE
        {
            get => St7Model._defaultTolerance;
        }

        public double ModelAbsoluteTolerance
        {
            get => this._absoluteTolerance;
            set => this._absoluteTolerance = value;
        }

        /// <summary>
        /// Get or set the model file Id of this St7Model.
        /// </summary>
        public int UId 
        { 
            get => this._uId; 
            set => this._uId = value; 
        }

        /// <summary>
        /// 
        /// </summary>
        public List<St7Node> Nodes { get => this._nodes; }

        /// <summary>
        /// 
        /// </summary>
        public List<St7Beam> Beams { get => this._beams; }

        /// <summary>
        /// 
        /// </summary>
        public List<St7Plate> Plates { get => this._plates; }

        /// <summary>
        /// Get the supports own by this St7Model.
        /// </summary>
        public List<St7Support> Supports { get => this._supports; }

        /// <summary>
        /// Get the joints own by this St7Model.
        /// </summary>
        public List<St7Joint> Joints { get => this._joints; }

        /// <summary>
        /// Get the loads applied to this St7Model.
        /// </summary>
        public List<St7Load> Loads { get => this._loads; }

        /// <summary>
        /// Get the materials applied to the elements of this St7Model.
        /// </summary>
        public List<St7Material> Materials { get => this._materials; }

        /// <summary>
        /// Get the available freedom cases of this St7Model.
        /// </summary>
        public List<St7FreedomCase> FreedomCases { get => this._freedomCases; }

        /// <summary>
        /// Get the available load cases of this St7Model.
        /// </summary>
        public List<St7LoadCase> LoadCases { get => this._loadCases; }

        /// <summary>
        /// 
        /// </summary>
        private string _filePath = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string FilePath
        {
            get => this._filePath;
            set => this._filePath = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public LengthUnitSystem LengthUnit { get => this._lengthU; set => this._lengthU = value; }
        
        /// <summary>
        /// 
        /// </summary>
        public ForceUnitSystem ForceUnit { get => this._forceU; set => this._forceU = value; }

        /// <summary>
        /// 
        /// </summary>
        public StressUnitSystem StressUnit { get => this._stressU; set => this._stressU = value; }

        /// <summary>
        /// 
        /// </summary>
        public MassUnitSystem MassUnit { get => this._massU; set => this._massU = value; }

        /// <summary>
        /// 
        /// </summary>
        public TemperatureUnitSystem TempUnit { get => this._tempU; set => this._tempU = value; }

        /// <summary>
        /// 
        /// </summary>
        public EnergyUnitSystem EnergyUnit { get => this._energyU; set => this._energyU = value; }

        /// <summary>
        /// 
        /// </summary>
        public AngleUnitSystem AngleUnit { get => this._angleU; set => this._angleU = value; }
        
        /// <summary>
        /// 
        /// </summary>
        private St7Solver Solver = new St7Solver();

        /// <summary>
        /// For a Strand7 model file to be valid: <para/>
        /// 1. The associated model Id must be greater than 0 <para/>
        /// 2. It must own a filepath for IO operations
        /// </summary>
        public bool IsValid { get => (this.UId > 0 && this.FilePath != string.Empty); }

        /// <summary>
        /// True if this model is modified.
        /// </summary>
        public bool IsChanged { get => this._isChanged; set => this._isChanged = value; }

        /// <summary>
        /// True if this model owns a directory for reference
        /// </summary>
        public bool HasPath
        {
            get 
            {
                return (this.FilePath!= string.Empty);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string FileName
        {
            get
            {
                if (!this.HasPath) return string.Empty;
                string[] arrString = this.FilePath.Split('\\');
                return (arrString[arrString.Length - 1]);
            }
        }
    }
}
