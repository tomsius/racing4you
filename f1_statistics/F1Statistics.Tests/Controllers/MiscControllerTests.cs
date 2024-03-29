﻿using F1Statistics.Controllers;
using F1Statistics.Library.Models;
using F1Statistics.Library.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Statistics.Tests.Controllers
{
    [TestClass]
    public class MiscControllerTests
    {
        private MiscController _controller;
        private Mock<IMiscService> _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new Mock<IMiscService>();

            _controller = new MiscController(_service.Object);
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
        public void GetRaceCountPerSeason_ReturnAggregatedRaceCountPerSeasonList_IfThereAreAnyRaces()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedSeasonRaces = GenerateSeasonRaces();
            _service.Setup((service) => service.AggregateRaceCountPerSeason(It.IsAny<OptionsModel>())).Returns(expectedSeasonRaces);

            // Act
            var actual = _controller.GetRaceCountPerSeason(options);

            // Assert
            Assert.AreEqual(expectedSeasonRaces.Count, actual.Count);

            for (int i = 0; i < expectedSeasonRaces.Count; i++)
            {
                Assert.AreEqual(expectedSeasonRaces[i].Season, actual[i].Season);
                Assert.AreEqual(expectedSeasonRaces[i].RaceCount, actual[i].RaceCount);
            }
        }

        [TestMethod]
        public void GetRaceCountPerSeason_ReturnEmptyList_IfThereAreNoRaces()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedSeasonRaces = new List<SeasonRacesModel>();
            _service.Setup((service) => service.AggregateRaceCountPerSeason(It.IsAny<OptionsModel>())).Returns(expectedSeasonRaces);

            // Act
            var actual = _controller.GetRaceCountPerSeason(options);

            // Assert
            Assert.AreEqual(expectedSeasonRaces.Count, actual.Count);
        }

        [TestMethod]
        public void GetHatTricks_ReturnAggregatedHatTricksList_IfThereAreAnyDriversWithHatTricks()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedHatTricks = GenerateHatTricks();
            _service.Setup((service) => service.AggregateHatTricks(It.IsAny<OptionsModel>())).Returns(expectedHatTricks);

            // Act
            var actual = _controller.GetHatTricks(options).Value;

            // Assert
            Assert.AreEqual(expectedHatTricks.Count, actual.Count);

            for (int i = 0; i < expectedHatTricks.Count; i++)
            {
                Assert.AreEqual(expectedHatTricks[i].Name, actual[i].Name);
                Assert.AreEqual(expectedHatTricks[i].HatTrickCount, actual[i].HatTrickCount);
            }
        }

        [TestMethod]
        public void GetHatTricks_ReturnEmptyList_IfThereAreNoDriversWithHatTricks()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedHatTricks = new List<HatTrickModel>();
            _service.Setup((service) => service.AggregateHatTricks(It.IsAny<OptionsModel>())).Returns(expectedHatTricks);

            // Act
            var actual = _controller.GetHatTricks(options).Value;

            // Assert
            Assert.AreEqual(expectedHatTricks.Count, actual.Count);
        }

        [TestMethod]
        public void GetHatTricks_ReturnBadRequest_IfExceptionIsRaised()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedMessage = "Duomenys prieinami nuo 2004 metų.";
            var expectedStatusCode = 400;
            _service.Setup((service) => service.AggregateHatTricks(It.IsAny<OptionsModel>())).Throws(new ArgumentException("Duomenys prieinami nuo 2004 metų."));

            // Act
            var actual = (BadRequestObjectResult)_controller.GetHatTricks(options).Result;

            // Assert
            Assert.AreEqual(expectedMessage, actual.Value);
            Assert.AreEqual(expectedStatusCode, actual.StatusCode);
        }

        [TestMethod]
        public void GetGrandSlams_ReturnAggregatedGrandSlamsList_IfThereAreAnyDriversWithGrandSlams()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedGrandSlams = GenerateGrandSlams();
            _service.Setup((service) => service.AggregateGrandSlams(It.IsAny<OptionsModel>())).Returns(expectedGrandSlams);

            // Act
            var actual = _controller.GetGrandSlams(options).Value;

            // Assert
            Assert.AreEqual(expectedGrandSlams.Count, actual.Count);

            for (int i = 0; i < expectedGrandSlams.Count; i++)
            {
                Assert.AreEqual(expectedGrandSlams[i].Name, actual[i].Name);
                Assert.AreEqual(expectedGrandSlams[i].GrandSlamCount, actual[i].GrandSlamCount);
            }
        }

        [TestMethod]
        public void GetGrandSlams_ReturnEmptyList_IfThereAreNoDriversWithGrandSlams()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedGrandSlams = new List<GrandSlamModel>();
            _service.Setup((service) => service.AggregateGrandSlams(It.IsAny<OptionsModel>())).Returns(expectedGrandSlams);

            // Act
            var actual = _controller.GetGrandSlams(options).Value;

            // Assert
            Assert.AreEqual(expectedGrandSlams.Count, actual.Count);
        }

        [TestMethod]
        public void GetGrandSlams_ReturnBadRequest_IfExceptionIsRaised()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedMessage = "Duomenys prieinami nuo 2004 metų.";
            var expectedStatusCode = 400;
            _service.Setup((service) => service.AggregateGrandSlams(It.IsAny<OptionsModel>())).Throws(new ArgumentException("Duomenys prieinami nuo 2004 metų."));

            // Act
            var actual = (BadRequestObjectResult)_controller.GetGrandSlams(options).Result;

            // Assert
            Assert.AreEqual(expectedMessage, actual.Value);
            Assert.AreEqual(expectedStatusCode, actual.StatusCode);
        }

        [TestMethod]
        public void GetNonFinishers_ReturnAggregatedNonFinishersList_IfThereAreAnyDriversWhoDidNotFinishRace()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedNonFinishers = GenerateNonFinishers();
            _service.Setup((service) => service.AggregateNonFinishers(It.IsAny<OptionsModel>())).Returns(expectedNonFinishers);

            // Act
            var actual = _controller.GetNonFinishers(options);

            // Assert
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
        public void GetNonFinishers_ReturnEmptyList_IfAllDriversFinishedEveryRace()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedNonFinishgers = new List<DidNotFinishModel>();
            _service.Setup((service) => service.AggregateNonFinishers(It.IsAny<OptionsModel>())).Returns(expectedNonFinishgers);

            // Act
            var actual = _controller.GetNonFinishers(options);

            // Assert
            Assert.AreEqual(expectedNonFinishgers.Count, actual.Count);
        }

        [TestMethod]
        public void GetPositionChanges_ReturnAggregatedSeasonPositionChangesList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedSeasonPositionChanges = GenerateSeasonPositionChanges();
            _service.Setup((service) => service.AggregateSeasonPositionChanges(It.IsAny<OptionsModel>())).Returns(expectedSeasonPositionChanges);

            // Act
            var actual = _controller.GetSeasonPositionChanges(options);

            // Assert
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
        public void GetSeasonPositionChanges_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedSeasonPositionChanges = new List<SeasonPositionChangesModel>();
            _service.Setup((service) => service.AggregateSeasonPositionChanges(It.IsAny<OptionsModel>())).Returns(expectedSeasonPositionChanges);

            // Act
            var actual = _controller.GetSeasonPositionChanges(options);

            // Assert
            Assert.AreEqual(expectedSeasonPositionChanges.Count, actual.Count);
        }

        [TestMethod]
        public void GetConstructorsFrontRows_ReturnAggregatedConstructorsfrontRowsCountList_IfThereAreAnyConstructorWithFrontRows()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedConstructorsFrontRows = GenerateConstructorsFrontRows();
            _service.Setup((service) => service.AggregateConstructorsFrontRows(It.IsAny<OptionsModel>())).Returns(expectedConstructorsFrontRows);

            // Act
            var actual = _controller.GetConstructorsFrontRows(options);

            // Assert
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
        public void GetConstructorsFrontRows_ReturnEmptyList_IfThereAreNoConstructorsWithFrontRows()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedConstructorsFrontRows = new List<FrontRowModel>();
            _service.Setup((service) => service.AggregateConstructorsFrontRows(It.IsAny<OptionsModel>())).Returns(expectedConstructorsFrontRows);

            // Act
            var actual = _controller.GetConstructorsFrontRows(options);

            // Assert
            Assert.AreEqual(expectedConstructorsFrontRows.Count, actual.Count);
        }

        [TestMethod]
        public void GetDriversFinishingPositions_ReturnAggregatedDriversFinishingPositionsList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedDriversFinishingPositions = GenerateDriversFinishingPositions();
            _service.Setup((service) => service.AggregateDriversFinishingPositions(It.IsAny<OptionsModel>())).Returns(expectedDriversFinishingPositions);

            // Act
            var actual = _controller.GetDriversFinishingPositions(options);

            // Assert
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
        public void GetDriversFinishingPositions_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedDriversFinishingPositions = new List<DriverFinishingPositionsModel>();
            _service.Setup((service) => service.AggregateDriversFinishingPositions(It.IsAny<OptionsModel>())).Returns(expectedDriversFinishingPositions);

            // Act
            var actual = _controller.GetDriversFinishingPositions(options);

            // Assert
            Assert.AreEqual(expectedDriversFinishingPositions.Count, actual.Count);
        }

        [TestMethod]
        public void GetDriversStandingsChanges_ReturnAggregatedDriversStandingsChangesList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedDriversStandingsChanges = GenerateSeasonStandingsChanges();
            _service.Setup((service) => service.AggregateDriversStandingsChanges(It.IsAny<OptionsModel>())).Returns(expectedDriversStandingsChanges);

            // Act
            var actual = _controller.GetDriversStandingsChanges(options);

            // Assert
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
        public void GetDriversStandingsChanges_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedDriversStandingsChanges = new List<SeasonStandingsChangesModel>();
            _service.Setup((service) => service.AggregateDriversStandingsChanges(It.IsAny<OptionsModel>())).Returns(expectedDriversStandingsChanges);

            // Act
            var actual = _controller.GetDriversStandingsChanges(options);

            // Assert
            Assert.AreEqual(expectedDriversStandingsChanges.Count, actual.Count);
        }

        [TestMethod]
        public void GetConstructorsStandingsChanges_ReturnAggregatedConstructorsStandingsChangesList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedConstructorsStandingsChanges = GenerateSeasonStandingsChanges();
            _service.Setup((service) => service.AggregateConstructorsStandingsChanges(It.IsAny<OptionsModel>())).Returns(expectedConstructorsStandingsChanges);

            // Act
            var actual = _controller.GetConstructorsStandingsChanges(options);

            // Assert
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
        public void GetConstructorsStandingsChanges_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedConstructorsStandingsChanges = new List<SeasonStandingsChangesModel>();
            _service.Setup((service) => service.AggregateConstructorsStandingsChanges(It.IsAny<OptionsModel>())).Returns(expectedConstructorsStandingsChanges);

            // Act
            var actual = _controller.GetConstructorsStandingsChanges(options);

            // Assert
            Assert.AreEqual(expectedConstructorsStandingsChanges.Count, actual.Count);
        }

        [TestMethod]
        public void GetDriversPositionChangesDuringRace_ReturnAggregatedDriversPositionChangesDuringRaceList_IfThereAreAnyDrivers()
        {
            // Arrange
            var season = 1;
            var race = 1;
            var expectedDriversPositionChangesDuringRace = GenerateDriversPositionChangesDuringRace();
            _service.Setup((service) => service.AggregateDriversPositionChangesDuringRace(season, race)).Returns(expectedDriversPositionChangesDuringRace);

            // Act
            var actual = _controller.GetDriversPositionChangesDuringRace(season, race).Value;

            // Assert
            Assert.AreEqual(expectedDriversPositionChangesDuringRace.Count, actual.Count);

            for(int i = 0; i < expectedDriversPositionChangesDuringRace.Count; i++)
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
        public void GetDriversPositionChangesDuringRace_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var season = 1;
            var race = 1;
            var expectedDriversPositionChangesDuringRace = new List<RacePositionChangesModel>();
            _service.Setup((service) => service.AggregateDriversPositionChangesDuringRace(season, race)).Returns(expectedDriversPositionChangesDuringRace);

            // Act
            var actual = _controller.GetDriversPositionChangesDuringRace(season, race).Value;

            // Assert
            Assert.AreEqual(expectedDriversPositionChangesDuringRace.Count, actual.Count);
        }

        [TestMethod]
        public void GetDriversPositionChangesDuringRace_ReturnBadRequest_IfExceptionIsRaised()
        {
            // Arrange
            var season = 1;
            var race = 1;
            var expectedMessage = "Duomenys prieinami nuo 1996 metų.";
            var expectedStatusCode = 400;
            _service.Setup((service) => service.AggregateDriversPositionChangesDuringRace(It.IsAny<int>(), It.IsAny<int>())).Throws(new ArgumentException("Duomenys prieinami nuo 1996 metų."));

            // Act
            var actual = (BadRequestObjectResult)_controller.GetDriversPositionChangesDuringRace(season, race).Result;

            // Assert
            Assert.AreEqual(expectedMessage, actual.Value);
            Assert.AreEqual(expectedStatusCode, actual.StatusCode);
        }

        [TestMethod]
        public void GetDriversLapTimes_ReturnAggregatedDriversLapTimesList_IfThereAreAnyDrivers()
        {
            // Arrange
            var season = 1;
            var race = 1;
            var expectedDriversLapTimes = GenerateDriversLapTimes();
            _service.Setup((service) => service.AggregateDriversLapTimes(season, race)).Returns(expectedDriversLapTimes);

            // Act
            var actual = _controller.GetDriversLapTimes(season, race).Value;

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
        public void GetDriversLapTimes_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var season = 1;
            var race = 1;
            var expectedDriversLapTimes = new List<LapTimesModel>();
            _service.Setup((service) => service.AggregateDriversLapTimes(season, race)).Returns(expectedDriversLapTimes);

            // Act
            var actual = _controller.GetDriversLapTimes(season, race).Value;

            // Assert
            Assert.AreEqual(expectedDriversLapTimes.Count, actual.Count);
        }

        [TestMethod]
        public void GetDriversLapTimes_ReturnBadRequest_IfExceptionIsRaised()
        {
            // Arrange
            var season = 1;
            var race = 1;
            var expectedMessage = "Duomenys prieinami nuo 1996 metų.";
            var expectedStatusCode = 400;
            _service.Setup((service) => service.AggregateDriversLapTimes(It.IsAny<int>(), It.IsAny<int>())).Throws(new ArgumentException("Duomenys prieinami nuo 1996 metų."));

            // Act
            var actual = (BadRequestObjectResult)_controller.GetDriversLapTimes(season, race).Result;

            // Assert
            Assert.AreEqual(expectedMessage, actual.Value);
            Assert.AreEqual(expectedStatusCode, actual.StatusCode);
        }
    }
}
