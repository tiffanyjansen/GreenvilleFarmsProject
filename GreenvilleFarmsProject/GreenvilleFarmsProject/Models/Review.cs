using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GreenvilleFarmsProject.Models
{
    public class Review
    {
        [Display(Name = "Author Name")]
        public string author_name { get; set; }

        [Display(Name = "Author URL")]
        public string author_url { get; set; }

        [Display(Name = "Profile Photo URL")]
        public string profile_photo_url { get; set; }

        [Display(Name = "Language")]
        public string language { get; set; }

        [Display(Name = "Rating")]
        public int rating { get; set; }

        [Display(Name = "Relative Time Description")]
        public string relative_time_description { get; set; }

        [Display(Name = "Text")]
        public string text { get; set; }

        [Display(Name = "Time")]
        public string time { get; set; }

    }
}