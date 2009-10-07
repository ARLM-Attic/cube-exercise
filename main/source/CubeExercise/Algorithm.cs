//-----------------------------------------------------------------------------
// <copyright file="Algorithm.cs" company="Rui Fan">
//     Copyright (c) Rui Fan.  All rights reserved.
// </copyright>
//
// <author email="albert@fanrui.net">
//     Rui Fan
// </author>
//
// <summary>
//     This file contains the Algorithm class.
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
// <history date="09/15/2009" Author="Rui Fan">
//     Rename to "Algorithm" class.
// </history>
//-----------------------------------------------------------------------------

namespace CubeExercise
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel;

    public partial class Algorithm : INotifyPropertyChanged
    {
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public int Id
        {
            get { return this.id; }
            set
            {
                if (this.id == value)
                {
                    return;
                }

                this.id = value;
                this.Notify("Id");
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
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

        [System.Xml.Serialization.XmlIgnoreAttribute()]
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

        [System.Xml.Serialization.XmlIgnoreAttribute()]
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

        [System.Xml.Serialization.XmlIgnoreAttribute()]
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

        [System.Xml.Serialization.XmlIgnoreAttribute()]
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

        [System.Xml.Serialization.XmlIgnoreAttribute()]
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

        [System.Xml.Serialization.XmlIgnoreAttribute()]
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
