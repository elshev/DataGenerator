using System;
using System.ComponentModel.DataAnnotations;

namespace APaers.DataGen.Log
{
    public class LogMessage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public byte Type { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Message { get; set; }
    }
}