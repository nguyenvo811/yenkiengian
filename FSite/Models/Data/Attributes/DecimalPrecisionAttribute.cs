using System;
namespace FSite.Models.Data.Attributes
{
    public class DecimalPrecisionAttribute : Attribute
    {
        private byte _precision;
        private byte _scale;
        public byte Precision
        {

            get { return _precision; }
            set { _precision = value; }
        }
        public byte Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }
        public DecimalPrecisionAttribute(byte precision, byte scale)
        {
            this.Precision = precision;
            this.Scale = scale;
        }
    }
}