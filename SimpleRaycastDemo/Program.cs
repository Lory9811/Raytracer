using Raytracer;

using Tuple = Raytracer.Tuple;

Console.WriteLine("The Ray Tracer Challenge / Chapter 5");
Console.WriteLine("----       Simple raycast       ----");

int canvasSize = 100;
Canvas canvas = new Canvas(canvasSize, canvasSize);
Tuple camera = Tuple.CreatePoint(0, 0, -5);
Color color = new Color(1, 0, 0);

float wallZ = 10.0f;
float wallSize = 7.0f;
float pixelSize = wallSize / canvasSize;
float half = wallSize / 2.0f;

Sphere sphere = new Sphere(Guid.NewGuid());

for (int y = 0; y < canvas.Height; y++) {
    float worldY = half - pixelSize * y;
    for (int x = 0; x < canvas.Width; x++) {
        float worldX = -half + pixelSize * x;
        Tuple targetPoint = Tuple.CreatePoint(worldX, worldY, wallZ);

        Ray ray = new Ray(camera, (targetPoint - camera).Normalized());
        Intersections intersections = new Intersections(sphere.Intersect(ray));
        if (intersections.Hit() is not null) {
            canvas.SetPixel(x, y, color);
        }
    }
}

using (StreamWriter output = new StreamWriter("./image.ppm")) {
    await output.WriteAsync(canvas.ToPpm());
}
