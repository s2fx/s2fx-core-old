using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "S2fx.Data.Npgsql",
    Author = "BinaryStar Technologies Yunnan LLC.",
    Website = "http://www.sandwych.com",
    Version = "0.1.0",
    Description = "The support of Npgsql",
    Dependencies = new string[] { "S2fx.Core", "S2fx.Core.EFCore" },
    Category = "Data"
)]