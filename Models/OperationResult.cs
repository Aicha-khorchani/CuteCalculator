using System;

namespace CuteCalculator.Models
{
    public class OperationResult
    {
        public string Formula { get; set; }   // e.g., "3.5 × 2"
        public string Result { get; set; }    // e.g., "7"
        public DateTime TimeStamp { get; set; } = DateTime.Now;
    }
}
