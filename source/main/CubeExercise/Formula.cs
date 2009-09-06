//-----------------------------------------------------------------------------
// <copyright file="Formula.cs" company="Rui Fan">
//     Copyright (c) Rui Fan.  All rights reserved.
// </copyright>
//
// <author email="albert@fanrui.net">
//     Rui Fan
// </author>
//
// <summary>
//     This file contains the Formula class.
// </summary>
//
// <remarks/>
//
// <disclaimer/>
//
// <history date="08/01/2009" Author="Rui Fan">
//     Class Created.
// </history>
// <history date="09/06/2009" Author="Rui Fan">
//     Add formulas grouping feature.
// </history>
//-----------------------------------------------------------------------------

namespace CubeExercise
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel;

    [Serializable]
    public class Formula : INotifyPropertyChanged
    {
        private string name;
        private string image;
        private string script;
        private string preScript;
        private string postScript;
        private string demo;
        private bool enabled = true;
        private int practiceTimes;

        private List<Formula> subNodes;

        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name == value)
                {
                    return;
                }

                this.name = value;
                this.Notify("Name");
            }
        }

        public string Image
        {
            get { return this.image; }
            set
            {
                if (this.image == value)
                {
                    return;
                }

                this.image = value;
                this.Notify("Image");
            }
        }

        public string Script
        {
            get { return this.script; }
            set
            {
                if (this.script == value)
                {
                    return;
                }

                this.script = value;
                this.Notify("Script");
            }
        }

        public string PreScript
        {
            get { return this.preScript; }
            set
            {
                if (this.preScript == value)
                {
                    return;
                }

                this.preScript = value;
                this.Notify("PreScript");
            }
        }

        public string PostScript
        {
            get { return this.postScript; }
            set
            {
                if (this.postScript == value)
                {
                    return;
                }

                this.postScript = value;
                this.Notify("PostScript");
            }
        }

        public string Demo
        {
            get { return this.demo; }
            set
            {
                if (this.demo == value)
                {
                    return;
                }

                this.demo = value;
                this.Notify("Demo");
            }
        }

        public bool Enabled
        {
            get { return this.enabled; }
            set
            {
                if (this.enabled == value)
                {
                    return;
                }

                this.enabled = value;
                this.Notify("Enabled");

                if (this.SubNodes != null)
                {
                    // Set "Enabled" property of all the sub nodes. This is a recursive call.
                    foreach (var f in this.SubNodes)
                    {
                        f.Enabled = value;
                    }
                }
            }
        }

        public int PracticeTimes
        {
            get { return this.practiceTimes; }
            set
            {
                if (this.practiceTimes == value)
                {
                    return;
                }

                this.practiceTimes = value;
                this.Notify("PracticeTimes");
            }
        }

        public List<Formula> SubNodes
        {
            get { return this.subNodes; }
            set { this.subNodes = value; }
        }

        #region INotifyPropertyChanged Members

        //[NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        protected void Notify(string propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
