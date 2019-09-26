using System.ComponentModel.DataAnnotations;

namespace GreenvilleFarmsProject.Models
{
    public partial class Picture
    {
        public int PictureId { get; set; }

        [Required]
        public string PictureName { get; set; }
    }
}
