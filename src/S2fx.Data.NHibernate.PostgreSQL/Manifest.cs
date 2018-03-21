using S2fx.Environment.Extensions;

[assembly: S2Module(
    key: "NHibernateNpgsql",
    Name = " The NHibernate NPgSQL support for Slipstream",
    Author = "BinaryStar Technologies Yunnan LLC.",
    Website = "http://www.sandwych.com",
    Version = "0.1.0",
    Description = "The support of NHibernate with Npgsql Driver",
    Dependencies = new string[] { "S2fx.Data.NHibernate" },
    Category = "Data"
)]