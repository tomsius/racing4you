﻿using F1Statistics.Library.Models;
using System.Collections.Generic;

namespace F1Statistics.Library.Services.Interfaces
{
    public interface IWinsService
    {
        List<WinsModel> AggregateDriversWins(OptionsModel options);
    }
}