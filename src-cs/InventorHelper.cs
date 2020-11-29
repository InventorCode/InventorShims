using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Inventor;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Diagnostics;

namespace InventorShims
{
    public class InventorHelper
    {
        // These are for the running object table and picking the right inv instance if there's more than one
        [DllImport("ole32.dll")] static extern int CreateBindCtx(uint reserved, out IBindCtx ppbc);
        [DllImport("ole32.dll")] static extern void GetRunningObjectTable(int reserved, out IRunningObjectTable prot);

        public Inventor.Application InventorInstance;

        public InventorHelper(bool createNewInstance = false)
        {
            // If the user didn't want to make a new instance
            if (!createNewInstance)
            {
                var numberOfInventorWindowsFound = Process.GetProcessesByName("Inventor").Count();
                
                // If there are no inventor processes yet, then just make a new one regardless of what we were told
                if (numberOfInventorWindowsFound == 0)
                {
                    makeNewInstance();
                }

                // If there is only one process, we don't need to offer the user a pick
                if (numberOfInventorWindowsFound == 1)
                {
                    // If there's only one.
                    try
                    {
                        // Get it the usual way
                        InventorInstance = (Inventor.Application)Marshal.GetActiveObject("Inventor.Application");
                    }
                    catch       // If it fails, make a new one anyways.
                    {
                        makeNewInstance();
                    }
                }

                // If there's more than one then we need to offer a pick with getInventorInstance()
                if (numberOfInventorWindowsFound > 1)
                {
                    // Try to get active instance
                    try
                    {
                        InventorInstance = getInventorInstance();
                    }
                    catch       // If it fails, make a new one anyways.
                    {
                        makeNewInstance();
                    }
                }
            }
            else     // If we were explicitly told to make a new instance.
            {
                makeNewInstance();
            }
        }

        private void makeNewInstance()
        {
            var inventorAppType = System.Type.GetTypeFromProgID("Inventor.Application");
            InventorInstance = (Inventor.Application)System.Activator.CreateInstance(inventorAppType);
            InventorInstance.Visible = true;
            //_InventorInstance.SilentOperation = true; // This may help with broken links messages
        }

        private Inventor.Application getInventorInstance()
        {
            List<Inventor.Application> invInstances;

            invInstances = GetAllInventorApplications();

            //((Inventor.Application)Marshal.GetActiveObject("Inventor.Application"));
            if (invInstances.Count == 1)
            {
                // If we only found one, give it back
                return invInstances[0];
            }
            else
            {
                // We know it's not 0 because GetAllInventorApplications() would have thrown an error

                // Prompt user for which one to use
                FormPickInventorInstances pickFrm = new FormPickInventorInstances(invInstances);

                pickFrm.ShowDialog();

                var instanceUserPicked = pickFrm.SelectedInventorIndex;

                pickFrm.Dispose();

                return invInstances[instanceUserPicked];
            }
        }

        // Get all running instance by querying ROT
        private static List<Inventor.Application> GetAllInventorApplications()
        {
            // get Running Object Table ...
            IRunningObjectTable Rot = null;
            GetRunningObjectTable(0, out Rot);

            if (Rot == null)
            {
                throw new Exception("Could not get a handle to the running object table.");
            }

            // get enumerator for ROT entries
            IEnumMoniker monikerEnumerator = null;
            Rot.EnumRunning(out monikerEnumerator);
            
            if (monikerEnumerator == null)
            {
                throw new Exception("Could not get a handle to monikerEnumerator.");
            }

            monikerEnumerator.Reset();

            IntPtr pNumFetched = new IntPtr();
            IMoniker[] monikers = new IMoniker[1];

            List<Inventor.Application> listOfInvInstances = new List<Inventor.Application>();

            // go through all entries and identifies app instances
            while (monikerEnumerator.Next(1, monikers, pNumFetched) == 0)
            {
                IBindCtx bindCtx = null;
                CreateBindCtx(0, out bindCtx);

                string displayName;
                monikers[0].GetDisplayName(bindCtx, null, out displayName);

                if (displayName.Contains(".ipt") ||
                    displayName.Contains(".iam") ||
                    displayName.Contains(".ipj") ||
                    displayName.Contains(".idw") ||
                    displayName.Contains(".ipn") ||
                    displayName.Contains(".ide"))
                {
                    object ComObject = null;
                    Rot.GetObject(monikers[0], out ComObject);

                    // If the moniker is an inventor doc, add parent to list
                    try
                    {
                        if (ComObject != null)
                        {
                            dynamic invDoc = ComObject;

                            Inventor.Application invApp = (Inventor.Application)invDoc.Parent;

                            listOfInvInstances.Add(invApp);
                        }
                    }
                    catch { }
                }
            }

            // If we didn't get anything even though the user wanted us to throw something so it can be caught further up from here and we can move on
            if (listOfInvInstances.Count == 0)
            {
                throw new Exception("Couldn't find any inventor instances.");
            }

            // Otherwise give back what we got
            return listOfInvInstances;
        }

        public string Project
        {
            get
            {
                var designManager = InventorInstance.DesignProjectManager;

                if (designManager != null)
                {
                    return designManager.ActiveDesignProject.FullFileName;
                }

                return "Error";
            }

            set
            {
                setNewProject(value);
            }
        }

        private void setNewProject(string newProjectFullPath)
        {
            var designManager = InventorInstance.DesignProjectManager;
            DesignProject newProject = null;

            // Is it not current active project
            if (!(newProjectFullPath.ToLower() == designManager.ActiveDesignProject.FullFileName.ToLower()))
            {

                // is it in the list already? if not, create a shortcut to it
                IEnumerable<DesignProject> designProjects = null;
                designProjects = designManager.DesignProjects.Cast<DesignProject>();

                // If there are projects in list of project
                if (designProjects != null)
                {
                    foreach (var project in designProjects)
                    {
                        if (project.FullFileName.ToLower() == newProjectFullPath.ToLower())
                        {
                            // If it's in the list already assign it to newProject
                            newProject = project;
                        }
                    }
                }

                // If it wasn't in the list
                if (newProject == null)
                {
                    newProject = designManager.DesignProjects.AddExisting(newProjectFullPath);
                    
                    // Don't set this cause then it won't use the filename as the name and it WILL use this as the filename even though it's not I hate inventor
                    //newProject.Name = "BYO Desktop Barrier"; 

                    if (InventorInstance.Documents.Count > 0)
                    {
                        throw new Exception("Cannot change project when files are open.");
                    }
                }

                if (newProject != null)
                {
                    newProject.Activate();
                }
                else
                {
                    throw new Exception("Couldn't assign new inventor project file: " + newProjectFullPath);
                }
            }


        }

        public static class Utilities
        {
            public static void ZoomExtents(Document documentToWork)
            {
                documentToWork.Activate();
                ((Inventor.Application)documentToWork.Parent).CommandManager.ControlDefinitions["AppZoomallCmd"].Execute();
            }

        }
    }

    internal class FormPickInventorInstances : Form
    {
        public int SelectedInventorIndex { get => _selectedInventorIndex; }

        private int _selectedInventorIndex;

        private List<Inventor.Application> _invenInstancesList;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeViewInventorInstances = new System.Windows.Forms.TreeView();
            this.buttonSelectInstance = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeViewInventorInstances
            // 
            this.treeViewInventorInstances.Location = new System.Drawing.Point(12, 12);
            this.treeViewInventorInstances.Name = "treeViewInventorInstances";
            this.treeViewInventorInstances.ShowRootLines = false;
            this.treeViewInventorInstances.Size = new System.Drawing.Size(255, 426);
            this.treeViewInventorInstances.TabIndex = 0;
            this.treeViewInventorInstances.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewInventorInstances_AfterSelect);
            // 
            // buttonSelectInstance
            // 
            this.buttonSelectInstance.Location = new System.Drawing.Point(67, 444);
            this.buttonSelectInstance.Name = "buttonSelectInstance";
            this.buttonSelectInstance.Size = new System.Drawing.Size(146, 60);
            this.buttonSelectInstance.TabIndex = 1;
            this.buttonSelectInstance.Text = "Select Inventor Instance";
            this.buttonSelectInstance.UseVisualStyleBackColor = true;
            this.buttonSelectInstance.Click += new System.EventHandler(this.buttonSelectInstance_Click);
            // 
            // FormPickInventorInstances
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 513);
            this.Controls.Add(this.buttonSelectInstance);
            this.Controls.Add(this.treeViewInventorInstances);
            this.Name = "FormPickInventorInstances";
            this.Text = "Inventor Instances";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewInventorInstances;
        private System.Windows.Forms.Button buttonSelectInstance;

        public FormPickInventorInstances(List<Inventor.Application> listToProcess)
        {
            _invenInstancesList = listToProcess;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var offsetIndex = 0;

            for (int i = 0; i < _invenInstancesList.Count; i++)
            {

                treeViewInventorInstances.BeginUpdate();
                treeViewInventorInstances.Nodes.Add("Inventor Instance " + (i + 1).ToString());
                treeViewInventorInstances.Nodes[offsetIndex].Nodes.Add(_invenInstancesList[i].ActiveDocument.DisplayName);

                treeViewInventorInstances.Nodes[offsetIndex].Expand();

                treeViewInventorInstances.Nodes.Add("");

                treeViewInventorInstances.EndUpdate();

                // For the fact that we have blank nodes so we need to skip those
                offsetIndex += 2;
            }
        }

        private void buttonSelectInstance_Click(object sender, EventArgs e)
        {
            // Return to previous task before this form was shown with ShowDialog
            this.DialogResult = DialogResult.OK;
        }

        private void treeViewInventorInstances_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent != null && e.Node.Parent.GetType() == typeof(TreeNode))
            {
                _selectedInventorIndex = e.Node.Parent.Index;

                if (_selectedInventorIndex != 0)
                {
                    _selectedInventorIndex /= 2;
                }
            }
            else
            {
                _selectedInventorIndex = treeViewInventorInstances.SelectedNode.Index;

                if (_selectedInventorIndex != 0)
                {
                    _selectedInventorIndex /= 2;
                }
            }
        }
    }
}
