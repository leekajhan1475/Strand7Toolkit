using System;
using System.Collections.Generic;
using System.Text;
using St7Toolkit.Element;
using St7API;
using Rhino.Geometry;
using Rhino.Geometry.Collections;
using System.IO;

namespace St7Toolkit
{
    public partial class St7Model
    {      
        /// <summary>
        /// Close Strand7 model file
        /// </summary>
        /// <returns>
        /// Boolean flag indicating whether operation completed successfully or not.
        /// </returns>
        public bool CloseFile()
        {
            return (FileIO.CloseFile(this.UId));
        }

        /// <summary>
        /// Create a new Strand7 model file
        /// </summary>
        /// <param name="fileDir"></param>
        /// <param name="scratch"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool NewFile(string fileDir)
        {
            // Try creating a Strand7 model file
            if (!FileIO.NewFile(this.UId, fileDir, FileIO.ScratchPath)) { return false; }
            this.FilePath = fileDir;
            return true;
        }

        /// <summary>
        /// Open a Strand7 model
        /// </summary>
        /// <param name="errorMessage">
        /// Error message for handling
        /// </param>
        /// <returns>
        /// </returns>
        public bool OpenFile()
        {
            // Abort if the model has no associate file name
            if (this.FilePath == string.Empty) { return false; }
            // Try openning Strand7 model file
            return (FileIO.OpenFile(this.UId, this.FilePath, FileIO.ScratchPath));
        }


        public bool ReadFile()
        {
            // Abort if fail to open this model
            if(!this.OpenFile()) {  return false; }
            // Pull Node data from Strand7 file
            if (!this.PullNodeFromStrand7()) { return false; }
            

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="closeAfterSave"></param>
        /// <returns></returns>
        public bool SaveFile(bool closeAfterSave)
        {
            return (FileIO.SaveFile(this.UId, closeAfterSave));
        }
    }
}
