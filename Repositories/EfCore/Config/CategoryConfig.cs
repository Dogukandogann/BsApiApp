using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EfCore.Config
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.CategoryId);
            builder.Property(c => c.CategoryName).IsRequired();
            builder.HasData(
            new Category()
            {
                 CategoryName = "Computer Science",
                 CategoryId = 1
            },
            new Category()
            {
                CategoryName = "Network",
                CategoryId = 2
            },
            new Category()
            {
                CategoryName = "Db Management Systems",
                CategoryId = 3
            });
        }
    }
}
