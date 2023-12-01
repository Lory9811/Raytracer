using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Raytracer;
using Tuple = Raytracer.Tuple;

namespace ProjectileSimulation {
    internal class Projectile {
        private Tuple position;
        private Tuple velocity;

        public Projectile(Tuple position, Tuple velocity) {
            this.position = position;
            this.velocity = velocity;
        }

        public Tuple Position { get => position; set => position = value; }
        public Tuple Velocity { get => velocity; set => velocity = value; }

        public void Tick(Environment environment) {
            position += velocity;
            velocity += environment.Gravity + environment.Wind;
        }

        public override string ToString() {
            return $"{{position={position},velocity={velocity}}}";
        }
    }
}
