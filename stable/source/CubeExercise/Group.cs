//-----------------------------------------------------------------------------
// <copyright file="Group.cs" company="Rui Fan">
//     Copyright (c) Rui Fan.  All rights reserved.
// </copyright>
//
// <author email="albert@fanrui.net">
//     Rui Fan
// </author>
//
// <summary>
//     This file contains the Group class.
// </summary>
//
// <remarks/>
//
// <disclaimer/>
//
// <history date="09/10/2009" Author="Rui Fan">
//     Class Created.
// </history>
//-----------------------------------------------------------------------------

namespace CubeExercise
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel;

    public partial class Group : INotifyPropertyChanged
    {
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
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Expanded
        {
            get { return this.expanded; }
            set
            {
                if (this.expanded == value)
                {
                    return;
                }

                this.expanded = value;
                this.Notify("Expanded");
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
