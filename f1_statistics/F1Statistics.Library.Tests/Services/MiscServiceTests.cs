﻿using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Models;
using F1Statistics.Library.Services;
using F1Statistics.Library.Validators.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Statistics.Library.Tests.Services
{
    [TestClass]
    public class MiscServiceTests
    {
        private MiscService _service;
        private Mock<IOptionsValidator> _validator;
        private Mock<IMiscAggregator> _aggregator;

        [TestInitialize]
        public void Setup()
        {
            _validator = new Mock<IOptionsValidator>();
            _aggregator = new Mock<IMiscAggregator>();

            _validator.Setup((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>())).Verifiable();

            _service = new MiscService(_validator.Object, _aggregator.Object);
        }

        private List<SeasonRacesModel> GenerateSeasonRaces()
        {
            var sseasonRaces = new List<SeasonRacesModel>
            {
                new SeasonRacesModel
                {
                    Season = 1,
                    RaceCount = 10
                },
                new SeasonRacesModel
                {
                    Season = 2,
                    RaceCount = 20
                }
            };

            return sseasonRaces;
        }

        private List<HatTrickModel> GenerateHatTricks()
        {
            var hatTricks = new List<HatTrickModel>
            {
                new HatTrickModel
                {
                    Name = "First",
                    HatTrickCount = 5
                },
                new HatTrickModel
                {
                    Name = "Second",
                    HatTrickCount = 3
                }
            };

            return hatTricks;
        }

        private List<GrandSlamModel> GenerateGrandSlams()
        {
            var grandSlams = new List<GrandSlamModel>
            {
                new GrandSlamModel
                {
                    Name = "First",
                    GrandSlamCount = 5
                },
                new GrandSlamModel
                {
                    Name = "Second",
                    GrandSlamCount = 3
                }
            };

            return grandSlams;
        }

        private List<DidNotFinishModel> GenerateNonFinishers()
        {
            var nonFinishers = new List<DidNotFinishModel>
            {
                new DidNotFinishModel
                {
                    Name = "First",
                    DidNotFinishByYear = new List<DidNotFinishByYearModel>
                    {
                        new DidNotFinishByYearModel
                        {
                            Year = 1,
                            DidNotFinishInformation = new List<DidNotFinishInformationModel>
                            {
                                new DidNotFinishInformationModel
                                {
                                    CircuitName = "First",
                                    LapsCompleted = 10
                                }
                            }
                        },
                        new DidNotFinishByYearModel
                        {
                            Year = 2,
                            DidNotFinishInformation = new List<DidNotFinishInformationModel>
                            {
                                new DidNotFinishInformationModel
                                {
                                    CircuitName = "Second",
                                    LapsCompleted = 0
                                }
                            }
                        }
                    }
                },
                new DidNotFinishModel
                {
                    Name = "Second",
                    DidNotFinishByYear = new List<DidNotFinishByYearModel>
                    {
                        new DidNotFinishByYearModel
                        {
                            Year = 1,
                            DidNotFinishInformation = new List<DidNotFinishInformationModel>
                            {
                                new DidNotFinishInformationModel
                                {
                                    CircuitName = "Third",
                                    LapsCompleted = 56
                                }
                            }
                        }
                    }
                }
            };

            return nonFinishers;
        }

        private List<SeasonPositionChangesModel> GenerateSeasonPositionChanges()
        {
            var seasonPositionChanges = new List<SeasonPositionChangesModel>
            {
                new SeasonPositionChangesModel
                {
                    Year = 1,
                    PositionChanges = new List<DriverPositionChangeModel>
                    {
                        new DriverPositionChangeModel
                        {
                            Name = "First",
                            DriverPositionChangeInformation = new List<DriverPositionChangeInformationModel>
                            {
                                new DriverPositionChangeInformationModel
                                {
                                    CircuitName = "First",
                                    RacePositionChange = 2
                                },
                                new DriverPositionChangeInformationModel
                                {
                                    CircuitName = "Second",
                                    RacePositionChange = 8
                                }
                            },
                            ChampionshipPosition = 1
                        },
                        new DriverPositionChangeModel
                        {
                            Name = "Second",
                            DriverPositionChangeInformation = new List<DriverPositionChangeInformationModel>
                            {
                                new DriverPositionChangeInformationModel
                                {
                                    CircuitName = "First",
                                    RacePositionChange = -2
                                },
                                new DriverPositionChangeInformationModel
                                {
                                    CircuitName = "Second",
                                    RacePositionChange = -8
                                }
                            },
                            ChampionshipPosition = 2
                        }
                    }
                },
                new SeasonPositionChangesModel
                {
                    Year = 2,
                    PositionChanges = new List<DriverPositionChangeModel>
                    {
                        new DriverPositionChangeModel
                        {
                            Name = "First",
                            DriverPositionChangeInformation = new List<DriverPositionChangeInformationModel>
                            {
                                new DriverPositionChangeInformationModel
                                {
                                    CircuitName = "First",
                                    RacePositionChange = -2
                                },
                                new DriverPositionChangeInformationModel
                                {
                                    CircuitName = "Second",
                                    RacePositionChange = 22
                                }
                            },
                            ChampionshipPosition = 1
                        },
                        new DriverPositionChangeModel
                        {
                            Name = "Second",
                            DriverPositionChangeInformation = new List<DriverPositionChangeInformationModel>
                            {
                                new DriverPositionChangeInformationModel
                                {
                                    CircuitName = "First",
                                    RacePositionChange = -5
                                },
                                new DriverPositionChangeInformationModel
                                {
                                    CircuitName = "Second",
                                    RacePositionChange = 5
                                }
                            },
                            ChampionshipPosition = 2
                        }
                    }
                }
            };

            return seasonPositionChanges;
        }

        private List<FrontRowModel> GenerateConstructorsFrontRows()
        {
            var constructorFrontRows = new List<FrontRowModel>
            {
                new FrontRowModel
                {
                    Name = "First",
                    FrontRowInformation = new List<FrontRowInformationModel>
                    {
                        new FrontRowInformationModel
                        {
                            CircuitName = "First",
                            CircuitFrontRowCount = 2
                        },
                        new FrontRowInformationModel
                        {
                            CircuitName = "Second",
                            CircuitFrontRowCount = 1
                        }
                    }
                },
                new FrontRowModel
                {
                    Name = "Second",
                    FrontRowInformation = new List<FrontRowInformationModel>
                    {
                        new FrontRowInformationModel
                        {
                            CircuitName = "Third",
                            CircuitFrontRowCount = 1
                        }
                    }
                }
            };

            return constructorFrontRows;
        }

        private List<DriverFinishingPositionsModel> GenerateDriversFinishingPositions()
        {
            var driversfinishingPositions = new List<DriverFinishingPositionsModel>()
            {
                new DriverFinishingPositionsModel
                {
                    Name = "First",
                    FinishingPositions = new List<FinishingPositionModel>
                    {
                        new FinishingPositionModel
                        {
                            FinishingPosition = 1,
                            FinishingPositionInformation = new List<FinishingPositionInformationModel>
                            {
                                new FinishingPositionInformationModel
                                {
                                    CircuitName = "First",
                                    FinishedRace = true
                                },
                                new FinishingPositionInformationModel
                                {
                                    CircuitName = "Second",
                                    FinishedRace = false
                                }
                            }
                        },
                        new FinishingPositionModel
                        {
                            FinishingPosition = 2,
                            FinishingPositionInformation = new List<FinishingPositionInformationModel>
                            {
                                new FinishingPositionInformationModel
                                {
                                    CircuitName = "First",
                                    FinishedRace = false
                                },
                                new FinishingPositionInformationModel
                                {
                                    CircuitName = "Second",
                                    FinishedRace = true
                                }
                            }
                        }
                    }
                },
                new DriverFinishingPositionsModel
                {
                    Name = "Second",
                    FinishingPositions = new List<FinishingPositionModel>
                    {
                        new FinishingPositionModel
                        {
                            FinishingPosition = 2,
                            FinishingPositionInformation = new List<FinishingPositionInformationModel>
                            {
                                new FinishingPositionInformationModel
                                {
                                    CircuitName = "First",
                                    FinishedRace = true
                                },
                                new FinishingPositionInformationModel
                                {
                                    CircuitName = "Second",
                                    FinishedRace = true
                                }
                            }
                        }
                    }
                }
            };

            return driversfinishingPositions;
        }

        private List<SeasonStandingsChangesModel> GenerateSeasonStandingsChanges()
        {
            var seasonStandings = new List<SeasonStandingsChangesModel>
            {
                new SeasonStandingsChangesModel
                {
                    Year = 1,
                    Standings = new List<StandingModel>
                    {
                        new StandingModel
                        {
                            Name = "First",
                            Rounds = new List<RoundModel>
                            {
                                new RoundModel
                                {
                                    Round = 1,
                                    Position = 2,
                                    Points = 18
                                },
                                new RoundModel
                                {
                                    Round = 2,
                                    Position = 1,
                                    Points = 33
                                }
                            }
                        },
                        new StandingModel
                        {
                            Name = "Second",
                            Rounds = new List<RoundModel>
                            {
                                new RoundModel
                                {
                                    Round = 1,
                                    Position = 1,
                                    Points = 25
                                },
                                new RoundModel
                                {
                                    Round = 2,
                                    Position = 2,
                                    Points = 32
                                }
                            }
                        }
                    }
                },
                new SeasonStandingsChangesModel
                {
                    Year = 2,
                    Standings = new List<StandingModel>
                    {
                        new StandingModel
                        {
                            Name = "First",
                            Rounds = new List<RoundModel>
                            {
                                new RoundModel
                                {
                                    Round = 1,
                                    Position = 1,
                                    Points = 25
                                },
                                new RoundModel
                                {
                                    Round = 2,
                                    Position = 1,
                                    Points = 50
                                }
                            }
                        },
                        new StandingModel
                        {
                            Name = "Second",
                            Rounds = new List<RoundModel>
                            {
                                new RoundModel
                                {
                                    Round = 1,
                                    Position = 2,
                                    Points = 18
                                },
                                new RoundModel
                                {
                                    Round = 2,
                                    Position = 2,
                                    Points = 32
                                }
                            }
                        }
                    }
                }
            };

            return seasonStandings;
        }

        private List<RacePositionChangesModel> GenerateDriversPositionChangesDuringRace()
        {
            var positionChangesDuringRace = new List<RacePositionChangesModel>
            {
                new RacePositionChangesModel
                {
                    Name = "First",
                    Laps = new List<LapPositionModel>
                    {
                        new LapPositionModel
                        {
                            LapNumber = 1,
                            Position = 1
                        },
                        new LapPositionModel
                        {
                            LapNumber = 2,
                            Position = 1
                        },
                        new LapPositionModel
                        {
                            LapNumber = 3,
                            Position = 1
                        }
                    }
                },
                new RacePositionChangesModel
                {
                    Name = "Second",
                    Laps = new List<LapPositionModel>
                    {
                        new LapPositionModel
                        {
                            LapNumber = 1,
                            Position = 2
                        },
                        new LapPositionModel
                        {
                            LapNumber = 2,
                            Position = 3
                        },
                        new LapPositionModel
                        {
                            LapNumber = 3,
                            Position = 2
                        }
                    }
                },
                new RacePositionChangesModel
                {
                    Name = "Third",
                    Laps = new List<LapPositionModel>
                    {
                        new LapPositionModel
                        {
                            LapNumber = 1,
                            Position = 3
                        },
                        new LapPositionModel
                        {
                            LapNumber = 2,
                            Position = 2
                        },
                        new LapPositionModel
                        {
                            LapNumber = 3,
                            Position = 3
                        }
                    }
                }
            };

            return positionChangesDuringRace;
        }

        private List<LapTimesModel> GenerateDriversLapTimes()
        {
            var lapTimes = new List<LapTimesModel>
            {
                new LapTimesModel
                {
                    Name = "First",
                    Timings = new List<double>
                    {
                        1.5,
                        1,
                        2
                    }
                },
                new LapTimesModel
                {
                    Name = "Second",
                    Timings = new List<double>
                    {
                        2.5,
                        3,
                        2.7
                    }
                }
            };

            return lapTimes;
        }

        [TestMethod]
        public void AggregateRaceCountPerSeason_ReturnSortedAggregatedRaceCountPerSeasonList_IfThereAreAnyRaces()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedSeasonRaces = GenerateSeasonRaces();
            expectedSeasonRaces.Sort((x, y) => x.Season.CompareTo(y.Season));
            _aggregator.Setup((aggregator) => aggregator.GetRaceCountPerSeason(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateSeasonRaces());

            // Act
            var actual = _service.AggregateRaceCountPerSeason(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedSeasonRaces.Count, actual.Count);

            for (int i = 0; i < expectedSeasonRaces.Count; i++)
            {
                Assert.AreEqual(expectedSeasonRaces[i].Season, actual[i].Season);
                Assert.AreEqual(expectedSeasonRaces[i].RaceCount, actual[i].RaceCount);
            }
        }

        [TestMethod]
        public void AggregateRaceCountPerSeason_ReturnEmptyList_IfThereAreNoRaces()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedSeasonRaces = new List<SeasonRacesModel>();
            _aggregator.Setup((aggregator) => aggregator.GetRaceCountPerSeason(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedSeasonRaces);

            // Act
            var actual = _service.AggregateRaceCountPerSeason(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedSeasonRaces.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateHatTricks_ReturnSortedAggregatedHatTricksList_IfThereAreAnyDriversWithHatTricks()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2004, YearTo = 2005 };
            var expectedHatTricks = GenerateHatTricks();
            expectedHatTricks.Sort((x, y) => y.HatTrickCount.CompareTo(x.HatTrickCount));
            _aggregator.Setup((aggregator) => aggregator.GetHatTricks(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateHatTricks());
            _validator.Setup((validator) => validator.AreFastestLapYearsValid(options)).Returns(true);

            // Act
            var actual = _service.AggregateHatTricks(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedHatTricks.Count, actual.Count);

            for (int i = 0; i < expectedHatTricks.Count; i++)
            {
                Assert.AreEqual(expectedHatTricks[i].Name, actual[i].Name);
                Assert.AreEqual(expectedHatTricks[i].HatTrickCount, actual[i].HatTrickCount);
            }
        }

        [TestMethod]
        public void AggregateHatTricks_ReturnEmptyList_IfThereAreNoDriversWithHatTricks()
        {
            // Arrange
            var options = new OptionsModel { Season = 2004 };
            var expectedHatTricks = new List<HatTrickModel>();
            _aggregator.Setup((aggregator) => aggregator.GetHatTricks(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedHatTricks);
            _validator.Setup((validator) => validator.AreFastestLapYearsValid(options)).Returns(true);

            // Act
            var actual = _service.AggregateHatTricks(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedHatTricks.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateHatTricks_ThrowsException_IfSeasonIsInvalid()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedMessage = "Duomenys prieinami nuo 2004 metų.";
            _validator.Setup((validator) => validator.AreFastestLapYearsValid(options)).Returns(false);

            try
            {
                // Act
                var actual = _service.AggregateHatTricks(options);

                // Assert
                Assert.Fail("Exception has to be thrown.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(expectedMessage, ex.Message);
            }
        }

        [TestMethod]
        public void AggregateGrandSlams_ReturnSortedAggregatedGrandSlamsList_IfThereAreAnyDriversWithGrandSlams()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2004, YearTo = 2005 };
            var expectedGrandslams = GenerateGrandSlams();
            expectedGrandslams.Sort((x, y) => y.GrandSlamCount.CompareTo(x.GrandSlamCount));
            _aggregator.Setup((aggregator) => aggregator.GetGrandSlams(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateGrandSlams());
            _validator.Setup((validator) => validator.AreFastestLapYearsValid(options)).Returns(true);

            // Act
            var actual = _service.AggregateGrandSlams(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedGrandslams.Count, actual.Count);

            for (int i = 0; i < expectedGrandslams.Count; i++)
            {
                Assert.AreEqual(expectedGrandslams[i].Name, actual[i].Name);
                Assert.AreEqual(expectedGrandslams[i].GrandSlamCount, actual[i].GrandSlamCount);
            }
        }

        [TestMethod]
        public void AggregateGrandSlams_ReturnEmptyList_IfThereAreNoDriversWithGrandSlams()
        {
            // Arrange
            var options = new OptionsModel { Season = 2004 };
            var expectedGrandSlams = new List<GrandSlamModel>();
            _aggregator.Setup((aggregator) => aggregator.GetGrandSlams(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedGrandSlams);
            _validator.Setup((validator) => validator.AreFastestLapYearsValid(options)).Returns(true);

            // Act
            var actual = _service.AggregateGrandSlams(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedGrandSlams.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateGrandSlams_ThrowsException_IfYearsAreInvalid()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedMessage = "Duomenys prieinami nuo 2004 metų.";
            _validator.Setup((validator) => validator.AreFastestLapYearsValid(options)).Returns(false);

            try
            {
                // Act
                var actual = _service.AggregateGrandSlams(options);

                // Assert
                Assert.Fail("Exception has to be thrown.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(expectedMessage, ex.Message);
            }
        }

        [TestMethod]
        public void AggregateNonFinishers_ReturnSortedAggregatedNonFinishersList_IfThereAreAnyDriversWhoDidNotFinishRace()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedNonFinishers = GenerateNonFinishers();
            expectedNonFinishers.Sort((x, y) => y.TotalDidNotFinishCount.CompareTo(x.TotalDidNotFinishCount));
            expectedNonFinishers.ForEach(model => model.DidNotFinishByYear.Sort((x, y) => x.Year.CompareTo(y.Year)));
            _aggregator.Setup((aggregator) => aggregator.GetNonFinishers(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateNonFinishers());

            // Act
            var actual = _service.AggregateNonFinishers(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedNonFinishers.Count, actual.Count);

            for (int i = 0; i < expectedNonFinishers.Count; i++)
            {
                Assert.AreEqual(expectedNonFinishers[i].Name, actual[i].Name);
                Assert.AreEqual(expectedNonFinishers[i].TotalDidNotFinishCount, actual[i].TotalDidNotFinishCount);
                Assert.AreEqual(expectedNonFinishers[i].DidNotFinishByYear.Count, actual[i].DidNotFinishByYear.Count);

                for (int j = 0; j < expectedNonFinishers[i].DidNotFinishByYear.Count; j++)
                {
                    Assert.AreEqual(expectedNonFinishers[i].DidNotFinishByYear[j].Year, actual[i].DidNotFinishByYear[j].Year);
                    Assert.AreEqual(expectedNonFinishers[i].DidNotFinishByYear[j].YearDidNotFinishCount, actual[i].DidNotFinishByYear[j].YearDidNotFinishCount);
                    Assert.AreEqual(expectedNonFinishers[i].DidNotFinishByYear[j].DidNotFinishInformation.Count, actual[i].DidNotFinishByYear[j].DidNotFinishInformation.Count);

                    for (int k = 0; k < expectedNonFinishers[i].DidNotFinishByYear[j].DidNotFinishInformation.Count; k++)
                    {
                        Assert.AreEqual(expectedNonFinishers[i].DidNotFinishByYear[j].DidNotFinishInformation[k].CircuitName, actual[i].DidNotFinishByYear[j].DidNotFinishInformation[k].CircuitName);
                        Assert.AreEqual(expectedNonFinishers[i].DidNotFinishByYear[j].DidNotFinishInformation[k].LapsCompleted, actual[i].DidNotFinishByYear[j].DidNotFinishInformation[k].LapsCompleted);
                    }
                }
            }
        }

        [TestMethod]
        public void AggregateNonFinisherss_ReturnEmptyList_IfAllDriversFinishedEveryRace()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedNonFinishers = new List<DidNotFinishModel>();
            _aggregator.Setup((aggregator) => aggregator.GetNonFinishers(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedNonFinishers);

            // Act
            var actual = _service.AggregateNonFinishers(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedNonFinishers.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateSeasonPositionChanges_ReturnSortedAggregatedSeasonPositionChangesList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedSeasonPositionChanges = GenerateSeasonPositionChanges();
            expectedSeasonPositionChanges.ForEach(season => season.PositionChanges.Sort((x, y) => y.TotalPositionChange.CompareTo(x.TotalPositionChange)));
            expectedSeasonPositionChanges.Sort((x, y) => x.Year.CompareTo(y.Year));
            _aggregator.Setup((aggregator) => aggregator.GetSeasonPositionChanges(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateSeasonPositionChanges());

            // Act
            var actual = _service.AggregateSeasonPositionChanges(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedSeasonPositionChanges.Count, actual.Count);

            for (int i = 0; i < expectedSeasonPositionChanges.Count; i++)
            {
                Assert.AreEqual(expectedSeasonPositionChanges[i].Year, actual[i].Year);
                Assert.AreEqual(expectedSeasonPositionChanges[i].PositionChanges.Count, actual[i].PositionChanges.Count);

                for (int j = 0; j < expectedSeasonPositionChanges[i].PositionChanges.Count; j++)
                {
                    Assert.AreEqual(expectedSeasonPositionChanges[i].PositionChanges[j].Name, actual[i].PositionChanges[j].Name);
                    Assert.AreEqual(expectedSeasonPositionChanges[i].PositionChanges[j].TotalPositionChange, actual[i].PositionChanges[j].TotalPositionChange);
                    Assert.AreEqual(expectedSeasonPositionChanges[i].PositionChanges[j].ChampionshipPosition, actual[i].PositionChanges[j].ChampionshipPosition);
                    Assert.AreEqual(expectedSeasonPositionChanges[i].PositionChanges[j].DriverPositionChangeInformation.Count, actual[i].PositionChanges[j].DriverPositionChangeInformation.Count);

                    for (int k = 0; k < expectedSeasonPositionChanges[i].PositionChanges[j].DriverPositionChangeInformation.Count; k++)
                    {
                        Assert.AreEqual(expectedSeasonPositionChanges[i].PositionChanges[j].DriverPositionChangeInformation[k].CircuitName, actual[i].PositionChanges[j].DriverPositionChangeInformation[k].CircuitName);
                        Assert.AreEqual(expectedSeasonPositionChanges[i].PositionChanges[j].DriverPositionChangeInformation[k].RacePositionChange, actual[i].PositionChanges[j].DriverPositionChangeInformation[k].RacePositionChange);
                    }
                }
            }
        }

        [TestMethod]
        public void AggregateSeasonPositionChanges_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedSeasonPositionChanges = new List<SeasonPositionChangesModel>();
            _aggregator.Setup((aggregator) => aggregator.GetSeasonPositionChanges(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedSeasonPositionChanges);

            // Act
            var actual = _service.AggregateSeasonPositionChanges(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedSeasonPositionChanges.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateConstructorsFrontRows_ReturnSortedAggregatedConstructorsFrontRowsCountList_IfThereAreAnyConstructorsWithFrontRows()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedConstructorsFrontRows = GenerateConstructorsFrontRows();
            expectedConstructorsFrontRows.Sort((x, y) => y.TotalFrontRowCount.CompareTo(x.TotalFrontRowCount));
            _aggregator.Setup((aggregator) => aggregator.GetConstructorsFrontRows(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateConstructorsFrontRows());

            // Act
            var actual = _service.AggregateConstructorsFrontRows(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedConstructorsFrontRows.Count, actual.Count);

            for (int i = 0; i < expectedConstructorsFrontRows.Count; i++)
            {
                Assert.AreEqual(expectedConstructorsFrontRows[i].Name, actual[i].Name);
                Assert.AreEqual(expectedConstructorsFrontRows[i].TotalFrontRowCount, actual[i].TotalFrontRowCount);
                Assert.AreEqual(expectedConstructorsFrontRows[i].FrontRowInformation.Count, actual[i].FrontRowInformation.Count);

                for (int k = 0; k < expectedConstructorsFrontRows[i].FrontRowInformation.Count; k++)
                {
                    Assert.AreEqual(expectedConstructorsFrontRows[i].FrontRowInformation[k].CircuitName, actual[i].FrontRowInformation[k].CircuitName);
                    Assert.AreEqual(expectedConstructorsFrontRows[i].FrontRowInformation[k].CircuitFrontRowCount, actual[i].FrontRowInformation[k].CircuitFrontRowCount);
                }
            }
        }

        [TestMethod]
        public void AggregateConstructorsFrontRows_ReturnEmptyList_IfThereAreNoConstructorsWithFrontRows()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedConstructorsFrontRows = new List<FrontRowModel>();
            _aggregator.Setup((aggregator) => aggregator.GetConstructorsFrontRows(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedConstructorsFrontRows);

            // Act
            var actual = _service.AggregateConstructorsFrontRows(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedConstructorsFrontRows.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateDriversFinishingPositions_ReturnSortedAggregatedDriversFinishingPositionsList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedDriversFinishingPositions = GenerateDriversFinishingPositions();
            expectedDriversFinishingPositions[1].FinishingPositions.Add(new FinishingPositionModel { FinishingPosition = 1, FinishingPositionInformation = new List<FinishingPositionInformationModel>() });
            expectedDriversFinishingPositions.ForEach(driver => driver.FinishingPositions.Sort((x, y) => x.FinishingPosition.CompareTo(y.FinishingPosition)));
            expectedDriversFinishingPositions.Sort((x, y) => x.Name.CompareTo(y.Name));
            _aggregator.Setup((aggregator) => aggregator.GetDriversFinishingPositions(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateDriversFinishingPositions());

            // Act
            var actual = _service.AggregateDriversFinishingPositions(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedDriversFinishingPositions.Count, actual.Count);

            for (int i = 0; i < expectedDriversFinishingPositions.Count; i++)
            {
                Assert.AreEqual(expectedDriversFinishingPositions[i].Name, actual[i].Name);
                Assert.AreEqual(expectedDriversFinishingPositions[i].FinishingPositions.Count, actual[i].FinishingPositions.Count);

                for (int j = 0; j < expectedDriversFinishingPositions[i].FinishingPositions.Count; j++)
                {
                    Assert.AreEqual(expectedDriversFinishingPositions[i].FinishingPositions[j].FinishingPosition, actual[i].FinishingPositions[j].FinishingPosition);
                    Assert.AreEqual(expectedDriversFinishingPositions[i].FinishingPositions[j].Count, actual[i].FinishingPositions[j].Count);
                    Assert.AreEqual(expectedDriversFinishingPositions[i].FinishingPositions[j].FinishingPositionInformation.Count, actual[i].FinishingPositions[j].FinishingPositionInformation.Count);

                    for (int k = 0; k < expectedDriversFinishingPositions[i].FinishingPositions[j].FinishingPositionInformation.Count; k++)
                    {
                        Assert.AreEqual(expectedDriversFinishingPositions[i].FinishingPositions[j].FinishingPositionInformation[k].CircuitName, actual[i].FinishingPositions[j].FinishingPositionInformation[k].CircuitName);
                        Assert.AreEqual(expectedDriversFinishingPositions[i].FinishingPositions[j].FinishingPositionInformation[k].FinishedRace, actual[i].FinishingPositions[j].FinishingPositionInformation[k].FinishedRace);
                    }
                }
            }
        }

        [TestMethod]
        public void AggregateDriversFinishingPositions_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedDriversFinishingPositions = new List<DriverFinishingPositionsModel>();
            _aggregator.Setup((aggregator) => aggregator.GetDriversFinishingPositions(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedDriversFinishingPositions);

            // Act
            var actual = _service.AggregateDriversFinishingPositions(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedDriversFinishingPositions.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateDriversStandingsChanges_ReturnSortedAggregatedDriversStandingsChangesList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedDriversStandingsChanges = GenerateSeasonStandingsChanges();
            expectedDriversStandingsChanges.Sort((x, y) => x.Year.CompareTo(y.Year));
            expectedDriversStandingsChanges.ForEach(model => model.Standings.ForEach(standing => standing.Rounds.Sort((x, y) => x.Round.CompareTo(y.Round))));
            _aggregator.Setup((aggregator) => aggregator.GetDriversStandingsChanges(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateSeasonStandingsChanges());

            // Act
            var actual = _service.AggregateDriversStandingsChanges(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedDriversStandingsChanges.Count, actual.Count);

            for (int i = 0; i < expectedDriversStandingsChanges.Count; i++)
            {
                Assert.AreEqual(expectedDriversStandingsChanges[i].Year, actual[i].Year);
                Assert.AreEqual(expectedDriversStandingsChanges[i].Standings.Count, actual[i].Standings.Count);

                for (int j = 0; j < expectedDriversStandingsChanges[i].Standings.Count; j++)
                {
                    Assert.AreEqual(expectedDriversStandingsChanges[i].Standings[j].Name, actual[i].Standings[j].Name);
                    Assert.AreEqual(expectedDriversStandingsChanges[i].Standings[j].Rounds.Count, actual[i].Standings[j].Rounds.Count);

                    for (int k = 0; k < expectedDriversStandingsChanges[i].Standings[j].Rounds.Count; k++)
                    {
                        Assert.AreEqual(expectedDriversStandingsChanges[i].Standings[j].Rounds[k].Round, actual[i].Standings[j].Rounds[k].Round);
                        Assert.AreEqual(expectedDriversStandingsChanges[i].Standings[j].Rounds[k].Position, actual[i].Standings[j].Rounds[k].Position);
                    }
                }
            }
        }

        [TestMethod]
        public void AggregateDriversStandingsChanges_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedDriversStandingsChanges = new List<SeasonStandingsChangesModel>();
            _aggregator.Setup((aggregator) => aggregator.GetDriversStandingsChanges(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedDriversStandingsChanges);

            // Act
            var actual = _service.AggregateDriversStandingsChanges(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedDriversStandingsChanges.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateConstructorsStandingsChanges_ReturnSortedAggregatedConstructorsStandingsChangesList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedConstructorsStandingsChanges = GenerateSeasonStandingsChanges();
            expectedConstructorsStandingsChanges.Sort((x, y) => x.Year.CompareTo(y.Year));
            expectedConstructorsStandingsChanges.ForEach(model => model.Standings.ForEach(standing => standing.Rounds.Sort((x, y) => x.Round.CompareTo(y.Round))));
            _aggregator.Setup((aggregator) => aggregator.GetConstructorsStandingsChanges(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateSeasonStandingsChanges());

            // Act
            var actual = _service.AggregateConstructorsStandingsChanges(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedConstructorsStandingsChanges.Count, actual.Count);

            for (int i = 0; i < expectedConstructorsStandingsChanges.Count; i++)
            {
                Assert.AreEqual(expectedConstructorsStandingsChanges[i].Year, actual[i].Year);
                Assert.AreEqual(expectedConstructorsStandingsChanges[i].Standings.Count, actual[i].Standings.Count);

                for (int j = 0; j < expectedConstructorsStandingsChanges[i].Standings.Count; j++)
                {
                    Assert.AreEqual(expectedConstructorsStandingsChanges[i].Standings[j].Name, actual[i].Standings[j].Name);
                    Assert.AreEqual(expectedConstructorsStandingsChanges[i].Standings[j].Rounds.Count, actual[i].Standings[j].Rounds.Count);

                    for (int k = 0; k < expectedConstructorsStandingsChanges[i].Standings[j].Rounds.Count; k++)
                    {
                        Assert.AreEqual(expectedConstructorsStandingsChanges[i].Standings[j].Rounds[k].Round, actual[i].Standings[j].Rounds[k].Round);
                        Assert.AreEqual(expectedConstructorsStandingsChanges[i].Standings[j].Rounds[k].Position, actual[i].Standings[j].Rounds[k].Position);
                    }
                }
            }
        }

        [TestMethod]
        public void AggregateConstructorsStandingsChanges_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedConstructorsStandingsChanges = new List<SeasonStandingsChangesModel>();
            _aggregator.Setup((aggregator) => aggregator.GetConstructorsStandingsChanges(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedConstructorsStandingsChanges);

            // Act
            var actual = _service.AggregateConstructorsStandingsChanges(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedConstructorsStandingsChanges.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateDriversPositionChangesDuringRace_ReturnSortedAggregatedDriversPositionChangesDuringRaceList_IfThereAreAnyDrivers()
        {
            // Arrange
            var season = 2020;
            var race = 1;
            var expectedDriversPositionChangesDuringRace = GenerateDriversPositionChangesDuringRace();
            expectedDriversPositionChangesDuringRace.Sort((x, y) => x.Name.CompareTo(y.Name));
            expectedDriversPositionChangesDuringRace.ForEach(driver => driver.Laps.Sort((x, y) => x.LapNumber.CompareTo(y.LapNumber)));
            _aggregator.Setup((aggregator) => aggregator.GetDriversPositionChangesDuringRace(season, race)).Returns(GenerateDriversPositionChangesDuringRace());
            _validator.Setup((validator) => validator.IsLapTimesSeasonValid(season)).Returns(true);

            // Act
            var actual = _service.AggregateDriversPositionChangesDuringRace(season, race);

            // Assert
            Assert.AreEqual(expectedDriversPositionChangesDuringRace.Count, actual.Count);

            for (int i = 0; i < expectedDriversPositionChangesDuringRace.Count; i++)
            {
                Assert.AreEqual(expectedDriversPositionChangesDuringRace[i].Name, actual[i].Name);
                Assert.AreEqual(expectedDriversPositionChangesDuringRace[i].Laps.Count, actual[i].Laps.Count);

                for (int j = 0; j < expectedDriversPositionChangesDuringRace[i].Laps.Count; j++)
                {
                    Assert.AreEqual(expectedDriversPositionChangesDuringRace[i].Laps[j].LapNumber, actual[i].Laps[j].LapNumber);
                    Assert.AreEqual(expectedDriversPositionChangesDuringRace[i].Laps[j].Position, actual[i].Laps[j].Position);
                }
            }
        }

        [TestMethod]
        public void AggregateDriversPositionChangesDuringRace_ThrowException_IfSeasonIsInvalid()
        {
            // Arrange
            var season = 1;
            var race = 1;
            var expectedMessage = "Duomenys prieinami nuo 1996 metų.";
            _validator.Setup((validator) => validator.IsLapTimesSeasonValid(season)).Returns(false);

            try
            {
                // Act
                var actual = _service.AggregateDriversPositionChangesDuringRace(season, race);

                // Assert
                Assert.Fail("Exception has to be thrown.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(expectedMessage, ex.Message);
            }
        }

        [TestMethod]
        public void AggregateDriversLapTimes_ReturnSortedAggregatedDriversLapTimesList_IfThereAreAnyDrivers()
        {
            // Arrange
            var season = 2020;
            var race = 1;
            var expectedDriversLapTimes = GenerateDriversLapTimes();
            expectedDriversLapTimes.Sort((x, y) => x.Name.CompareTo(y.Name));
            expectedDriversLapTimes.ForEach(model => model.Timings.Sort((x, y) => x.CompareTo(y)));
            _aggregator.Setup((aggregator) => aggregator.GetDriversLapTimes(season, race)).Returns(GenerateDriversLapTimes());
            _validator.Setup((validator) => validator.IsLapTimesSeasonValid(season)).Returns(true);

            // Act
            var actual = _service.AggregateDriversLapTimes(season, race);

            // Assert
            Assert.AreEqual(expectedDriversLapTimes.Count, actual.Count);

            for (int i = 0; i < expectedDriversLapTimes.Count; i++)
            {
                Assert.AreEqual(expectedDriversLapTimes[i].Name, actual[i].Name);
                Assert.AreEqual(expectedDriversLapTimes[i].Timings.Count, actual[i].Timings.Count);

                for (int j = 0; j < expectedDriversLapTimes[i].Timings.Count; j++)
                {
                    Assert.AreEqual(expectedDriversLapTimes[i].Timings[j], actual[i].Timings[j]);
                }
            }
        }

        [TestMethod]
        public void AggregateDriversLapTimes_ThrowsException_IfSeasonIsInvalid()
        {
            // Arrange
            var season = 1995;
            var race = 1;
            var expectedMessage = "Duomenys prieinami nuo 1996 metų.";
            _validator.Setup((validator) => validator.IsLapTimesSeasonValid(season)).Returns(false);

            // Act
            try
            {
                var actual = _service.AggregateDriversLapTimes(season, race);

                // Assert
                Assert.Fail("Exception has to be thrown.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(expectedMessage, ex.Message);
            }
        }
    }
}
