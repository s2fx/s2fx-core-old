using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "S2fx.Web",
    Author = "BinaryStar Technologies Yunnan LLC.",
    Website = "http://www.sandwych.com",
    Version = "0.1.0",
    Description = "Web UI",
    Dependencies = new[] { "S2fx.Core", "S2fx.EFCore" },
    Category = "Web"
)]