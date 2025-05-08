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
        private int _uId;

        private List<Node> _nodes;

        private List<Beam> _beams;

        private List<Plate> _plates;

        /*
        private readonly St7Model _default = new St7Model(-1);

        private readonly LengthUnitSystem _defaultUnits = LengthUnitSystem.Millimeters;

        private readonly AngleUnitSystem _defaultAngleUnit = AngleUnitSystem.Radians;
        */

        /// <summary>
        /// 
        /// </summary>
        private LengthUnitSystem _lengthU;

        /*
        private ForceUnitSystem _forceU;

        private StressUnitSystem _stressU;

        private MassUnitSystem _massU;

        private TemperatureUnitSystem _tempU;

        private EnergyUnitSystem _energyU;

        private AngleUnitSystem _angleU;

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

        /// <summary>
        /// 
        /// </summary>
        public int UId { get => this._uId; set => this._uId = value; }

        /// <summary>
        /// 
        /// </summary>
        public List<Node> NodeList { get => this._nodes; }

        /// <summary>
        /// 
        /// </summary>
        public List<Beam> BeamList { get => this._beams; }

        /// <summary>
        /// 
        /// </summary>
        public List<Plate> PlateList { get => this._plates; }

        /// <summary>
        /// 
        /// </summary>
        private string _fileDir = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string FileDirectory
        {
            get => this._fileDir;
            set => this._fileDir = value;
        }
            

        
        /// <summary>
        /// 
        /// </summary>
        public LengthUnitSystem LengthUnits { get => this._lengthU; set => this._lengthU = value; }

        /*
        /// <summary>
        /// 
        /// </summary>
        public ForceUnitSystem ForceUnits { get => this._forceU; set => this._forceU = value; }

        /// <summary>
        /// 
        /// </summary>
        public StressUnitSystem StressUnits { get => this._stressU; set => this._stressU = value; }

        /// <summary>
        /// 
        /// </summary>
        public MassUnitSystem Units { get => this._massU; set => this._massU = value; }

        /// <summary>
        /// 
        /// </summary>
        public TemperatureUnitSystem TempUnits { get => this._tempU; set => this._tempU = value; }

        /// <summary>
        /// 
        /// </summary>
        public EnergyUnitSystem EnergyUnits { get => this._energyU; set => this._energyU = value; }

        /// <summary>
        /// 
        /// </summary>
        public AngleUnitSystem AngleUnits { get => this._angleU; set => this._angleU = value; }
        */

        /// <summary>
        /// 
        /// </summary>
        private St7Solver Solver = new St7Solver();

        public const int PlateCombinedSystem = St7.stPlateCombined;

        public const int PlateLocalCoordinateSystem = St7.stPlateLocal;

        public const int PlateGlobalXYZSystem = St7.stPlateGlobal;

        public const int GlobalXYZSystem = 1;

        /// <summary>
        /// For a Strand7 model file to be valid: <para/>
        /// 1. The associated model Id must be greater than 0 <para/>
        /// 2. It must own a filepath for IO operations
        /// </summary>
        public bool IsValid
        {
            get
            {
                if (this.UId <= 0 || this.FileDirectory == string.Empty) { return false; }
                return true;
            }
        }

        /// <summary>
        /// True if this model owns a directory for reference
        /// </summary>
        public bool HasPath
        {
            get 
            {
                return (this.FileDirectory != string.Empty);
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
                string[] arrString = this.FileDirectory.Split('\\');
                return (arrString[arrString.Length - 1]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uID"></param>
        public St7Model(int uID)
        {
            this.UId = uID;
            this._nodes = new List<Node>();
            this._beams = new List<Beam>();
            this._plates = new List<Plate>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="solver"></param>
        /// <param name="solverMessage"></param>
        /// <returns></returns>
        public bool RunAnalysis(St7Solver solver, out string solverMessage)
        {
            // Set Strand7 model's solver
            this.Solver = solver;
            string err_msg;
            if (!Compute.RunSolver(this.UId, this.Solver, out err_msg))
            {
                solverMessage = "Fail to run solver. See following message: " + err_msg;
                return false;
            }
            solverMessage = err_msg;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="solver"></param>
        /// <returns></returns>
        public bool SetSolver(St7Solver solver)
        {
            if (!solver.IsValid) return false;
            this.Solver = solver;
            return true;
        }
    }
}
