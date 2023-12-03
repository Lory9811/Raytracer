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

Sphere[] entities = {
     new Sphere(0)
};

entities[0].material = material;
Tuple lightPosition = Tuple.CreatePoint(-10, 10, -10);
Color lightColor = new Color(1, 1, 1);
PointLight light = new PointLight(lightPosition, color);

for (int y = 0; y < canvas.Height; y++) {
    float worldY = half - pixelSize * y;
    for (int x = 0; x < canvas.Width; x++) {
        float worldX = -half + pixelSize * x;
        Tuple targetPoint = Tuple.CreatePoint(worldX, worldY, wallZ);

        Ray ray = new Ray(camera, (targetPoint - camera).Normalized());
        Intersections intersections = new Intersections(entities[0].Intersect(ray));
        Intersection? hit = intersections.Hit();
        if (hit != null) {
            Tuple point = ray.Position(hit.t);
            Tuple normal = entities[hit.entity].NormalAt(point);
            Tuple eye = -ray.Direction;
            canvas.SetPixel(x, y, entities[hit.entity].material.Lighting(light, point, eye, normal));
        }
    }
}

using (StreamWriter output = new StreamWriter("./image.ppm")) {
    await output.WriteAsync(canvas.ToPpm());
}
