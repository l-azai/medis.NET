﻿using System.Web;

namespace medis.Api.ViewModel
{
    public class AddVideoViewModel
    {
        public string VideoFilename { get; set; }
        public string VideoCategoryId { get; set; }
        public int YearReleased { get; set; }
        //public HttpPostedFileBase File { get; set; }
    }
}