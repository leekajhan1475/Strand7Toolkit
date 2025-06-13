using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Runtime;
using Rhino.Geometry;

namespace St7Toolkit.Element
{
    /// <summary>
    /// Class for managing Strand7 support object.
    /// </summary>
    public class St7Support
    {
        private int _id = -1;

        private int _ucsId = -1;

        private int _onNodeId = -1;

        private bool _tX = false; // Set translation x to false by default

        private bool _tY = false; // Set translation y to false by default

        private bool _tZ = false; // Set translation y to false by default

        private bool _rX = false; // Set rotation x to false by default

        private bool _rY = false; // Set rotation x to false by default

        private bool _rZ = false; // Set rotation x to false by default

        /// <summary>
        /// Get or set the support's entity ID, relating to "this" support's Coordinate System
        /// </summary>
        public int EntityId
        {
            get => this._id;
            set => this._id = value;
        }

        #region DegreeOfFreedoms Properties
        #region Translation

        /// <summary>
        /// Gets or sets whether "Translation" about the local axis-system's X-Axis is allowed.
        /// </summary>
        /// <returns>
        /// Type: <see cref="Boolean"/>
        /// </returns>
        public bool TranslationX 
        { 
            get => this._tX;
            set => this._tX = value;
        }

        /// <summary>
        /// Gets or sets whether "Translation" about the local axis-system's Y-Axis is allowed.
        /// </summary>
        /// <returns>
        /// Type: <see cref="Boolean"/>
        /// </returns>
        public bool TranslationY
        {
            get => this._tY;
            set => this._tY = value;
        }

        /// <summary>
        /// Gets or sets whether "Translation" about the local axis-system's Z-Axis is allowed.
        /// </summary>
        /// <returns>
        /// Type: <see cref="Boolean"/>
        /// </returns>
        public bool TranslationZ
        {
            get => this._tZ;
            set => this._tZ = value;
        }

        #endregion
        #region Rotation
        /// <summary>
        /// Gets or sets whether "Rotation" about the local axis-system's X-Axis is allowed.
        /// </summary>
        /// <returns>
        /// Type: <see cref="Boolean"/>
        /// </returns>
        public bool RotationX
        {
            get => this._rX;
            set => this._rX = value;
        }

        /// <summary>
        /// Gets or sets whether "Rotation" about the local axis-system's Y-Axis is allowed.
        /// </summary>
        /// <returns>
        /// Type: <see cref="Boolean"/>
        /// </returns>
        public bool RotationY
        {
            get => this._rY;
            set => this._rY = value;
        }

        /// <summary>
        /// Gets or sets whether "Rotation" about the local axis-system's Z-Axis is allowed.
        /// </summary>
        /// <returns>
        /// Type: <see cref="Boolean"/>
        /// </returns>
        public bool RotationZ
        {
            get => this._rZ;
            set => this._rZ = value;
        }
        #endregion
        #endregion


        /// <summary>
        /// Get or set the entity number of the Node(origin) of the support.
        /// </summary>
        public int OnNodeId
        {
            get => _onNodeId;
            set => _onNodeId = value;
        }

        /// <summary>
        /// Get or set the coordinate system Id of this support.
        /// </summary>
        public int UCSId
        {
            get => _ucsId;
            set => _ucsId = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsValid
        {
            get => (this._id > 0 && this._onNodeId > 0);
        }

        /// <summary>
        /// Gets the value indicating whether this support points to a Node in the St7Model's data structure.
        /// </summary>
        /// <returns>
        /// Type: <see cref="bool"/>
        /// </returns>
        public bool HasNodeId
        {
            get => this._onNodeId > 0;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public St7Support() {}

        /// <summary>
        /// Initializes a support given its entity number and respective Node entity number.
        /// </summary>
        /// <param name="id">
        /// Support's entity number.
        /// </param>
        /// <param name="nodeId">
        /// Node's entity number pointer.<br/>
        /// See: <see cref="HasNodeId"/>
        /// </param>
        public St7Support(int id, int nodeId)
        {
            this._id = id;
            this._onNodeId = nodeId;
        }

        /// <summary>
        /// Locks all translations and rotations of this <see cref="St7Support"/>. 
        /// </summary>
        public void Lock()
        {
            this._tX = false;
            this._tY = false;
            this._tZ = false;
            this._rX = false;
            this._rY = false;
            this._rZ = false;
        }

        /// <summary>
        /// Frees all translations and rotations of this <see cref="St7Support"/>. 
        /// </summary>
        public void Free()
        {
            this._tX = true;
            this._tY = true;
            this._tZ = true;
            this._rX = true;
            this._rY = true;
            this._rZ = true;
        }
    }
}
