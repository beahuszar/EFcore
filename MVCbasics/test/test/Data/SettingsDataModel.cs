using System.ComponentModel.DataAnnotations;

namespace Fasetto.Word.Web.Server.Data
{
    public class SettingsDataModel
    {
        [Key]
        public string Id { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        [Required]
        [MaxLength(2048)]
        public string Value { set; get; }
    }
}
