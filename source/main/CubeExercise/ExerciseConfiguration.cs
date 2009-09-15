using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace CubeExercise
{
    public class ExerciseConfiguration : INotifyPropertyChanged
    {
        private ExerciseMode mode;

        private int numberOfAlgorithms;

        private int showScriptDelay = 3;

        private int algorithmTimeLimit = -1;

        public ExerciseMode Mode
        {
            get
            {
                return this.mode;
            }
            set
            {
                if (this.mode == value)
                {
                    return;
                }

                this.mode = value;
                this.Notify("Mode");
            }
        }

        public int NumberOfAlgorithms
        {
            get { return this.numberOfAlgorithms; }
            set
            {
                if (this.numberOfAlgorithms == value)
                {
                    return;
                }

                this.numberOfAlgorithms = value;
                this.Notify("NumberOfAlgorithms");
            }
        }

        public int ShowScriptDelay
        {
            get { return this.showScriptDelay; }
            set
            {
                if (this.showScriptDelay == value)
                {
                    return;
                }

                this.showScriptDelay = value;
                this.Notify("ShowScriptDelay");
            }
        }

        public int AlgorithmTimeLimit
        {
            get { return this.algorithmTimeLimit; }
            set
            {
                if (this.algorithmTimeLimit == value)
                {
                    return;
                }

                this.algorithmTimeLimit = value;
                this.Notify("AlgorithmTimeLimit");
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
