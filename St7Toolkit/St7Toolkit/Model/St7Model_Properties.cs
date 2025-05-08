using System;
using System.Collections.Generic;
using System.Text;
using St7Toolkit.Element;
using St7API;
using Rhino;
using St7Toolkit.Solver;


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

        private string _fileDir = string.Empty;

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
        public string FileDirectory { get => this._fileDir; set => this._fileDir = value; }

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
        private St7Solver<SolverSetting> _solver = new St7Solver<SolverSetting>();

        /// <summary>
        /// Get or set the current solver for the model
        /// </summary>
        public St7Solver<SolverSetting> Solver
        {
            get => this._solver;
            set => this._solver = value;
        }

        /// <summary>
        /// For a Strand7 model file to be valid: <para/>
        /// 1. The associated model Id must be greater than 0 <para/>
        /// 2. It must own a file path for IO operations
        /// </summary>
        public bool IsValid
        {
            get => (this.UId <= 0 || this.FileDirectory == string.Empty) ? false : true;
        }

        /// <summary>
        /// True if this model owns a directory for reference
        /// </summary>
        public bool HasPath { get => (this.FileDirectory != string.Empty); }

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
    }
}
