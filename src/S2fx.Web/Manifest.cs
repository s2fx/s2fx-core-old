using S2fx.Environment.Extensions;

[assembly: S2Module(
    Name = "Web",
    Author = "BinaryStar Technologies Yunnan LLC.",
    Website = "http://www.sandwych.com",
    Version = "0.1.0",
    Description = "Web UI",
    Dependencies = new[] { "Core", "EFCore" },
    Category = "Web"
)]