namespace TNAI.Model.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TNAI.Model.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TNAI.Model.AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            if(!context.Categories.Any())
            {
                context.Categories.Add(new Entities.Category()
                {
                    Id = 1,
                    Name = "Sportowe"
                });

                context.Categories.Add(new Entities.Category()
                {
                    Id = 2,
                    Name = "Zimowe"
                });

                context.Categories.Add(new Entities.Category()
                {
                    Id = 3,
                    Name = "Kalosze"
                });

                if (!context.Products.Any())
                {
                    context.Products.Add(new Entities.Product()
                    {
                        Name = "Jordany",
                        Price = 1000,
                        CategoryId = 1
                    });

                    context.Products.Add(new Entities.Product()
                    {
                        Name = "Kozaki",
                        Price = 250,
                        CategoryId = 2
                    });

                    context.Products.Add(new Entities.Product()
                    {
                        Name = "Kalosze rybaka",
                        Price = 50,
                        CategoryId = 3
                    });
                }

                context.SaveChanges();
            }
        }
    }
}
