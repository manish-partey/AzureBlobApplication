using System.ComponentModel.DataAnnotations;

namespace AzureBlobApplication.Models
{
    public class Container
    {
        [Required]
        public string Name { get; set; }
    }
}
