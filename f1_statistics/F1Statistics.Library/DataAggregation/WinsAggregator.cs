﻿using F1Statistics.Library.DataAccess.Interfaces;
using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Helpers.Interfaces;
using F1Statistics.Library.Models;
using F1Statistics.Library.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Statistics.Library.DataAggregation
{
    public class WinsAggregator : IWinsAggregator
    {
        private readonly IResultsDataAccess _resultsDataAccess;
        private readonly INameHelper _nameHelper;
        private readonly ITimeHelper _timeHelper;

        public WinsAggregator(IResultsDataAccess resultsDataAccess, INameHelper nameHelper, ITimeHelper timeHelper)
        {
            _resultsDataAccess = resultsDataAccess;
            _nameHelper = nameHelper;
            _timeHelper = timeHelper;
        }

        public List<WinsModel> GetDriversWins(int from, int to)
        {
            var driversWins = new List<WinsModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year => 
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                foreach (var race in races)
                {
                    var winner = _nameHelper.GetDriverName(race.Results[0].Driver);
                    var circuitName = race.Circuit.circuitName;
                    var time = race.Results[1].Time != null ? race.Results[1].Time.time : "0";
                    var gapToSecond = _timeHelper.ConvertGapFromStringToDouble(time);
                    var gridPosition = int.Parse(race.Results[0].grid);
                    var newWinInformationModel = new WinInformationModel { CircuitName = circuitName, GapToSecond = gapToSecond, GridPosition = gridPosition };

                    lock (lockObject) 
                    {
                        var newWinsByYearModel = new WinsByYearModel { Year = year, WinInformation = new List<WinInformationModel> { newWinInformationModel } };

                        if (!driversWins.Where(driver => driver.Name == winner).Any())
                        {
                            var newWinsModel = new WinsModel { Name = winner, WinsByYear = new List<WinsByYearModel> { newWinsByYearModel } };
                            driversWins.Add(newWinsModel);
                        }
                        else
                        {
                            var driver = driversWins.Where(driver => driver.Name == winner).First();

                            if (!driver.WinsByYear.Where(model => model.Year == year).Any())
                            {
                                driver.WinsByYear.Add(newWinsByYearModel);
                            }
                            else
                            {
                                driver.WinsByYear.Where(model => model.Year == year).First().WinInformation.Add(newWinInformationModel);
                            }
                        }
                    }
                }
            });

            return driversWins;
        }

        public List<WinsModel> GetConstructorsWins(int from, int to)
        {
            var constructorsWins = new List<WinsModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year => 
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                foreach (var race in races)
                {
                    var winner = _nameHelper.GetConstructorName(race.Results[0].Constructor);
                    var circuitName = race.Circuit.circuitName;
                    var time = race.Results[1].Time != null ? race.Results[1].Time.time : "0";
                    var gapToSecond = _timeHelper.ConvertGapFromStringToDouble(race.Results[1].Time.time);
                    var gridPosition = int.Parse(race.Results[0].grid);
                    var newWinInformationModel = new WinInformationModel { CircuitName = circuitName, GapToSecond = gapToSecond, GridPosition = gridPosition };

                    lock (lockObject)
                    {
                        var newWinsByYearModel = new WinsByYearModel { Year = year, WinInformation = new List<WinInformationModel> { newWinInformationModel } };

                        if (!constructorsWins.Where(constructor => constructor.Name == winner).Any())
                        {
                            var newWinsModel = new WinsModel { Name = winner, WinsByYear = new List<WinsByYearModel> { newWinsByYearModel } };
                            constructorsWins.Add(newWinsModel);
                        }
                        else
                        {
                            var constructor = constructorsWins.Where(constructor => constructor.Name == winner).First();

                            if (!constructor.WinsByYear.Where(model => model.Year == year).Any())
                            {
                                constructor.WinsByYear.Add(newWinsByYearModel);
                            }
                            else
                            {
                                constructor.WinsByYear.Where(model => model.Year == year).First().WinInformation.Add(newWinInformationModel);
                            }
                        }
                    }
                }
            });

            return constructorsWins;
        }

        public List<AverageWinsModel> GetDriversWinPercent(int from, int to)
        {
            var driversAverageWins = new List<AverageWinsModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                foreach (var race in races)
                {

                    lock (lockObject)
                    {
                        foreach (var result in race.Results)
                        {
                            var driverName = _nameHelper.GetDriverName(result.Driver);

                            if (!driversAverageWins.Where(driver => driver.Name == driverName).Any())
                            {
                                var newAverageWinsModel = new AverageWinsModel { Name = driverName, ParticipationCount = 1 };
                                driversAverageWins.Add(newAverageWinsModel);
                            }
                            else
                            {
                                driversAverageWins.Where(driver => driver.Name == driverName).First().ParticipationCount++;
                            }
                        }

                        var winner = $"{race.Results[0].Driver.givenName} {race.Results[0].Driver.familyName}";

                        driversAverageWins.Where(driver => driver.Name == winner).First().WinCount++;  
                    }
                }
            });

            return driversAverageWins;
        }

        public List<AverageWinsModel> GetConstructorsWinPercent(int from, int to)
        {
            var constructorsAverageWins = new List<AverageWinsModel>(to - from + 1);
            var lockObject = new object();
            var lockIncrement = new object();

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                foreach (var race in races)
                {
                    lock (lockObject)
                    {
                        foreach (var result in race.Results)
                        {
                            var constructorName = _nameHelper.GetConstructorName(result.Constructor);

                            if (!constructorsAverageWins.Where(constructor => constructor.Name == constructorName).Any())
                            {
                                var newAverageWinsModel = new AverageWinsModel { Name = constructorName, ParticipationCount = 1 };
                                constructorsAverageWins.Add(newAverageWinsModel);
                            }
                            else
                            {
                                constructorsAverageWins.Where(constructor => constructor.Name == constructorName).First().ParticipationCount++;
                            }
                        }

                        RemoveDoubleCarCountingInARace(constructorsAverageWins, race);  
                    }

                    lock (lockIncrement)
                    {
                        var winner = $"{race.Results[0].Constructor.name}";

                        constructorsAverageWins.Where(driver => driver.Name == winner).First().WinCount++; 
                    }
                }
            });

            return constructorsAverageWins;
        }

        private void RemoveDoubleCarCountingInARace(List<AverageWinsModel> constructorsAverageWins, RacesDataResponse race)
        {
            foreach (var constructor in constructorsAverageWins)
            {
                if (AreTwoConstructorsCarsInARace(race, constructor.Name) == 2)
                {
                    constructor.ParticipationCount--;
                }
            }
        }

        private int AreTwoConstructorsCarsInARace(RacesDataResponse race, string constructor)
        {
            int carCount = 0;

            foreach (var result in race.Results)
            {
                var constructorName = $"{result.Constructor.name}";

                if (constructorName == constructor)
                {
                    carCount++;
                }
            }

            return carCount;
        }

        public List<CircuitWinsModel> GetCircuitWinners(int from, int to)
        {
            var circuitWinners = new List<CircuitWinsModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                foreach (var race in races)
                {
                    var circuitName = race.Circuit.circuitName;
                    var winnerName = _nameHelper.GetDriverName(race.Results[0].Driver);

                    lock (lockObject)
                    {
                        foreach (var result in race.Results)
                        {
                            var driverName = _nameHelper.GetDriverName(result.Driver);
                            var newWinsAndParticipationsModel = new WinsAndParticipationsModel { Name = driverName, WinCount = 0, ParticipationsCount = 1 };

                            if (!circuitWinners.Where(circuit => circuit.Name == circuitName).Any())
                            {
                                var newCircuitWinsModel = new CircuitWinsModel { Name = circuitName, Winners = new List<WinsAndParticipationsModel> { newWinsAndParticipationsModel } };
                                circuitWinners.Add(newCircuitWinsModel);
                            }
                            else
                            {
                                if (!circuitWinners.Where(circuit => circuit.Name == circuitName).First().Winners.Where(driver => driver.Name == driverName).Any())
                                {
                                    circuitWinners.Where(circuit => circuit.Name == circuitName).First().Winners.Add(newWinsAndParticipationsModel);
                                }
                                else
                                {
                                    circuitWinners.Where(circuit => circuit.Name == circuitName).First().Winners.Where(driver => driver.Name == driverName).First().ParticipationsCount++;
                                }
                            }
                        }

                        circuitWinners.Where(circuit => circuit.Name == circuitName).First().Winners.Where(winner => winner.Name == winnerName).First().WinCount++; 
                    }
                }
            });

            return circuitWinners;
        }

        public List<UniqueSeasonWinnersModel> GetUniqueSeasonDriverWinners(int from, int to)
        {
            var uniqueDriverWinners = new List<UniqueSeasonWinnersModel>(to - from + 1);
            var lockObject = new object();
            var lockAdd = new object();

            Parallel.For(from, to + 1, year => 
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                var newUniqueSeasonWinnersModel = new UniqueSeasonWinnersModel { Season = year, Winners = new List<string>(), RacesCount = races.Count };

                foreach (var race in races)
                {
                    var winnerName = _nameHelper.GetDriverName(race.Results[0].Driver);

                    lock (lockObject)
                    {
                        if (!newUniqueSeasonWinnersModel.Winners.Where(winner => winner == winnerName).Any())
                        {
                            newUniqueSeasonWinnersModel.Winners.Add(winnerName);
                        } 
                    }
                }

                lock (lockAdd)
                {
                    uniqueDriverWinners.Add(newUniqueSeasonWinnersModel); 
                }
            });

            return uniqueDriverWinners;
        }

        public List<UniqueSeasonWinnersModel> GetUniqueSeasonConstructorWinners(int from, int to)
        {
            var uniqueConstructorWinners = new List<UniqueSeasonWinnersModel>(to - from + 1);
            var lockObject = new object();
            var lockAdd = new object();

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                var newUniqueSeasonWinnersModel = new UniqueSeasonWinnersModel { Season = year, Winners = new List<string>(), RacesCount = races.Count };

                foreach (var race in races)
                {
                    var winnerName = _nameHelper.GetConstructorName(race.Results[0].Constructor);

                    lock (lockObject)
                    {
                        if (!newUniqueSeasonWinnersModel.Winners.Where(winner => winner == winnerName).Any())
                        {
                            newUniqueSeasonWinnersModel.Winners.Add(winnerName);
                        } 
                    }
                }

                lock (lockAdd)
                {
                    uniqueConstructorWinners.Add(newUniqueSeasonWinnersModel); 
                }
            });

            return uniqueConstructorWinners;
        }

        public List<WinnersFromPoleModel> GetWinnersFromPole(int from, int to)
        {
            var winsFromPole = new List<WinnersFromPoleModel>(to - from + 1);
            var lockAdd = new object();

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                var newWinnersFromPoleModel = new WinnersFromPoleModel { Season = year, RacesCount = races.Count, WinnersFromPole = new List<string>() };

                foreach (var race in races)
                {
                    var winnerInformation = race.Results[0];
                    var gridPosition = winnerInformation.grid;
                    var finishPosition = winnerInformation.position;

                    var winnerName = _nameHelper.GetDriverName(winnerInformation.Driver);

                    if (gridPosition == finishPosition)
                    {
                        newWinnersFromPoleModel.WinnersFromPole.Add(winnerName);
                    }
                }

                lock (lockAdd)
                {
                    winsFromPole.Add(newWinnersFromPoleModel);
                }
            });

            return winsFromPole;
        }

        public List<WinsByGridPositionModel> GetWinnersByGridPosition(int from, int to)
        {
            var winnersByGridPosition = new List<WinsByGridPositionModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                foreach (var race in races)
                {
                    var winnerInformation = race.Results[0];
                    var gridPosition = int.Parse(winnerInformation.grid);
                    var winnerName = _nameHelper.GetDriverName(winnerInformation.Driver);
                    var circuitName = race.Circuit.circuitName;
                    var newWinByGridInformationModel = new WinByGridInformationModel { WinnerName = winnerName, CircuitName = circuitName };

                    lock (lockObject)
                    {
                        if (!winnersByGridPosition.Where(grid => grid.GridPosition == gridPosition).Any())
                        {
                            var neWinInformation = new List<WinByGridInformationModel> { newWinByGridInformationModel };
                            var newWinsByGridPositionModel = new WinsByGridPositionModel { GridPosition = gridPosition, WinInformation = neWinInformation };
                            winnersByGridPosition.Add(newWinsByGridPositionModel);
                        }
                        else
                        {
                            winnersByGridPosition.Where(grid => grid.GridPosition == gridPosition).First().WinInformation.Add(newWinByGridInformationModel);
                        }
                    }
                }
            });

            return winnersByGridPosition;
        }
    }
}
