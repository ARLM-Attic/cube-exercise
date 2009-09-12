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

        private int numberOfFormulas;

        private int showScriptDelay = 3;

        private int formulaTimeLimit = -1;

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

        public int NumberOfFormulas
        {
            get { return this.numberOfFormulas; }
            set
            {
                if (this.numberOfFormulas == value)
                {
                    return;
                }

                this.numberOfFormulas = value;
                this.Notify("NumberOfFormulas");
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

        public int FormulaTimeLimit
        {
            get { return this.formulaTimeLimit; }
            set
            {
                if (this.formulaTimeLimit == value)
                {
                    return;
                }

                this.formulaTimeLimit = value;
                this.Notify("FormulaTimeLimit");
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
