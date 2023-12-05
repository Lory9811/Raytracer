using Raytracer;

using Tuple = Raytracer.Tuple;

World world = new World();

Entity floor = world.CreateEntity("sphere", "floor");
floor.Transform = Matrix.Scale(10, 0.01f, 10);
floor.SurfaceMaterial = new Material {
    color = new Color(1, 0.9f, 0.9f),
    specular = 0
};

Entity leftWall = world.CreateEntity("sphere", "left_wall");
leftWall.Transform = Matrix.Translation(0, 0, 5) *
    Matrix.RotationY(-MathF.PI / 4) * Matrix.RotationX(MathF.PI / 2) *
    Matrix.Scale(10, 0.01f, 10);
leftWall.SurfaceMaterial = floor.SurfaceMaterial;

Entity rightWall = world.CreateEntity("sphere", "right_wall");
rightWall.Transform = Matrix.Translation(0, 0, 5) *
    Matrix.RotationY(MathF.PI / 4) * Matrix.RotationX(MathF.PI / 2) *
    Matrix.Scale(10, 0.01f, 10);
rightWall.SurfaceMaterial = floor.SurfaceMaterial;

Entity middle = world.CreateEntity("sphere", "middle");
middle.Transform = Matrix.Translation(-0.5f, 1, 0.5f);
middle.SurfaceMaterial = new Material {
    color = new Color(0.1f, 1, 0.5f),
    diffuse = 0.7f,
    specular = 0.3f
};

Entity right = world.CreateEntity("sphere", "right");
right.Transform = Matrix.Translation(1.5f, 0.5f, -0.5f) * 
    Matrix.Scale(0.5f, 0.5f, 0.5f);
right.SurfaceMaterial = new Material {
    color = new Color(0.5f, 1, 0.1f),
    diffuse = 0.7f,
    specular = 0.3f
};

Entity left = world.CreateEntity("sphere", "left");
left.Transform = Matrix.Translation(-1.5f, 0.33f, -0.75f) *
    Matrix.Scale(0.33f, 0.33f, 0.33f);
left.SurfaceMaterial = new Material {
    color = new Color(1, 0.8f, 0.1f),
    diffuse = 0.7f,
    specular = 0.3f
};

Camera camera = new Camera(512, 512, MathF.PI / 3);
camera.Transform = Matrix.ViewTransform(
    Tuple.CreatePoint(0, 1.5f, -5),
    Tuple.CreatePoint(0, 1, 0),
    Tuple.CreateVector(0, 1, 0)
);

Canvas image = camera.Render(world);

using (StreamWriter output = new StreamWriter("./image.ppm")) {
    await output.WriteAsync(image.ToPpm());
}
