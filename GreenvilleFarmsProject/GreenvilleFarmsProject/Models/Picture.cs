namespace GreenvilleFarmsProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Picture
    {
        public int PictureId { get; set; }

        [Required]
        public string PictureName { get; set; }
    }
}
