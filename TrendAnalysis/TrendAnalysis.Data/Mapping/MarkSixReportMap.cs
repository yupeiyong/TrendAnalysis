using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendAnalysis.Models;

namespace TrendAnalysis.Data.Mapping
{
    public class MarkSixReportMap:EntityTypeConfiguration<MarkSixReport>
    {
        public MarkSixReportMap()
        {
            Property(p => p.Times).HasMaxLength(7).IsRequired().HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_MarkSixReportTimes") { IsUnique = true }));
        }
    }
}
