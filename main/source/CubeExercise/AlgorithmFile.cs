using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CubeExercise
{
    public partial class AlgorithmFile
    {
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool LoadOnStart
        {
            get { return this.loadOnStart; }
            set { this.loadOnStart = value; }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public string Path
        {
            get;
            set;
        }
    }
}
