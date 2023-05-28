namespace Zapis.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("category")]
    public class category
    {
        public int CategoryId { get; set; }

        [StringLength(2000)]
        [Required(ErrorMessage = "Пожалуйста введите услугу")]
        [DisplayName("Услуга")]
        public string? CategoryName { get; set; }

        [DisplayName("IP")]
        public string? IpAddress { get; set; }

        [DisplayName("Статус")]
        public bool Status { get; set; }
    }
}
