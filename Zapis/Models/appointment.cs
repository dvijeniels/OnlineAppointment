namespace Zapis.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("appointment")]
    public class appointment
    {
        public int AppointmentId { get; set; }

        [DisplayName("Услуга")]
        public int CategoryId { get; set; }

        [StringLength(500)]
        [Required(ErrorMessage = "Пожалуйста введите ФИО (фамилия, имя, отчество)")]
        [DisplayName("ФИО")]
        public string? NameSurname { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Пожалуйста введите номер контактного телефона")]
        [DisplayName("Номер телефон")]
        public string? Telefon { get; set; }

        [StringLength(25)]
        [Required(ErrorMessage = "Пожалуйста выберите дату")]
        [DisplayName("Дата")]
        public string? DateTime { get; set; }

        [DisplayName("IP")]
        public string? IpAddress { get; set; }

        [DisplayName("Статус")]
        public Int16 Status { get; set; }
        public virtual category? category { get; set; }
    }
}