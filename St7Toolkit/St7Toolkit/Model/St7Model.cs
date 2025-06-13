using System.Collections.Generic;
using St7Toolkit.Element;


namespace St7Toolkit
{
    /// <summary>
    /// Implements all of the Strand7 API methods.
    /// </summary>
    public partial class St7Model
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public St7Model() 
        {
            this._nodes = new List<St7Node>();
            this._beams = new List<St7Beam>();
            this._plates = new List<St7Plate>();
            this._supports = new List<St7Support>();
            this._joints = new List<St7Joint>();
            this._loads = new List<St7Load>();
            this._materials = new List<St7Material>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uID"></param>
        public St7Model(int uID)
        {
            this.UId = uID;
            this._nodes = new List<St7Node>();
            this._beams = new List<St7Beam>();
            this._plates = new List<St7Plate>();
            this._supports = new List<St7Support>();
            this._joints = new List<St7Joint>();
            this._loads = new List<St7Load>();
            this._materials = new List<St7Material>();
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
