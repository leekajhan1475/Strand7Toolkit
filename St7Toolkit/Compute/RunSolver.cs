
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using St7API;

namespace St7Toolkit
{
    public static partial class Compute
    {
        internal static bool RunSolver(int uID, St7Solver solver, out string errorMessage)
        {
            string err_msg;

            int iErr = St7.St7RunSolver(
                uID, 
                Convert.ToInt32(solver.GetType()), 
                Convert.ToInt32(solver.GetMode()), 
                St7.btTrue
                );

            if (iErr != St7.ERR7_NoError)
            {
                Compute.GetErrorMessage(iErr, out err_msg);
                errorMessage = err_msg;
                return false;
            }

            errorMessage = "No errors found";

            return true;
        }
        
    }
}
