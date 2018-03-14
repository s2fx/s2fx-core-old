using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "S2fx.Core",
    Author = "BinaryStar Technologies Yunnan LLC.",
    Website = "http://www.sandwych.com",
    Version = "0.1.0",
    Description = "Core module",
    Dependencies = new string[] { "S2fx.EFCore" },
    Category = "Core"
)]