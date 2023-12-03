using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer {
    public class PointLight {
        private Tuple position;
        private Color intensity;

        public Tuple Position { get { return position; } }
        public Color Intensity { get { return intensity; } }

        public PointLight(Tuple position, Color intensity) { 
            this.position = position;
            this.intensity = intensity;
        }
    }
}
