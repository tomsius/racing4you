﻿using F1Statistics.Library.DataAccess.Interfaces;
using F1Statistics.Library.DataAggregation;
using F1Statistics.Library.Models;
using F1Statistics.Library.Models.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Statistics.Library.Tests.DataAggregation
{
    [TestClass]
    public class LeadingLapsAggregatorTests
    {
        private LeadingLapsAggregator _aggregator;
        private Mock<IResultsDataAccess> _resultsDataAccess;
        private Mock<ILapsDataAccess> _lapsDataAccess;
        private Mock<IDriversDataAccess> _driversDataAccess;
        private Mock<IConstructorsDataAccess> _constructorsDataAccess;

        [TestInitialize]
        public void Setup()
        {
            _resultsDataAccess = new Mock<IResultsDataAccess>();
            _lapsDataAccess = new Mock<ILapsDataAccess>();
            _driversDataAccess = new Mock<IDriversDataAccess>();
            _constructorsDataAccess = new Mock<IConstructorsDataAccess>();

            _driversDataAccess.Setup((driversDataAccess) => driversDataAccess.GetDriverName("1")).Returns("FirstName FirstFamily");
            _driversDataAccess.Setup((driversDataAccess) => driversDataAccess.GetDriverName("2")).Returns("SecondName SecondFamily");
            _driversDataAccess.Setup((driversDataAccess) => driversDataAccess.GetDriverName("3")).Returns("ThirdName ThirdFamily");

            _constructorsDataAccess.Setup((constructorsDataAccess) => constructorsDataAccess.GetDriverConstructor(It.IsAny<int>(), It.IsAny<int>(), "1")).Returns("FirstConstructor");
            _constructorsDataAccess.Setup((constructorsDataAccess) => constructorsDataAccess.GetDriverConstructor(It.IsAny<int>(), It.IsAny<int>(), "2")).Returns("SecondConstructor");
            _constructorsDataAccess.Setup((constructorsDataAccess) => constructorsDataAccess.GetDriverConstructor(It.IsAny<int>(), It.IsAny<int>(), "3")).Returns("ThirdConstructor");
            
            _lapsDataAccess.Setup((lapsDataAccess) => lapsDataAccess.GetLapsFrom(1, 1)).Returns(GenerateLaps()[0]);
            _lapsDataAccess.Setup((lapsDataAccess) => lapsDataAccess.GetLapsFrom(1, 2)).Returns(GenerateLaps()[1]);
            _lapsDataAccess.Setup((lapsDataAccess) => lapsDataAccess.GetLapsFrom(1, 3)).Returns(GenerateLaps()[2]);
            _lapsDataAccess.Setup((lapsDataAccess) => lapsDataAccess.GetLapsFrom(2, 1)).Returns(GenerateLaps()[0]);
            _lapsDataAccess.Setup((lapsDataAccess) => lapsDataAccess.GetLapsFrom(2, 2)).Returns(GenerateLaps()[0]);
            _lapsDataAccess.Setup((lapsDataAccess) => lapsDataAccess.GetLapsFrom(2, 3)).Returns(GenerateLaps()[1]);

            _aggregator = new LeadingLapsAggregator(_resultsDataAccess.Object, _lapsDataAccess.Object, _driversDataAccess.Object, _constructorsDataAccess.Object);
        }

        private List<List<RacesDataResponse>> GenerateRaces()
        {
            var racesList = new List<List<RacesDataResponse>>
            {
                new List<RacesDataResponse>
                {
                    new RacesDataResponse(),
                    new RacesDataResponse(),
                    new RacesDataResponse()
                },
                new List<RacesDataResponse>
                {
                    new RacesDataResponse(),
                    new RacesDataResponse(),
                    new RacesDataResponse()
                }
            };

            return racesList;
        }

        private List<List<LapsDataResponse>> GenerateLaps()
        {
            var lapsList = new List<List<LapsDataResponse>>
            {
                new List<LapsDataResponse>
                {
                    new LapsDataResponse
                    {
                        Timings = new List<TimingsDataResponse>
                        {
                            new TimingsDataResponse
                            {
                                driverId = "1"
                            }
                        }
                    }
                },
                new List<LapsDataResponse>
                {
                    new LapsDataResponse
                    {
                        Timings = new List<TimingsDataResponse>
                        {
                            new TimingsDataResponse
                            {
                                driverId = "2"
                            }
                        }
                    }
                },
                new List<LapsDataResponse>
                {
                    new LapsDataResponse
                    {
                        Timings = new List<TimingsDataResponse>
                        {
                            new TimingsDataResponse
                            {
                                driverId = "3"
                            }
                        }
                    }
                }
            };

            return lapsList;
        }

        [TestMethod]
        public void GetDriversLeadingLapsCount_ReturnAggregatedDriversLeadingLapsCountList_IfThereAreAnyDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedDriversLeadingLapsCount = new List<LeadingLapsModel> 
            {
                new LeadingLapsModel 
                {
                    Name = "FirstName FirstFamily", 
                    LeadingLapsByYear = new List<LeadingLapsByYearModel>
                    {
                        new LeadingLapsByYearModel
                        {
                            Year = 1,
                            LeadingLapCount = 1
                        },
                        new LeadingLapsByYearModel
                        {
                            Year = 2,
                            LeadingLapCount = 2
                        }
                    }
                },
                new LeadingLapsModel 
                {
                    Name = "SecondName SecondFamily",
                    LeadingLapsByYear = new List<LeadingLapsByYearModel>
                    {
                        new LeadingLapsByYearModel
                        {
                            Year = 1,
                            LeadingLapCount = 1
                        },
                        new LeadingLapsByYearModel
                        {
                            Year = 2,
                            LeadingLapCount = 1
                        }
                    }
                },
                new LeadingLapsModel 
                {
                    Name = "ThirdName ThirdFamily",
                    LeadingLapsByYear = new List<LeadingLapsByYearModel>
                    {
                        new LeadingLapsByYearModel
                        {
                            Year = 1,
                            LeadingLapCount = 1
                        }
                    }
                }
            };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetDriversLeadingLapsCount(from, to);
                actual.Sort((x, y) => y.LeadingLapCount.CompareTo(x.LeadingLapCount));
                actual.ForEach(model => model.LeadingLapsByYear.Sort((x, y) => x.Year.CompareTo(y.Year)));

                // Assert
                Assert.AreEqual(expectedDriversLeadingLapsCount.Count, actual.Count);

                for (int i = 0; i < expectedDriversLeadingLapsCount.Count; i++)
                {
                    Assert.AreEqual(expectedDriversLeadingLapsCount[i].Name, actual[i].Name);
                    Assert.AreEqual(expectedDriversLeadingLapsCount[i].LeadingLapCount, actual[i].LeadingLapCount);
                    Assert.AreEqual(expectedDriversLeadingLapsCount[i].LeadingLapsByYear.Count, actual[i].LeadingLapsByYear.Count);

                    for (int j = 0; j < expectedDriversLeadingLapsCount[i].LeadingLapsByYear.Count; j++)
                    {
                        Assert.AreEqual(expectedDriversLeadingLapsCount[i].LeadingLapsByYear[j].Year, actual[i].LeadingLapsByYear[j].Year);
                        Assert.AreEqual(expectedDriversLeadingLapsCount[i].LeadingLapsByYear[j].LeadingLapCount, actual[i].LeadingLapsByYear[j].LeadingLapCount);
                    }
                }
            }
        }

        [TestMethod]
        public void GetDriversLeadingLapsCount_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedDriversLeadingLapsCount = new List<LeadingLapsModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetDriversLeadingLapsCount(from, to);

                // Assert
                Assert.AreEqual(expectedDriversLeadingLapsCount.Count, actual.Count);
            }
        }

        [TestMethod]
        public void GetConstructorsLeadingLapsCount_ReturnAggregatedConstructorsLeadingLapsCountList_IfThereAreAnyConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedConstructorsLeadingLapsCount = new List<LeadingLapsModel> 
            {
                new LeadingLapsModel
                {
                    Name = "FirstConstructor",
                    LeadingLapsByYear = new List<LeadingLapsByYearModel>
                    {
                        new LeadingLapsByYearModel
                        {
                            Year = 1,
                            LeadingLapCount = 1
                        },
                        new LeadingLapsByYearModel
                        {
                            Year = 2,
                            LeadingLapCount = 2
                        }
                    }
                },
                new LeadingLapsModel 
                {
                    Name = "SecondConstructor",
                    LeadingLapsByYear = new List<LeadingLapsByYearModel>
                    {
                        new LeadingLapsByYearModel
                        {
                            Year = 1,
                            LeadingLapCount = 1
                        },
                        new LeadingLapsByYearModel
                        {
                            Year = 2,
                            LeadingLapCount = 1
                        }
                    }
                },
                new LeadingLapsModel 
                {
                    Name = "ThirdConstructor",
                    LeadingLapsByYear = new List<LeadingLapsByYearModel>
                    {
                        new LeadingLapsByYearModel
                        {
                            Year = 1,
                            LeadingLapCount = 1
                        }
                    }
                }
            };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetConstructorsLeadingLapsCount(from, to);
                actual.Sort((x, y) => y.LeadingLapCount.CompareTo(x.LeadingLapCount));
                actual.ForEach(model => model.LeadingLapsByYear.Sort((x, y) => x.Year.CompareTo(y.Year)));

                // Assert
                Assert.AreEqual(expectedConstructorsLeadingLapsCount.Count, actual.Count);

                for (int i = 0; i < expectedConstructorsLeadingLapsCount.Count; i++)
                {
                    Assert.AreEqual(expectedConstructorsLeadingLapsCount[i].Name, actual[i].Name);
                    Assert.AreEqual(expectedConstructorsLeadingLapsCount[i].LeadingLapCount, actual[i].LeadingLapCount);
                    Assert.AreEqual(expectedConstructorsLeadingLapsCount[i].LeadingLapsByYear.Count, actual[i].LeadingLapsByYear.Count);

                    for (int j = 0; j < expectedConstructorsLeadingLapsCount[i].LeadingLapsByYear.Count; j++)
                    {
                        Assert.AreEqual(expectedConstructorsLeadingLapsCount[i].LeadingLapsByYear[j].Year, actual[i].LeadingLapsByYear[j].Year);
                        Assert.AreEqual(expectedConstructorsLeadingLapsCount[i].LeadingLapsByYear[j].LeadingLapCount, actual[i].LeadingLapsByYear[j].LeadingLapCount);
                    }
                }
            }
        }

        [TestMethod]
        public void GetConstructorsLeadingLapsCount_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedConstructorsLeadingLapsCount = new List<LeadingLapsModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetConstructorsLeadingLapsCount(from, to);

                // Assert
                Assert.AreEqual(expectedConstructorsLeadingLapsCount.Count, actual.Count);
            }
        }
    }
}
