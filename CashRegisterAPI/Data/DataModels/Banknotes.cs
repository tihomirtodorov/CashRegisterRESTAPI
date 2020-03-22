using System.ComponentModel.DataAnnotations;

namespace CashRegisterAPI.Data.DataModels
{
    public class Banknotes
    {
        [Key]
        public int Id { get; set; }
        public int Fifty { get; set; }
        public int Twenty { get; set; }
        public int Ten { get; set; }
        public int Five { get; set; }
        public int Two { get; set; }
        public int One { get; set; }
    }
}
