using Raytracer;

using Tuple = Raytracer.Tuple;

Console.WriteLine("The Ray Tracer Challenge / Chapter 4");
Console.WriteLine("----         Clock Face         ----");

Canvas canvas = new Canvas(64, 64);
Tuple point = Tuple.CreatePoint(0, 0, 28);
Matrix translation = Matrix.Translation(32, 0, 32);
Color color = new Color(1, 1, 1);

for (int i = 0; i < 12; i++) {
    Matrix rotation = Matrix.RotationY(i * MathF.PI / 6.0f);
    Tuple currentPoint = translation * (rotation * point);
    canvas.SetPixel((int)currentPoint.x, 64 - (int)currentPoint.z, color);
}


using (StreamWriter output = new StreamWriter("./clock.ppm")) {
    await output.WriteAsync(canvas.ToPpm());
}
