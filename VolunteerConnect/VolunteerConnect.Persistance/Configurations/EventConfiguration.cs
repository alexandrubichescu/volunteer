using VolunteerConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VolunteerConnect.Persistance.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            // Configure the Title property
            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            // Configure the relationship between Event and Category
            builder.HasOne<Category>() // An Event belongs to one Category
                .WithMany(c => c.Events) // A Category can have many Events
                .HasForeignKey(e => e.CategoryId) // Specify the foreign key
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete behavior
        }
    }

}
