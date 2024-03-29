﻿using F1Statistics.Library.DataAccess.Interfaces;
using F1Statistics.Library.DataAggregation;
using F1Statistics.Library.Helpers.Interfaces;
using F1Statistics.Library.Models;
using F1Statistics.Library.Models.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Tests.DataAggregation
{
    [TestClass]
    public class WinsAggregatorTests
    {
        private WinsAggregator _aggregator;
        private Mock<IResultsDataAccess> _resultsDataAccess;

        [TestInitialize]
        public void Setup()
        {
            _resultsDataAccess = new Mock<IResultsDataAccess>();

            Mock<INameHelper> nameHelper = new Mock<INameHelper>();
            nameHelper.Setup(helper => helper.GetDriverName(It.IsAny<DriverDataResponse>())).Returns<DriverDataResponse>(driver => $"{driver.givenName} {driver.familyName}");
            nameHelper.Setup(helper => helper.GetConstructorName(It.IsAny<ConstructorDataResponse>())).Returns<ConstructorDataResponse>(constructor => $"{constructor.name}");

            Mock<ITimeHelper> timeHelper = new Mock<ITimeHelper>();
            timeHelper.Setup(helper => helper.ConvertGapFromStringToDouble(It.IsAny<string>())).Returns<string>(time => double.Parse(time));

            _aggregator = new WinsAggregator(_resultsDataAccess.Object, nameHelper.Object, timeHelper.Object);
        }

        private List<List<RacesDataResponse>> GenerateRaces()
        {
            var racesList = new List<List<RacesDataResponse>>
            {
                new List<RacesDataResponse>
                {
                    new RacesDataResponse
                    {
                        Circuit = new CircuitDataResponse
                        {
                            circuitName = "FirstCircuit"
                        },
                        Results = new List<ResultsDataResponse>
                        {
                            new ResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    familyName = "FirstFamily",
                                    givenName= "FirstName"
                                },
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "FirstConstructor"
                                },
                                position = "1",
                                grid = "1"
                            },
                            new ResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    familyName = "SecondFamily",
                                    givenName= "SecondName"
                                },
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "SecondConstructor"
                                },
                                position = "2",
                                grid = "2",
                                Time = new TimeDataResponse
                                {
                                    time = "+2.548"
                                }
                            }
                        }
                    } 
                },
                new List<RacesDataResponse>
                {
                    new RacesDataResponse 
                    {
                        Circuit = new CircuitDataResponse
                        {
                            circuitName = "SecondCircuit"
                        },
                        Results = new List<ResultsDataResponse> 
                        { 
                            new ResultsDataResponse 
                            {
                                Driver = new DriverDataResponse 
                                { 
                                    familyName = "FirstFamily", 
                                    givenName = "FirstName" 
                                }, 
                                Constructor = new ConstructorDataResponse 
                                { 
                                    name = "FirstConstructor" 
                                },
                                position = "1",
                                grid = "1"
                            },
                            new ResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    familyName = "SecondFamily",
                                    givenName= "SecondName"
                                },
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "SecondConstructor"
                                },
                                position = "2",
                                grid = "2",
                                Time = new TimeDataResponse
                                {
                                    time = "0.548"
                                }
                            }
                        } 
                    },
                    new RacesDataResponse 
                    {
                        Circuit = new CircuitDataResponse
                        {
                            circuitName = "ThirdCircuit"
                        },
                        Results = new List<ResultsDataResponse>
                        { 
                            new ResultsDataResponse 
                            { 
                                Driver = new DriverDataResponse 
                                { 
                                    familyName = "SecondFamily", 
                                    givenName = "SecondName" 
                                }, 
                                Constructor = new ConstructorDataResponse 
                                { 
                                    name = "SecondConstructor" 
                                },
                                position = "1",
                                grid = "1"
                            },
                            new ResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    familyName = "FirstFamily",
                                    givenName= "FirstName"
                                },
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "FirstConstructor"
                                },
                                position = "2",
                                grid = "2",
                                Time = new TimeDataResponse
                                {
                                    time = "+2"
                                }
                            }
                        }
                    },
                    new RacesDataResponse
                    {
                        Circuit = new CircuitDataResponse
                        {
                            circuitName = "FirstCircuit"
                        },
                        Results = new List<ResultsDataResponse>
                        {
                            new ResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    familyName = "FirstFamily",
                                    givenName = "FirstName"
                                },
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "FirstConstructor"
                                },
                                position = "1",
                                grid = "1"
                            },
                            new ResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    familyName = "SecondFamily",
                                    givenName= "SecondName"
                                },
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "SecondConstructor"
                                },
                                position = "2",
                                grid = "2",
                                Time = new TimeDataResponse
                                {
                                    time = "0.334"
                                }
                            },
                            new ResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    familyName = "ThirdFamily",
                                    givenName= "ThirdName"
                                },
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "SecondConstructor"
                                },
                                position = "3",
                                grid = "3",
                                Time = new TimeDataResponse
                                {
                                    time = "0.554"
                                }
                            }
                        }
                    }
                }
            };

            return racesList;
        }

        [TestMethod]
        public void GetDriversWins_ReturnAggregatedDriversList_IfThereAreAnyDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedDriversWinners = new List<WinsModel> 
            {
                new WinsModel 
                {
                    Name = "FirstName FirstFamily", 
                    WinsByYear = new List<WinsByYearModel>
                    {
                        new WinsByYearModel
                        {
                            Year = 1,
                            WinInformation = new List<WinInformationModel>
                            {
                                new WinInformationModel
                                {
                                    CircuitName = "FirstCircuit",
                                    GapToSecond = 2.548,
                                    GridPosition = 1
                                }
                            }
                        },
                        new WinsByYearModel
                        {
                            Year = 2,
                            WinInformation = new List<WinInformationModel>
                            {
                                new WinInformationModel
                                {
                                    CircuitName = "SecondCircuit",
                                    GapToSecond = 0.548,
                                    GridPosition = 1
                                },
                                new WinInformationModel
                                {
                                    CircuitName = "FirstCircuit",
                                    GapToSecond = 0.334,
                                    GridPosition = 1
                                }
                            }
                        }
                    }
                },
                new WinsModel 
                {
                    Name = "SecondName SecondFamily", 
                    WinsByYear = new List<WinsByYearModel>
                    {
                        new WinsByYearModel
                        {
                            Year = 2,
                            WinInformation = new List<WinInformationModel>
                            {
                                new WinInformationModel
                                {
                                    CircuitName = "ThirdCircuit",
                                    GapToSecond = 2,
                                    GridPosition = 1
                                }
                            }
                        }
                    }
                }
            };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int t = 0; t < 10000; t++)
            {
                // Act
                var actual = _aggregator.GetDriversWins(from, to);
                actual.Sort((x, y) => y.TotalWinCount.CompareTo(x.TotalWinCount));
                actual.ForEach(model => model.WinsByYear.Sort((x, y) => x.Year.CompareTo(y.Year)));

                // Assert
                Assert.AreEqual(expectedDriversWinners.Count, actual.Count);

                for (int i = 0; i < expectedDriversWinners.Count; i++)
                {
                    Assert.AreEqual(expectedDriversWinners[i].Name, actual[i].Name);
                    Assert.AreEqual(expectedDriversWinners[i].TotalWinCount, actual[i].TotalWinCount);
                    Assert.AreEqual(expectedDriversWinners[i].WinsByYear.Count, actual[i].WinsByYear.Count);

                    for (int j = 0; j < expectedDriversWinners[i].WinsByYear.Count; j++)
                    {
                        Assert.AreEqual(expectedDriversWinners[i].WinsByYear[j].Year, actual[i].WinsByYear[j].Year);
                        Assert.AreEqual(expectedDriversWinners[i].WinsByYear[j].YearWinCount, actual[i].WinsByYear[j].YearWinCount);
                        Assert.AreEqual(expectedDriversWinners[i].WinsByYear[j].WinInformation.Count, actual[i].WinsByYear[j].WinInformation.Count);

                        for (int k = 0; k < expectedDriversWinners[i].WinsByYear[j].WinInformation.Count; k++)
                        {
                            Assert.AreEqual(expectedDriversWinners[i].WinsByYear[j].WinInformation[k].CircuitName, actual[i].WinsByYear[j].WinInformation[k].CircuitName);
                            Assert.AreEqual(expectedDriversWinners[i].WinsByYear[j].WinInformation[k].GapToSecond, actual[i].WinsByYear[j].WinInformation[k].GapToSecond);
                            Assert.AreEqual(expectedDriversWinners[i].WinsByYear[j].WinInformation[k].GridPosition, actual[i].WinsByYear[j].WinInformation[k].GridPosition);
                        }
                    }
                } 
            }
        }

        [TestMethod]
        public void GetDriversWins_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedDriversWinners = new List<WinsModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetDriversWins(from, to);

                // Assert
                Assert.AreEqual(expectedDriversWinners.Count, actual.Count); 
            }
        }

        [TestMethod]
        public void GetConstructorsWins_ReturnAggregatedConstructorsList_IfThereAreAnyConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedConstructorWinners = new List<WinsModel> 
            {
                new WinsModel 
                {
                    Name = "FirstConstructor", 
                    WinsByYear = new List<WinsByYearModel>
                    {
                        new WinsByYearModel
                        {
                            Year = 1,
                            WinInformation = new List<WinInformationModel>
                            {
                                new WinInformationModel
                                {
                                    CircuitName = "FirstCircuit",
                                    GapToSecond = 2.548,
                                    GridPosition = 1
                                }
                            }
                        },
                        new WinsByYearModel
                        {
                            Year = 2,
                            WinInformation = new List<WinInformationModel>
                            {
                                new WinInformationModel
                                {
                                    CircuitName = "SecondCircuit",
                                    GapToSecond = 0.548,
                                    GridPosition = 1
                                },
                                new WinInformationModel
                                {
                                    CircuitName = "FirstCircuit",
                                    GapToSecond = 0.334,
                                    GridPosition = 1
                                }
                            }
                        }
                    } 
                },
                new WinsModel 
                {
                    Name = "SecondConstructor",
                    WinsByYear = new List<WinsByYearModel>
                    {
                        new WinsByYearModel
                        {
                            Year = 2,
                            WinInformation = new List<WinInformationModel>
                            {
                                new WinInformationModel
                                {
                                    CircuitName = "ThirdCircuit",
                                    GapToSecond = 2,
                                    GridPosition = 1
                                }
                            }
                        }
                    }
                }
            };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int t = 0; t < 10000; t++)
            {
                // Act
                var actual = _aggregator.GetConstructorsWins(from, to);
                actual.Sort((x, y) => y.TotalWinCount.CompareTo(x.TotalWinCount));
                actual.ForEach(model => model.WinsByYear.Sort((x, y) => x.Year.CompareTo(y.Year)));

                // Assert
                Assert.AreEqual(expectedConstructorWinners.Count, actual.Count);

                for (int i = 0; i < expectedConstructorWinners.Count; i++)
                {
                    Assert.AreEqual(expectedConstructorWinners[i].Name, actual[i].Name);
                    Assert.AreEqual(expectedConstructorWinners[i].TotalWinCount, actual[i].TotalWinCount);
                    Assert.AreEqual(expectedConstructorWinners[i].WinsByYear.Count, actual[i].WinsByYear.Count);

                    for (int j = 0; j < expectedConstructorWinners[i].WinsByYear.Count; j++)
                    {
                        Assert.AreEqual(expectedConstructorWinners[i].WinsByYear[j].Year, actual[i].WinsByYear[j].Year);
                        Assert.AreEqual(expectedConstructorWinners[i].WinsByYear[j].YearWinCount, actual[i].WinsByYear[j].YearWinCount);
                        Assert.AreEqual(expectedConstructorWinners[i].WinsByYear[j].WinInformation.Count, actual[i].WinsByYear[j].WinInformation.Count);

                        for (int k = 0; k < expectedConstructorWinners[i].WinsByYear[j].WinInformation.Count; k++)
                        {
                            Assert.AreEqual(expectedConstructorWinners[i].WinsByYear[j].WinInformation[k].CircuitName, actual[i].WinsByYear[j].WinInformation[k].CircuitName);
                            Assert.AreEqual(expectedConstructorWinners[i].WinsByYear[j].WinInformation[k].GapToSecond, actual[i].WinsByYear[j].WinInformation[k].GapToSecond);
                            Assert.AreEqual(expectedConstructorWinners[i].WinsByYear[j].WinInformation[k].GridPosition, actual[i].WinsByYear[j].WinInformation[k].GridPosition);
                        }
                    }
                } 
            }
        }

        [TestMethod]
        public void GetConstructorsWins_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedConstructorWinners = new List<WinsModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetConstructorsWins(from, to);

                // Assert
                Assert.AreEqual(expectedConstructorWinners.Count, actual.Count); 
            }
        }

        [TestMethod]
        public void GetDriversWinPercent_ReturnAggregatedDriversWithWinPercentageList_IfThereAreAnyDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedDriversWinPercent = new List<AverageWinsModel> 
            {
                new AverageWinsModel 
                {
                    Name = "FirstName FirstFamily", 
                    WinCount = 3,
                    ParticipationCount = 4 
                },
                new AverageWinsModel 
                {
                    Name = "SecondName SecondFamily", 
                    WinCount = 1, 
                    ParticipationCount = 4 
                },
                new AverageWinsModel
                {
                    Name = "ThirdName ThirdFamily",
                    WinCount = 0,
                    ParticipationCount = 1
                }
            };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetDriversWinPercent(from, to);
                actual.Sort((x, y) => y.AverageWins.CompareTo(x.AverageWins));

                // Assert
                Assert.AreEqual(expectedDriversWinPercent.Count, actual.Count);

                for (int i = 0; i < expectedDriversWinPercent.Count; i++)
                {
                    Assert.AreEqual(expectedDriversWinPercent[i].Name, actual[i].Name);
                    Assert.AreEqual(expectedDriversWinPercent[i].WinCount, actual[i].WinCount);
                    Assert.AreEqual(expectedDriversWinPercent[i].ParticipationCount, actual[i].ParticipationCount);
                    Assert.AreEqual(expectedDriversWinPercent[i].AverageWins, actual[i].AverageWins);
                } 
            }
        }

        [TestMethod]
        public void GetDriversWinPercent_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedDriversWinPercent = new List<AverageWinsModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetDriversWinPercent(from, to);

                // Assert
                Assert.AreEqual(expectedDriversWinPercent.Count, actual.Count); 
            }
        }

        [TestMethod]
        public void GetConstructorsWinPercent_ReturnAggregatedConstructorsWithWinPercentageList_IfThereAreAnyConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedConstructorsWinPercent = new List<AverageWinsModel> 
            {
                new AverageWinsModel 
                {
                    Name = "FirstConstructor", 
                    WinCount = 3, 
                    ParticipationCount = 4 
                },
                new AverageWinsModel 
                {
                    Name = "SecondConstructor", 
                    WinCount = 1, 
                    ParticipationCount = 4 
                }
            };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetConstructorsWinPercent(from, to);
                actual.Sort((x, y) => y.AverageWins.CompareTo(x.AverageWins));

                // Assert
                Assert.AreEqual(expectedConstructorsWinPercent.Count, actual.Count);

                for (int i = 0; i < expectedConstructorsWinPercent.Count; i++)
                {
                    Assert.AreEqual(expectedConstructorsWinPercent[i].Name, actual[i].Name);
                    Assert.AreEqual(expectedConstructorsWinPercent[i].WinCount, actual[i].WinCount);
                    Assert.AreEqual(expectedConstructorsWinPercent[i].ParticipationCount, actual[i].ParticipationCount);
                    Assert.AreEqual(expectedConstructorsWinPercent[i].AverageWins, actual[i].AverageWins);
                } 
            }
        }

        [TestMethod]
        public void GetConstructorsWinPercent_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedConstructorsWinPercent = new List<AverageWinsModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetConstructorsWinPercent(from, to);

                // Assert
                Assert.AreEqual(expectedConstructorsWinPercent.Count, actual.Count); 
            }
        }

        [TestMethod]
        public void GetCircuitWinners_ReturnAggregatedCircuitWinnersList_IfThereAreAnyCircuits()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedCircuitWinners = new List<CircuitWinsModel>
            {
                new CircuitWinsModel
                {
                    Name = "FirstCircuit",
                    Winners = new List<WinsAndParticipationsModel>
                    {
                        new WinsAndParticipationsModel
                        {
                            Name = "FirstName FirstFamily",
                            WinCount = 2,
                            ParticipationsCount = 2
                        },
                        new WinsAndParticipationsModel
                        {
                            Name = "SecondName SecondFamily",
                            WinCount = 0,
                            ParticipationsCount = 2
                        }
                    }
                },
                new CircuitWinsModel
                {
                    Name = "SecondCircuit",
                    Winners = new List<WinsAndParticipationsModel>
                    {
                        new WinsAndParticipationsModel
                        {
                            Name = "FirstName FirstFamily",
                            WinCount = 1,
                            ParticipationsCount = 1
                        },
                        new WinsAndParticipationsModel
                        {
                            Name = "SecondName SecondFamily",
                            WinCount = 0,
                            ParticipationsCount = 1
                        }
                    }
                },
                new CircuitWinsModel
                {
                    Name = "ThirdCircuit",
                    Winners = new List<WinsAndParticipationsModel>
                    {
                        new WinsAndParticipationsModel
                        {
                            Name = "SecondName SecondFamily",
                            WinCount = 1,
                            ParticipationsCount = 1
                        },
                        new WinsAndParticipationsModel
                        {
                            Name = "FirstName FirstFamily",
                            WinCount = 0,
                            ParticipationsCount = 1
                        }
                    }
                }
            };

            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetCircuitWinners(from, to);
                actual.ForEach(circuit => circuit.Winners.Sort((x, y) => y.WinCount.CompareTo(x.WinCount)));
                actual.Sort((x, y) => x.Name.CompareTo(y.Name));

                // Assert
                Assert.AreEqual(expectedCircuitWinners.Count, actual.Count);

                for (int i = 0; i < expectedCircuitWinners.Count; i++)
                {
                    Assert.AreEqual(expectedCircuitWinners[i].Name, actual[i].Name);

                    for (int j = 0; j < expectedCircuitWinners[i].Winners.Count; j++)
                    {
                        Assert.AreEqual(expectedCircuitWinners[i].Winners[j].Name, actual[i].Winners[j].Name);
                        Assert.AreEqual(expectedCircuitWinners[i].Winners[j].WinCount, actual[i].Winners[j].WinCount);
                        Assert.AreEqual(expectedCircuitWinners[i].Winners[j].ParticipationsCount, actual[i].Winners[j].ParticipationsCount);
                    }
                } 
            }
        }

        [TestMethod]
        public void GetCircuitWinners_ReturnEmptyList_IfThereAreNoCircuits()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedCircuitWinners = new List<CircuitWinsModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());
            
            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetCircuitWinners(from, to);

                // Assert
                Assert.AreEqual(expectedCircuitWinners.Count, actual.Count); 
            }
        }

        [TestMethod]
        public void GetUniqueSeasonDriverWinners_ReturnAggregatedUniqueSeasonWinners_IfThereAreAnyWinners()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedUniqueWinners = new List<UniqueSeasonWinnersModel>
            {
                new UniqueSeasonWinnersModel
                {
                    Season = 1,
                    Winners = new List<string>
                    {
                        "FirstName FirstFamily"
                    },
                    RacesCount = 1
                },
                new UniqueSeasonWinnersModel
                {
                    Season = 2,
                    Winners = new List<string>
                    {
                        "FirstName FirstFamily",
                        "SecondName SecondFamily"
                    },
                    RacesCount = 3
                }
            };

            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetUniqueSeasonDriverWinners(from, to);
                actual.Sort((x, y) => x.Season.CompareTo(y.Season));

                // Assert
                Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);

                for (int i = 0; i < expectedUniqueWinners.Count; i++)
                {
                    Assert.AreEqual(expectedUniqueWinners[i].Season, actual[i].Season);
                    Assert.AreEqual(expectedUniqueWinners[i].RacesCount, actual[i].RacesCount);
                    Assert.AreEqual(expectedUniqueWinners[i].UniqueWinnersCount, actual[i].UniqueWinnersCount);
                } 
            }
        }

        [TestMethod]
        public void GetUniqueSeasonDriverWinners_ReturnEmptyList_IfThereAreNoWinners()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedUniqueWinners = new List<UniqueSeasonWinnersModel> { new UniqueSeasonWinnersModel { Winners = new List<string>() }, new UniqueSeasonWinnersModel { Winners = new List<string>() } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetUniqueSeasonDriverWinners(from, to);

                // Assert
                Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);
                Assert.AreEqual(expectedUniqueWinners[0].Winners.Count, actual[0].Winners.Count);
            }
        }

        [TestMethod]
        public void GetUniqueSeasonConstructorWinners_ReturnAggregatedUniqueSeasonWinners_IfThereAreAnyWinners()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedUniqueWinners = new List<UniqueSeasonWinnersModel>
            {
                new UniqueSeasonWinnersModel
                {
                    Season = 1,
                    Winners = new List<string>
                    {
                        "FirstConstructor"
                    },
                    RacesCount = 1
                },
                new UniqueSeasonWinnersModel
                {
                    Season = 2,
                    Winners = new List<string>
                    {
                        "FirstConstructor",
                        "SecondConstructor"
                    },
                    RacesCount = 3
                }
            };

            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetUniqueSeasonConstructorWinners(from, to);
                actual.Sort((x, y) => x.Season.CompareTo(y.Season));

                // Assert
                Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);

                for (int i = 0; i < expectedUniqueWinners.Count; i++)
                {
                    Assert.AreEqual(expectedUniqueWinners[i].Season, actual[i].Season);
                    Assert.AreEqual(expectedUniqueWinners[i].RacesCount, actual[i].RacesCount);
                    Assert.AreEqual(expectedUniqueWinners[i].UniqueWinnersCount, actual[i].UniqueWinnersCount);
                } 
            }
        }

        [TestMethod]
        public void GetUniqueSeasonConstructorWinners_ReturnEmptyList_IfThereAreNoWinners()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedUniqueWinners = new List<UniqueSeasonWinnersModel> { new UniqueSeasonWinnersModel { Winners = new List<string>() }, new UniqueSeasonWinnersModel { Winners = new List<string>() } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetUniqueSeasonConstructorWinners(from, to);

                // Assert
                Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);
                Assert.AreEqual(expectedUniqueWinners[0].Winners.Count, actual[0].Winners.Count);
            }
        }

        [TestMethod]
        public void GetWinnersFromPole_ReturnAggregatedWinnersFromPoleList_IfThereAreAnyWinnersFromPole()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinnersFromPole = new List<WinnersFromPoleModel> 
            {
                new WinnersFromPoleModel 
                {
                    Season = 1, 
                    RacesCount = 1, 
                    WinnersFromPole = new List<string> 
                    {
                        "FirstName FirstFamily" 
                    }
                },
                new WinnersFromPoleModel 
                {
                    Season = 2, 
                    RacesCount = 3, 
                    WinnersFromPole = new List<string> 
                    {
                        "FirstName FirstFamily", 
                        "FirstName FirstFamily",
                        "SecondName SecondFamily" 
                    }
                }
            };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetWinnersFromPole(from, to);
                actual.Sort((x, y) => x.Season.CompareTo(y.Season));

                // Assert
                Assert.AreEqual(expectedWinnersFromPole.Count, actual.Count);

                for (int i = 0; i < expectedWinnersFromPole.Count; i++)
                {
                    Assert.AreEqual(expectedWinnersFromPole[i].Season, actual[i].Season);
                    Assert.AreEqual(expectedWinnersFromPole[i].WinsFromPoleCount, actual[i].WinsFromPoleCount);
                    Assert.AreEqual(expectedWinnersFromPole[i].RacesCount, actual[i].RacesCount);
                }
            }
        }

        [TestMethod]
        public void GetWinnersFromPole_ReturnEmptyList_IfThereAreNoWinnersFromPole()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedUniquePoleSittersConstructors = new List<WinnersFromPoleModel> { new WinnersFromPoleModel { WinnersFromPole = new List<string>() }, new WinnersFromPoleModel { WinnersFromPole = new List<string>() } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetWinnersFromPole(from, to);

                // Assert
                Assert.AreEqual(expectedUniquePoleSittersConstructors.Count, actual.Count);
                Assert.AreEqual(expectedUniquePoleSittersConstructors[0].WinnersFromPole.Count, actual[0].WinnersFromPole.Count);
                Assert.AreEqual(expectedUniquePoleSittersConstructors[1].WinnersFromPole.Count, actual[1].WinnersFromPole.Count);
            }
        }

        [TestMethod]
        public void GetWinnersByGridPosition_ReturnAggregatedWinnersByGridPositionList_IfThereAreAnyWinners()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinnersByGridPosition = new List<WinsByGridPositionModel> 
            {
                new WinsByGridPositionModel
                {
                    GridPosition = 1,
                    WinInformation = new List<WinByGridInformationModel>
                    {
                        new WinByGridInformationModel
                        {
                            CircuitName = "FirstCircuit",
                            WinnerName = "FirstName FirstFamily"
                        },
                        new WinByGridInformationModel
                        {
                            CircuitName = "FirstCircuit",
                            WinnerName = "FirstName FirstFamily"
                        },
                        new WinByGridInformationModel
                        {
                            CircuitName = "SecondCircuit",
                            WinnerName = "FirstName FirstFamily"
                        },
                        new WinByGridInformationModel
                        {
                            CircuitName = "ThirdCircuit",
                            WinnerName = "SecondName SecondFamily"
                        }
                    }
                }
            };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetWinnersByGridPosition(from, to);
                actual.Sort((x, y) => x.GridPosition.CompareTo(y.GridPosition));
                actual.ForEach(model => model.WinInformation.Sort((x, y) => x.CircuitName.CompareTo(y.CircuitName)));

                // Assert
                Assert.AreEqual(expectedWinnersByGridPosition.Count, actual.Count);

                for (int i = 0; i < expectedWinnersByGridPosition.Count; i++)
                {
                    Assert.AreEqual(expectedWinnersByGridPosition[i].GridPosition, actual[i].GridPosition);
                    Assert.AreEqual(expectedWinnersByGridPosition[i].WinCount, actual[i].WinCount);
                    Assert.AreEqual(expectedWinnersByGridPosition[i].WinInformation.Count, actual[i].WinInformation.Count);

                    for (int j = 0; j < expectedWinnersByGridPosition[i].WinInformation.Count; j++)
                    {
                        Assert.AreEqual(expectedWinnersByGridPosition[i].WinInformation[j].CircuitName, actual[i].WinInformation[j].CircuitName);
                        Assert.AreEqual(expectedWinnersByGridPosition[i].WinInformation[j].WinnerName, actual[i].WinInformation[j].WinnerName);
                    }
                }
            }
        }

        [TestMethod]
        public void GetWinnersByGridPosition_ReturnEmptyList_IfThereAreNoWinners()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinnersByGridPosition = new List<WinsByGridPositionModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetWinnersByGridPosition(from, to);

                // Assert
                Assert.AreEqual(expectedWinnersByGridPosition.Count, actual.Count);
            }
        }
    }
}
