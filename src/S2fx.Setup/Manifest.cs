using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "Slipstream Setup Module",
    Author = "BinaryStar Technologies Yunnan LLC.",
    Website = "http://www.sandwych.com",
    Version = "0.1.0",
    Description = "Setup module",
    Dependencies = new string[] { "S2fx.AdminUI" },
    Category = "Core"
)]