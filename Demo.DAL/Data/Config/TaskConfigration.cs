using Demo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.DAL.Data.Config
{
    public class TaskConfigration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.Property(t => t.Title).IsRequired();
            builder.Property(t => t.DueDate).IsRequired();
            builder.Property(t => t.Status)
                .HasConversion(
                    taskStatus => taskStatus.ToString(),
                    taskStatus => (Entities.TaskStatusEnum)Enum.Parse(typeof(Entities.TaskStatusEnum), taskStatus)
                );
        }
    }
}
