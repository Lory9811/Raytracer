using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Raytracer;
using Tuple = Raytracer.Tuple;

namespace ProjectileSimulation {
    class Environment {
        private Tuple gravity;
        private Tuple wind;

        public Environment(Tuple gravity, Tuple wind) {
            this.gravity = gravity;
            this.wind = wind;
        }

        public Tuple Gravity { get => gravity; set => gravity = value; }
        public Tuple Wind { get => wind; set => wind = value; }
    }
}
