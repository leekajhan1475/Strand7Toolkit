using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using St7API;

namespace St7Toolkit
{
    public partial class FileIO
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="solver"></param>
        /// <returns></returns>
        internal static string St7SolverResultPath(string fileName, St7Solver solver)
        {
            // Return null if input file name is bogus
            if (fileName == null) return "NaN";
            // Check if solver is valid, return null if not
            if (!solver.IsValid) return "NaN";
            // Gets solver type as string
            string solverType = solver.GetType().ToString();
            // Get file name according to selected solver
            string resultPath = fileName.Split('.')[0] + "." + solverType;
            return resultPath;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="uID"></param>
        /// <param name="fileName"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        private static int ValidateResultFile(int uID, string fileName, out int[] flags)
        {
            int[] result = new int[2];
            int validation = -1;
            int solverType = -1;

            int iErr = St7.St7ValidateResultFile(uID, fileName, ref validation, ref solverType);

            result[0] = validation;
            result[1] = solverType;

            flags = result;

            return iErr;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="uID"></param>
        /// <param name="fileName"></param>
        /// <param name="resultCases"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        internal static bool OpenResultFile(int uID, string fileName, out int[] resultCases, out string errorMessage)
        {
            string err_msg;

            int[] flags;

            int iErr = FileIO.ValidateResultFile(uID, fileName, out flags);

            /*
             * check if result file is valid
             * if result file is not valid, retrieve and forward the error message
            */
            if (iErr != St7.ERR7_NoError) 
            {
                resultCases = new int[] { -1 };
                Compute.GetErrorMessage(iErr, out err_msg);
                errorMessage = err_msg;
                return false;
            }

            /*
             * try open the result file
            */

            // Check if file name is valid
            if (fileName == null) 
            {
                resultCases = new int[] { -1 };
                errorMessage = "Check result file's path and name";
                return false;
            }

            int numPrimary = -1;
            int numSecondary = -1;

            iErr = St7.St7OpenResultFile(
                uID, 
                fileName, 
                "",
                St7.kUseExistingCombinations,
                ref numPrimary,
                ref numSecondary
                );

            int[] result = new int[] { numPrimary, numSecondary };

            /*
             * if errors occurred, retrieve and forward the error message
             */
            if (iErr != St7.ERR7_NoError)
            {
                resultCases = new int[] { -1 };
                Compute.GetErrorMessage(iErr, out err_msg);
                errorMessage = err_msg;
                return false;
            }

            resultCases = result;
            errorMessage = "No errors found";

            return true;
        }


        

    }
}
