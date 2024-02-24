using Mcare21.Database.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcare21.Database.Configuration
{
    public class Mcare21Configuration: IEntityTypeConfiguration<Domain.InstructionMaster>
    {
        public void Configure(EntityTypeBuilder<Domain.InstructionMaster> builder)
        {
            builder
                .Property(x => x.ID)
                .HasConversion(typeof(McareIdConverter));

            builder
                .Property(x => x.Code)
                .IsRequired()
                .HasConversion(typeof(McareCodeConverter));

            builder
                .Property(x => x.Display)
                .HasMaxLength(512)
                .HasConversion(typeof(DisplayConverter));
        }
    }
}
