using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace CubeExercise
{
    public partial class AlgorithmReference : INotifyPropertyChanged
    {
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public Algorithm Algorithm;

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public int Id
        {
            get { return this.id; }
            //set
            //{
            //    if (this.id == value)
            //    {
            //        return;
            //    }

            //    this.id = value;
            //    this.Notify("Id");
            //}
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(this.name) && this.Algorithm != null)
                {
                    return this.Algorithm.Name;
                }

                return this.name;
            }

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
        public string Demo
        {
            get
            {
                if (string.IsNullOrEmpty(this.demo) && this.Algorithm != null)
                {
                    return this.Algorithm.Demo;
                }

                return this.demo;
            }

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

        #region INotifyPropertyChanged Members

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
