using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace FSite.Models.Data.Attributes
{
    public class DecimalPrecisionAttributeConvention : PrimitivePropertyAttributeConfigurationConvention<DecimalPrecisionAttribute>
    {
        public override void Apply(ConventionPrimitivePropertyConfiguration configuration, DecimalPrecisionAttribute attribute)
        {
            configuration.HasPrecision(attribute.Precision, attribute.Scale);
        }
    }
}