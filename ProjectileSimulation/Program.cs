using ProjectileSimulation;
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
