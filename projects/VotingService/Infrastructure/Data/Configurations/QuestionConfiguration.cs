using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Entities;

namespace VotingService.Infrastructure.Data.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Questions");

        builder.HasKey(q => q.Id);
        builder.Property(q => q.Id).ValueGeneratedOnAdd();

        builder.Property(q => q.Text)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasMany(q => q.Options)
            .WithOne(o => o.Question)
            .HasForeignKey(o => o.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
