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
        internal static bool SaveFile(int uID, bool close)
        {
            // close result files if opened
            int iErr = St7.St7CloseResultFile(uID);
            iErr = St7.St7SaveFile(uID);
            // if keyword "close" is true, close the file after save
            if(close) iErr = St7.St7CloseFile(uID); 
            return (iErr == St7.ERR7_NoError);
        }
    }
}
