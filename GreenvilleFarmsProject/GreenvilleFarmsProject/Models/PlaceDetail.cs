using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenvilleFarmsProject.Models
{
    public class PlaceDetail
    {
        public string[] html_attributions { get; set; }
        public Result result { get; set; }
        public string status { get; set; }
    }

    public class Result
    {
        public Review[] reviews { get; set; }
    }
}