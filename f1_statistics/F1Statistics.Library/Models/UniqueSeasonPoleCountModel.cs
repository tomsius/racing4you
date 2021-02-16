﻿using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class UniqueSeasonPoleCountModel
    {
        public int Season { get; set; }
        public List<string> PoleSitters { get; set; }
        public int UniquePoleSittersCount
        {
            get
            {
                return PoleSitters.Count;
            }
        }
    }
}
