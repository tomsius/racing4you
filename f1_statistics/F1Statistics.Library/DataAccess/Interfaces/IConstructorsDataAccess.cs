﻿using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.DataAccess.Interfaces
{
    public interface IConstructorsDataAccess
    {
        string GetConstructorByDriver(int year, int round, string leadingDriverId);
    }
}
