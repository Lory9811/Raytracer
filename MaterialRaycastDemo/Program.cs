using Raytracer;

using Tuple = Raytracer.Tuple;

Console.WriteLine("The Ray Tracer Challenge / Chapter 6");
Console.WriteLine("----       Simple raycast       ----");

int canvasSize = 100;
Canvas canvas = new Canvas(canvasSize, canvasSize);
Tuple camera = Tuple.CreatePoint(0, 0, -5);
Color color = new Color(1, 0, 0);

float wallZ = 10.0f;
float wallSize = 7.0f;
float pixelSize = wallSize / canvasSize;
float half = wallSize / 2.0f;

Material material = new Material();
material.color = new Color(1, 0.2f, 1);


Dictionary<Guid, Entity> entities = new Dictionary<Guid, Entity>();
Sphere sphere = new Sphere(Guid.NewGuid());

entities[sphere.Id] = sphere;
entities[sphere.Id].SurfaceMaterial = material;
Tuple lightPosition = Tuple.CreatePoint(-10, 10, -10);
Color lightColor = new Color(1, 1, 1);
PointLight light = new PointLight(lightPosition, color);

for (int y = 0; y < canvas.Height; y++) {
    float worldY = half - pixelSize * y;
    for (int x = 0; x < canvas.Width; x++) {
        float worldX = -half + pixelSize * x;
        Tuple targetPoint = Tuple.CreatePoint(worldX, worldY, wallZ);

        Ray ray = new Ray(camera, (targetPoint - camera).Normalized());

        Intersections intersections = new Intersections(entities[sphere.Id].Intersect(ray));
        Intersection? hit = intersections.Hit();
        if (hit is not null) {
            Tuple point = ray.Position(hit.t);
            Tuple normal = hit.entity.NormalAt(point);
            Tuple eye = -ray.Direction;
            canvas.SetPixel(x, y, hit.entity.SurfaceMaterial.Lighting(light, point, eye, normal));
        }
    }
}

using (StreamWriter output = new StreamWriter("./image.ppm")) {
    await output.WriteAsync(canvas.ToPpm());
}
