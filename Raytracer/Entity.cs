using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer {
    public abstract class Entity {
        protected int id;
        protected Matrix transform = Matrix.Eye(4);

        public Matrix Transform {
            get { return transform; }
            set {
                if (value.order != 4) throw new ArgumentException("Cannot use non 4x4 matrix as transform");
                transform = value;
            }
        }

        public int Id { get { return id; } }

        public Entity(int id) {
            this.id = id;
        }
    }
}
