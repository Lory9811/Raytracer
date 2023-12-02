using ProjectileSimulation;
using Raytracer;
using Environment = ProjectileSimulation.Environment;
using Tuple = Raytracer.Tuple;

Console.WriteLine("The Ray Tracer Challenge / Chapter 1");
Console.WriteLine("----   Projectile  simulation   ----");

Environment e = new Environment(
    Tuple.CreateVector(0, -0.1f, 0),
    Tuple.CreateVector(-0.01f, 0, 0)
);

Projectile p = new Projectile(
    Raytracer.Tuple.CreatePoint(0, 1, 0),
    Raytracer.Tuple.CreateVector(1, 1, 0).Normalized()
);

int ticks = 0;
while (p.Position.y > 0) {
    p.Tick(e);
    ticks++;
    Console.WriteLine($"[{ticks}] {p}");
}

Console.WriteLine("The Ray Tracer Challenge / Chapter 2");
Console.WriteLine("----      Projectile  plot      ----");

Tuple start = Tuple.CreatePoint(0, 1, 0);
Tuple velocity = Tuple.CreateVector(1, 1.8f, 0).Normalized() * 11.25f;
p = new Projectile(start, velocity);

Tuple gravity = Tuple.CreateVector(0, -0.1f, 0);
Tuple wind = Tuple.CreateVector(-0.01f, 0, 0);
e = new Environment(gravity, wind);

Canvas canvas = new Canvas(900, 550);

Color color = new Color(1, 0, 0);
canvas.SetPixel((int)start.x, 550 - (int)start.y, color);
while (p.Position.y > 0) {
    p.Tick(e);
    canvas.SetPixel((int)p.Position.x, 550 - (int)p.Position.y, color);
}

using (StreamWriter output = new StreamWriter("./projectile.ppm")) {
    await output.WriteAsync(canvas.ToPpm());
}
