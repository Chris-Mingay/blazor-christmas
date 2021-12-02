using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            //builder.Ignore(e => e.DomainEvents);

            builder.Property(t => t.Text)
                .IsRequired();
            
            builder.Property(t => t.Category)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
