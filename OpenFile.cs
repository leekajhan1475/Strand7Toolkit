using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using St7API;

namespace St7Toolkit
{
    public partial class FileIO
    {
        /// <summary>
        /// System's temporary folder at C:\TEMP
        /// </summary>
        internal static string ScratchPath { get; } = System.IO.Path.GetTempPath();

        /// <summary>
        /// Create a new Strand7 model file
        /// </summary>
        /// <param name="uID"></param>
        /// <param name="fileDirectory"></param>
        /// <param name="scratchPath"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        internal static bool NewFile(int uID, string fileDirectory, string scratchPath)
        {
            // Abort if the model has no associate file name
            if (fileDirectory == null) { return false; }
            int iErr = St7.St7NewFile(uID, fileDirectory, scratchPath);
            return (iErr == St7.ERR7_NoError);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uID"></param>
        /// <param name="fileName"></param>
        /// <param name="scratchPath"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        internal static bool OpenFile(int uID, string fileDirectory, string scratchPath)
        {
            int iErr = St7.St7OpenFile(uID, fileDirectory, scratchPath);
            return (iErr == St7.ERR7_NoError);
        }

        /// <summary>
        /// Try to open file for read only, if the method failed to open result file, 
        /// it will return a false value, or else it will return a true value.
        /// </summary>
        /// <param name="uID"></param>
        /// <param name="fileName"></param>
        /// <param name="scratchPath"></param>
        /// <returns></returns>
        internal static bool OpenFileReadOnly(int uID, string fileName, string scratchPath)
        {
            int iErr = St7.St7OpenFileReadOnly(uID, fileName, scratchPath);
            return (iErr == St7.ERR7_NoError);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uID"></param>
        /// <returns></returns>
        internal static bool CloseFile(int uID)
        {
            int iErr = St7.St7CloseFile(uID);
            return (iErr == St7.ERR7_NoError);
        }
    }
}
