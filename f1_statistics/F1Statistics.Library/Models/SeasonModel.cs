﻿using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class SeasonModel
    {
        public int Year { get; set; }
        public List<RaceModel> Races { get; set; }
    }
}
