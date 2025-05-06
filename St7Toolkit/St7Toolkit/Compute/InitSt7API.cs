using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using St7API;
using System.ComponentModel;

namespace St7Toolkit
{
    public static partial class Compute
    {
        /// <summary>
        /// Initialise Strand7 API
        /// </summary>
        /// <param name="errorMessage">
        /// Error message for handling
        /// </param>
        /// <returns>
        /// True if API initialised without errors
        /// </returns>
        public static bool InitSt7API()
        {
            int iErr = St7.St7Init();
            return (iErr == St7.ERR7_NoError);
        }

        /// <summary>
        /// Release Strand7 API
        /// </summary>
        /// <param name="errorMessage">
        /// Error message for handling
        /// </param>
        /// <returns>
        /// True if API released without errors
        /// </returns>
        public static bool ReleaseSt7API()
        {
            int iErr = St7.St7Release();
            return (iErr == St7.ERR7_NoError);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        internal static bool GetErrorMessage(int errorCode, out string errorMessage) 
        {
            StringBuilder errorString = new StringBuilder();
            int iErr = St7.St7GetSolverErrorString(errorCode, errorString, St7.kMaxStrLen);
            errorMessage = errorString.ToString();
            return (iErr == St7.ERR7_NoError);
        }
    }
}
