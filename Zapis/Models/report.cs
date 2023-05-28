namespace Zapis.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("report")]
    public class report
    {
        public int ReportId { get; set; }

        [DisplayName("ФИО")]
        public string? Name { get; set; }

        [DisplayName("Телефон")]
        public string? Telefon { get; set; }
    }
}