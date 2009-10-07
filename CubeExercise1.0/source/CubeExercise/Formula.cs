using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace CubeExercise
{
    [Serializable]
    public class Formula : INotifyPropertyChanged
    {
        private string name;
        private string image;
        private string script;
        private string preScript;
        private string postScript;
        private string demo;
        private bool enabled;
        private int practiceTimes;

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
