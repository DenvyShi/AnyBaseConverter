using System;

namespace DETDataGenerator
{
    class OrderInfo
    {
        public string ProductCode { get; set; }
        public string Sku { get; set; }
        public string Style { get; set; }
        public string Size { get; set; }
        public string GO { get; set; }
        public string Color { get; set; }
        public int Qty { get; set; }
    }

    class SkuIndex
    {
        public string Sku { get; set; }
        public ulong StartIndex { get; set; }
        public ulong EndIndex { get; set; }
        public string OrderFilename { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}