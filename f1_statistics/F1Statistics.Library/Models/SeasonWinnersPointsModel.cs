﻿using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class SeasonWinnersPointsModel
    {
        public int Year { get; set; }
        public string Winner { get; set; }
        public int Points { get; set; }
        public int RacesCount { get; set; }
    }
}
