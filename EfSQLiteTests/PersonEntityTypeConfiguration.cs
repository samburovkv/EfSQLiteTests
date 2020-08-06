using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EfSQLiteTests
{
    public class PersonEntityTypeConfiguration : IEntityTypeConfiguration<Person>
    {
        internal static readonly ValueConverter<List<int>, string> Converter
            = new ValueConverter<List<int>, string>(
                v => JsonSerializer.Serialize(v, null),
                v => JsonSerializer.Deserialize<List<int>>(v, null));

        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(x => x.Id);
            builder
                .Property(x => x.Numbers)
                .HasDefaultValue(new List<int>())
                .IsRequired()
                .HasConversion(Converter)
                .Metadata.SetValueComparer(new ValueComparer<List<int>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()));
        }
    }
}
