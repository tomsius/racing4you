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
    public class FastestLapsServiceTests
    {
        private FastestLapsService _service;
        private Mock<IOptionsValidator> _validator;
        private Mock<IFastestLapsAggregator> _aggregator;

        [TestInitialize]
        public void Setup()
        {
            _validator = new Mock<IOptionsValidator>();
            _aggregator = new Mock<IFastestLapsAggregator>();

            _validator.Setup((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>())).Verifiable();

            _service = new FastestLapsService(_validator.Object, _aggregator.Object);
        }

        private List<FastestLapModel> GenerateFastestLappers()
        {
            var winners = new List<FastestLapModel>
            {
                new FastestLapModel
                {
                    Name = "First",
                    FastestLapsCount = 2
                },
                new FastestLapModel
                {
                    Name = "Second",
                    FastestLapsCount = 1
                }
            };

            return winners;
        }

        private List<UniqueSeasonFastestLapModel> GenerateUniqueSeasonFastestLappers()
        {
            var uniqueSeaosnWinners = new List<UniqueSeasonFastestLapModel>
            {
                new UniqueSeasonFastestLapModel
                {
                    Season = 2020,
                    FastestLapAchievers = new List<string>
                    {
                        "First",
                        "Second"
                    }
                },
                new UniqueSeasonFastestLapModel
                {
                    Season = 2021,
                    FastestLapAchievers = new List<string>
                    {
                        "First",
                        "Second",
                        "Third"
                    }
                }
            };

            return uniqueSeaosnWinners;
        }

        [TestMethod]
        public void AggregateDriversFastestLaps_ReturnSortedAggregatedFastestDriversList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedDriversFastestLappers = GenerateFastestLappers();
            expectedDriversFastestLappers.Sort((x, y) => y.FastestLapsCount.CompareTo(x.FastestLapsCount));
            _aggregator.Setup((aggregator) => aggregator.GetDriversFastestLaps(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateFastestLappers());

            // Act
            var actual = _service.AggregateDriversFastestLaps(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedDriversFastestLappers.Count, actual.Count);

            for (int i = 0; i < expectedDriversFastestLappers.Count; i++)
            {
                Assert.AreEqual(expectedDriversFastestLappers[i].Name, actual[i].Name);
                Assert.AreEqual(expectedDriversFastestLappers[i].FastestLapsCount, actual[i].FastestLapsCount);
            }
        }

        [TestMethod]
        public void AggregateDriversFastestLaps_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedDriversFastestLappers = new List<FastestLapModel>();
            expectedDriversFastestLappers.Sort((x, y) => y.FastestLapsCount.CompareTo(x.FastestLapsCount));
            _aggregator.Setup((aggregator) => aggregator.GetDriversFastestLaps(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedDriversFastestLappers);

            // Act
            var actual = _service.AggregateDriversFastestLaps(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedDriversFastestLappers.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateConstructorsFastestLaps_ReturnSortedAggregatedFastestConstructorsList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedConstructorsFastestLappers = GenerateFastestLappers();
            expectedConstructorsFastestLappers.Sort((x, y) => y.FastestLapsCount.CompareTo(x.FastestLapsCount));
            _aggregator.Setup((aggregator) => aggregator.GetConstructorsFastestLaps(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateFastestLappers());

            // Act
            var actual = _service.AggregateConstructorsFastestLaps(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedConstructorsFastestLappers.Count, actual.Count);

            for (int i = 0; i < expectedConstructorsFastestLappers.Count; i++)
            {
                Assert.AreEqual(expectedConstructorsFastestLappers[i].Name, actual[i].Name);
                Assert.AreEqual(expectedConstructorsFastestLappers[i].FastestLapsCount, actual[i].FastestLapsCount);
            }
        }

        [TestMethod]
        public void AggregateConstructorsFastestLaps_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedConstructorsFastestLappers = new List<FastestLapModel>();
            expectedConstructorsFastestLappers.Sort((x, y) => y.FastestLapsCount.CompareTo(x.FastestLapsCount));
            _aggregator.Setup((aggregator) => aggregator.GetConstructorsFastestLaps(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedConstructorsFastestLappers);

            // Act
            var actual = _service.AggregateConstructorsFastestLaps(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedConstructorsFastestLappers.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateUniqueDriversFastestLapsPerSeason_ReturnAggregatedUniqueSeasonDriversFastestLappersList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedUniqueDriversFastestLappers = GenerateUniqueSeasonFastestLappers();
            _aggregator.Setup((aggregator) => aggregator.GetUniqueDriversFastestLaps(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedUniqueDriversFastestLappers);

            // Act
            var actual = _service.AggregateUniqueDriversFastestLapsPerSeason(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedUniqueDriversFastestLappers.Count, actual.Count);

            for (int i = 0; i < expectedUniqueDriversFastestLappers.Count; i++)
            {
                Assert.AreEqual(expectedUniqueDriversFastestLappers[i].Season, actual[i].Season);
                Assert.AreEqual(expectedUniqueDriversFastestLappers[i].UniqueFastestLapsCount, actual[i].UniqueFastestLapsCount);
            }
        }

        [TestMethod]
        public void AggregateUniqueDriversFastestLapsPerSeason_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedUniqueDriversFastestLappers = new List<UniqueSeasonFastestLapModel>();
            _aggregator.Setup((aggregator) => aggregator.GetUniqueDriversFastestLaps(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedUniqueDriversFastestLappers);

            // Act
            var actual = _service.AggregateUniqueDriversFastestLapsPerSeason(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedUniqueDriversFastestLappers.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateUniqueConstructorsFastestLapsPerseason_ReturnAggregatedUniqueSeasonConstructorsFastestLappersList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedUniqueConstructorsFastestLappers = GenerateUniqueSeasonFastestLappers();
            _aggregator.Setup((aggregator) => aggregator.GetUniqueConstructorsFastestLaps(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedUniqueConstructorsFastestLappers);

            // Act
            var actual = _service.AggregateUniqueConstructorsFastestLapsPerseason(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedUniqueConstructorsFastestLappers.Count, actual.Count);

            for (int i = 0; i < expectedUniqueConstructorsFastestLappers.Count; i++)
            {
                Assert.AreEqual(expectedUniqueConstructorsFastestLappers[i].Season, actual[i].Season);
                Assert.AreEqual(expectedUniqueConstructorsFastestLappers[i].UniqueFastestLapsCount, actual[i].UniqueFastestLapsCount);
            }
        }

        [TestMethod]
        public void AggregateUniqueConstructorsFastestLapsPerseason_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedUniqueConstructorsFastestLappers = new List<UniqueSeasonFastestLapModel>();
            _aggregator.Setup((aggregator) => aggregator.GetUniqueConstructorsFastestLaps(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedUniqueConstructorsFastestLappers);

            // Act
            var actual = _service.AggregateUniqueConstructorsFastestLapsPerseason(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedUniqueConstructorsFastestLappers.Count, actual.Count);
        }
    }
}