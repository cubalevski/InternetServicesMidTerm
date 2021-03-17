using System;
using Microsoft.EntityFrameworkCore;


public class Class1
{
	public Class1()
	{
        public Class1(DbContextOptions<Class1> options)
        : base(options)
        {
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<SurveyUser> SurveyUsers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answers>(answer =>
        {
            answer.Property(p => p.UserId).IsRequired();
            answer.Property(p => p.Id).IsRequired();
            answer.HasKey(p => p.UserId);
            answer.Property(p => p.OptionId).IsRequired();
            answer.HasKey(p => p.OptionId);

            answer.HasOne(pm => pm.Option)
                    .WithMany(p => p.Answer)
                    .HasForeignKey(pm => pm.OptionsId);

            answer.HasOne(pm => pm.SurveyUser)
                     .WithMany(p => p.Answers)
                     .HasForeignKey(pm => pm.UserId);

            answer.ToTable("Answer");
        });

        modelBuilder.Entity<Option>(option =>
        {
            option.Property(p => p.Id).IsRequired();
            option.Property(p => p.Text).HasMaxLength(800).IsRequired();
            option.Property(p => p.Order).IsRequired();
            option.Property(p => p.QuestionId).IsRequired();
            option.HasKey(p => p.QuestionId);
            option.ToTable("Option");
        });

        modelBuilder.Entity<Question>(question =>
        {
            question.Property(p => p.Id).IsRequired();
            question.Property(p => p.Text).HasMaxLength(800).IsRequired();
            question.Property(p => p.Description).IsRequired();
            question.HasMany(p => p.Options);
            question.ToTable("Question");
        });

        modelBuilder.Entity<SurveyUser>(user =>
        {
            user.Property(p => p.FirstName).IsRequired();
            user.Property(p => p.LastName).IsRequired();
            user.Property(p => p.Id).IsRequired();
            user.Property(p => p.DoB).IsRequired();
            user.Property(p => p.Gender).IsRequired();
            user.Property(p => p.Country).IsRequired();
            user.ToTable("SurveyUser");
        });
}
}
