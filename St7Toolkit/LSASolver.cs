using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using St7API;

namespace St7Toolkit
{
    public partial class St7Solver
    {
        /// <summary>
        /// 
        /// </summary>
        public enum Type
        {
            LSA = St7.stLinearStatic,
            LBA = St7.stLinearBuckling,
            LIA = St7.stLoadInfluence,
            NLA = St7.stNonlinearStatic,
            QSA = St7.stQuasiStatic,
            NFA = St7.stNaturalFrequency,
            NaN = -1
        }

        /// <summary>
        /// 
        /// </summary>
        public enum Mode
        {
            NORMAL = St7.smNormalRun,
            SHOWPROGRESS = St7.smProgressRun,
            BACKGROUND = St7.smBackgroundRun,
            CLOSERUN = St7.smNormalCloseRun,
            NaN = -1
        }

        /// <summary>
        /// 
        /// </summary>
        private Enum? SolverType;

        /// <summary>
        /// 
        /// </summary>
        private Enum? SolverMode;

        /// <summary>
        /// 
        /// </summary>
        public bool IsValid
        {
            get { return (this.SolverType == null || this.SolverMode == null) ? false : true; }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public St7Solver()
        {
            this.SolverType = Type.NaN;
            this.SolverMode = Mode.NaN;
        }

        /// <summary>
        /// Construct a St7Solver of normal mode.
        /// </summary>
        /// <param name="solverType">
        /// One of the solver types listed in Strand7 API Manual Solver Types
        /// </param>
        public St7Solver(Enum solverType)
        {
            this.SolverType = solverType;
            this.SolverMode = Mode.NORMAL;
        }

        /// <summary>
        /// Gets the current solver type
        /// </summary>
        /// <returns>
        /// Solver type
        /// </returns>
        public new Enum GetType()
        {
            if (this.SolverType == null) return St7Solver.Type.NaN;
            return this.SolverType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Enum GetMode()
        {
            if(this.SolverMode == null) return St7Solver.Mode.NaN;
            return this.SolverMode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="solverMode"></param>
        public void SetMode(Enum solverMode)
        {
            this.SolverMode = solverMode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="solverType"></param>
        public void SetType(Enum solverType)
        {
            this.SolverType = solverType;
        }
    }
}
