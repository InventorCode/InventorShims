﻿using Inventor;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace InventorShims
{
    /// <summary>
    /// A class that allows for simpler manipulation of the iLogic Addin's list of External Rule Directories.
    /// </summary>
    public class ExternalRuleDirectories : IDisposable
    {
        private bool disposedValue;
        private Inventor.Application _app;
        private dynamic iLogicAddIn { get; set; }
        private dynamic iLogicAuto { get; set; }

        public ExternalRuleDirectories()
        {
            _app = ApplicationShim.Instance();
            Initialize(_app);
        }

        public ExternalRuleDirectories(Inventor.Application local_app)
        {
            _app = local_app;
            Initialize(_app);
        }

        private void Initialize(Inventor.Application _app)
        {
            ApplicationAddIns appAddIns = _app.ApplicationAddIns;
            ApplicationAddIn iLogicAddIn = appAddIns.ItemById["{3bdd8d79-2179-4b11-8a5a-257b1c0263ac}"];

            if (iLogicAddIn == null)
                throw new SystemException("The iLogic add-in could not be found by ID {3bdd8d79-2179-4b11-8a5a-257b1c0263ac}.");

            try
            {
                iLogicAddIn.Activate();
                iLogicAuto = iLogicAddIn.Automation;
            }
            catch (Exception e)
            {
                throw new SystemException("The iLogic add-in could not be accessed.", e);
            }
        }

        private List<string> _directories = new List<string> { };
        public List<string> Directories
        {
            get
            {
                string[] dirs = iLogicAuto.FileOptions.ExternalRuleDirectories();
                List<string> dirList = new List<string> { };
                foreach (string i in dirs)
                {
                    dirList.Add(i);
                }
                return dirList;
            }
            set
            {
                iLogicAuto.FileOptions.ExternalRuleDirectories = value.ToArray();
            }
        }

        /// <summary>
        /// Adds a directory to the iLogic Addin's list of External Rule Directories.  Does not
        /// add the directory if it already exists in the list.
        /// </summary>
        /// <param name="directory">Directory to add</param>
        public void Add(string directory)
        {
            if (!Directories.Contains(directory))
            {
                var _directories = Directories;
                _directories.Add(directory);
                Directories = _directories;
            }

                Directories.Add(directory);
        }

        /// <summary>
        /// Removes a directory from the iLogic Addin's list of External Rule Directories.
        /// </summary>
        /// <param name="directory">Directory to remove</param>
        public void Remove(string directory)
        {
            Directories.Remove(directory);
        }

        /// <summary>
        /// Determines whether a directory is in the iLogic Addin's list of External Rule Directories.
        /// </summary>
        /// <param name="directory"></param>
        /// <returns>true if directory is found in the list.</returns>
        public bool Contains(string directory)
        {
            return Directories.Contains(directory);
        }

        /// <summary>
        /// Clears all entries from the iLogic Addin's list of External Rule Directories.
        /// </summary>
        public void Clear()
        {
            string[] temp = { };
            iLogicAuto.FileOptions.ExternalRuleDirectories = temp;
        }

        /// <summary>
        /// Get the number of directories contained in ExternalRuleDirectories.
        /// </summary>
        public int Count { get { return Directories.Count; } }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    iLogicAddIn = null;
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                Marshal.ReleaseComObject(iLogicAuto);

                Marshal.ReleaseComObject(_app);

                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~ExternalRuleDirectories()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}