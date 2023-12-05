using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer {
    public class World {
        private Dictionary<Guid, Entity> entities = new Dictionary<Guid, Entity>();
        private Dictionary<string, Type> shapes = new Dictionary<string, Type>();
        private PointLight light = new PointLight(Tuple.CreatePoint(-10, -10, -10), new Color(1, 1, 1));

        public World() {
            AddShape("sphere", typeof(Sphere));
        }

        public PointLight Light { get => light; set => light = value; }

        public void AddShape(string name, Type type) {
            if (!type.IsSubclassOf(typeof(Entity)))
                throw new ArgumentException("Trying to register a new shape that is not an Entity");

            shapes[name] = type;
        }

        public Entity CreateEntity(string shape, string? name = null) {
            if (!shapes.ContainsKey(shape))
                throw new ArgumentException($"Shape \"{shape}\" is not registered");

            Guid id = Guid.NewGuid();


            List<object> arguments = new List<object> { id };
            if (name is not null) {
                arguments.Add(name);
            }

            Entity? entity = (Entity?)Activator.CreateInstance(shapes[shape], BindingFlags.OptionalParamBinding, null, arguments.ToArray(), null);

            if (entity is null)
                throw new ArgumentNullException("Entity cannot be null");

            entities[id] = entity;

            return entity;
        }

        public Entity? GetEntity(Guid guid) {
            if (!entities.ContainsKey(guid)) return null;

            return entities[guid];
        }

        public Entity[] FindEntitiesByName(string name) { 
            return entities.Values.Where(x => x.Name == name).ToArray();
        }

        public Intersections Intersect(Ray ray) {
            List<Intersection> intersections = new List<Intersection>();
            foreach (KeyValuePair<Guid, Entity> entity in entities) {
                intersections.AddRange(entity.Value.Intersect(ray));
            }
            return new Intersections(intersections.ToArray());
        }

        public Color ShadeHit(Intersection.HitData hitData) {
            return hitData.entity.SurfaceMaterial.Lighting(light, hitData.point,
                hitData.eye, hitData.normal);
        }

        public Color ColorAt(Ray ray) {
            Intersections intersections = Intersect(ray);
            Intersection? hit = intersections.Hit();

            if (hit is null) return new Color(0, 0, 0);

            return ShadeHit(hit.ComputeHitData(ray));
        }
    }
}
