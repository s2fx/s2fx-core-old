using S2fx.Environment.Extensions;

[assembly: S2Module(
    key: "Web",
    Name = "Web",
    Author = "BinaryStar Technologies Yunnan LLC.",
    Website = "http://www.sandwych.com",
    Version = "0.1.0",
    Description = "Web UI",
    Dependencies = new[] { "S2fx.Core", "", "OrchardCore.Mvc" },
    Category = "Web"
)]