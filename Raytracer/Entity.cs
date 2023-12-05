using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer {
    public abstract class Entity {
        protected Guid id;
        protected string name;
        protected Matrix transform = Matrix.Eye(4);
        protected Material material = new Material();

        public Matrix Transform {
            get { return transform; }
            set {
                if (value.order != 4) throw new ArgumentException("Cannot use non 4x4 matrix as transform");
                transform = value;
            }
        }

        public Material SurfaceMaterial { 
            get { return material; }
            set { material = value; }
        }

        public Guid Id { get { return id; } }
        public string Name { 
            get { return name; } 
            set { name = value; }
        }

        internal Entity(Guid id, string name = "unnamed_entity") {
            this.id = id;
            this.name = name;
        }

        public abstract Intersection[] Intersect(Ray ray);
        public abstract Tuple NormalAt(Tuple point);
    }
}
