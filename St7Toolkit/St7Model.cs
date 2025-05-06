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

        private readonly St7Model _default = new St7Model(-1);

        private readonly LengthUnitSystem _defaultLengthU = LengthUnitSystem.Millimeters;

        private readonly ForceUnitSystem _defaultForceU = ForceUnitSystem.KN;

        private readonly StressUnitSystem _defaultStressU = StressUnitSystem.KPa;

        private readonly MassUnitSystem _defaultMassU = MassUnitSystem.Kg;

        private readonly TemperatureUnitSystem _defaultTempU = TemperatureUnitSystem.C;

        private readonly EnergyUnitSystem _defaultEnergyU = EnergyUnitSystem.Kj;

        private readonly AngleUnitSystem _defaultAngleU = AngleUnitSystem.Radians;

        /***** Unset St7Model unit systems' values *****/
        /// <summary>
        /// 
        /// </summary>
        private LengthUnitSystem _lengthUnits = LengthUnitSystem.None;

        /// <summary>
        /// 
        /// </summary>
        private ForceUnitSystem _forceUnits = ForceUnitSystem.None;

        /// <summary>
        /// 
        /// </summary>
        private StressUnitSystem _stressUnits = StressUnitSystem.None;

        /// <summary>
        /// 
        /// </summary>
        private MassUnitSystem _massUnits = MassUnitSystem.None;

        /// <summary>
        /// 
        /// </summary>
        private TemperatureUnitSystem _tempUnits = TemperatureUnitSystem.None;

        /// <summary>
        /// 
        /// </summary>
        private EnergyUnitSystem _energyUnits = EnergyUnitSystem.None;

        /// <summary>
        /// 
        /// </summary>
        private AngleUnitSystem _angleUnits = AngleUnitSystem.None;

        /// <summary>
        /// 
        /// </summary>
        public St7Model DEFAULT { get => this._default; }

        /// <summary>
        /// 
        /// </summary>
        public LengthUnitSystem DEFAULTLENGTH { get => this._defaultLengthU; }

        /// <summary>
        /// 
        /// </summary>
        public ForceUnitSystem DEFAULTFORCE { get => this._defaultForceU; }

        /// <summary>
        /// 
        /// </summary>
        public StressUnitSystem DEFAULTSTRESS { get => this._defaultStressU; }

        /// <summary>
        /// 
        /// </summary>
        public MassUnitSystem DEFAULTMASS { get => this._defaultMassU; }

        /// <summary>
        /// 
        /// </summary>
        public TemperatureUnitSystem DEFAULTTEMP { get => this._defaultTempU; }

        /// <summary>
        /// 
        /// </summary>
        public EnergyUnitSystem DEFAULTENERGY { get => this._defaultEnergyU; }

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
        public string FileDirectory { get; set; } = string.Empty;

        /// <summary>
        /// Get the text representation of the current unit systems of this St7Model
        /// </summary>
        public string Units
        {
            get
            {
                return $"St7Model Id:{this.UId.ToString()} units systems\n" + 
                       $"         Length: {this.LengthU.ToString()}\n" +
                       $"         Force: {this.ForceU.ToString()}\n" +
                       $"         Stress: {this.StressU.ToString()}\n" +
                       $"         Mass: {this.MassU.ToString()}\n" +
                       $"         Temperature: {this.TempU.ToString()}\n" +
                       $"         Energy: {this.EnergyU.ToString()}";
            }
        }

        /// <summary>
        /// Get or set the length unit system
        /// </summary>
        public LengthUnitSystem LengthU { get => this._lengthUnits; set => this._lengthUnits = value; }

        /// <summary>
        /// Get or set the force unit system
        /// </summary>
        public ForceUnitSystem ForceU { get => this._forceUnits; set => this._forceUnits = value; }

        /// <summary>
        /// Get or set the stress unit system
        /// </summary>
        public StressUnitSystem StressU { get => this._stressUnits; set => this._stressUnits = value; }

        /// <summary>
        /// Get or set the mass unit system
        /// </summary>
        public MassUnitSystem MassU { get => this._massUnits; set => this._massUnits = value; }

        /// <summary>
        /// Get or set the temperature unit system
        /// </summary>
        public TemperatureUnitSystem TempU { get => this._tempUnits; set => this._tempUnits = value; }

        /// <summary>
        /// Get or set the energy unit system
        /// </summary>
        public EnergyUnitSystem EnergyU { get => this._energyUnits; set => this._energyUnits = value; }

        /// <summary>
        /// Get or set the angle unit system
        /// </summary>
        public AngleUnitSystem AngleUnits { get => this._angleUnits; set => this._angleUnits = value; }

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

        public bool Push()
        {

            // If this model has constraints, set up here

            // Setup node constraints
            /*
            int iErr = St7.St7SetNodeRestraint6(
                this.UId,
                entityNumber,
                caseNumber,
                UCSId,
                DoF,
                enforced
                );

            if(iErr != St7.ERR7_NoError) return false;
            */
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
