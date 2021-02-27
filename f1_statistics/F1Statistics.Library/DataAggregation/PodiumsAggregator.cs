﻿using F1Statistics.Library.DataAccess.Interfaces;
using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Statistics.Library.DataAggregation
{
    public class PodiumsAggregator : IPodiumsAggregator
    {
        private readonly IResultsDataAccess _resultsDataAccess;

        public PodiumsAggregator(IResultsDataAccess resultsDataAccess)
        {
            _resultsDataAccess = resultsDataAccess;
        }

        public List<PodiumsModel> GetDriversPodiums(int from, int to)
        {
            var driversPodiums = new List<PodiumsModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetRacesFrom(year);

                foreach (var race in races)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        string podiumFinisher = $"{race.Results[i].Driver.givenName} {race.Results[i].Driver.familyName}";

                        lock (lockObject)
                        {
                            if (!driversPodiums.Where(driver => driver.Name == podiumFinisher).Any())
                            {
                                var newPodiumsModel = new PodiumsModel { Name = podiumFinisher, PodiumCount = 1 };
                                driversPodiums.Add(newPodiumsModel);
                            }
                            else
                            {
                                driversPodiums.Where(driver => driver.Name == podiumFinisher).First().PodiumCount++;
                            }
                        } 
                    }
                }
            });

            return driversPodiums;
        }

        public List<PodiumsModel> GetConstructorsPodiums(int from, int to)
        {
            var constructorsPodiums = new List<PodiumsModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetRacesFrom(year);

                foreach (var race in races)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        string podiumFinisher = $"{race.Results[i].Constructor.name}";

                        lock (lockObject)
                        {
                            if (!constructorsPodiums.Where(constructor => constructor.Name == podiumFinisher).Any())
                            {
                                var newPodiumsModel = new PodiumsModel { Name = podiumFinisher, PodiumCount = 1 };
                                constructorsPodiums.Add(newPodiumsModel);
                            }
                            else
                            {
                                constructorsPodiums.Where(constructor => constructor.Name == podiumFinisher).First().PodiumCount++;
                            }
                        }
                    }
                }
            });

            return constructorsPodiums;
        }

        public List<SamePodiumsModel> GetSameDriversPodiums(int from, int to)
        {
            var sameDriversPodiums = new List<SamePodiumsModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetRacesFrom(year);

                foreach (var race in races)
                {
                    List<string> podiumFinishers = new List<string>(3);

                    for (int i = 0; i < 3; i++)
                    {
                        string podiumFinisher = $"{race.Results[i].Driver.givenName} {race.Results[i].Driver.familyName}";
                        podiumFinishers.Add(podiumFinisher);
                    }

                    lock (lockObject)
                    {
                        if (IsUniquePodiumFinishers(sameDriversPodiums, podiumFinishers))
                        {
                            var newSamePodiumsModel = new SamePodiumsModel { PodiumFinishers = podiumFinishers, SamePodiumCount = 1 };
                            sameDriversPodiums.Add(newSamePodiumsModel);
                        }
                        else
                        {
                            IncrementSamePodiumCount(sameDriversPodiums, podiumFinishers);
                        }
                    }
                }
            });

            return sameDriversPodiums;
        }

        private bool IsUniquePodiumFinishers(List<SamePodiumsModel> samePodiums, List<string> podiumFinishers)
        {
            foreach (var podium in samePodiums)
            {
                if (IsPodiumEqual(podium.PodiumFinishers, podiumFinishers))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsPodiumEqual(List<string> existingPodium, List<string> newPodium)
        {
            List<string> podium = new List<string>(existingPodium);

            foreach (var podiumFinisher in newPodium)
            {
                podium.Remove(podiumFinisher);
            }

            return podium.Count == 0;
        }

        private void IncrementSamePodiumCount(List<SamePodiumsModel> samePodiums, List<string> podiumFinishers)
        {
            foreach (var podium in samePodiums)
            {
                if (IsPodiumEqual(podium.PodiumFinishers, podiumFinishers))
                {
                    podium.SamePodiumCount++;
                    break;
                }
            }
        }

        public List<SamePodiumsModel> GetSameConstructorsPodiums(int from, int to)
        {
            var sameConstructorsPodiums = new List<SamePodiumsModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetRacesFrom(year);

                foreach (var race in races)
                {
                    List<string> podiumFinishers = new List<string>(3);

                    for (int i = 0; i < 3; i++)
                    {
                        string podiumFinisher = $"{race.Results[i].Constructor.name}";
                        podiumFinishers.Add(podiumFinisher);
                    }

                    lock (lockObject)
                    {
                        if (IsUniquePodiumFinishers(sameConstructorsPodiums, podiumFinishers))
                        {
                            var newSamePodiumsModel = new SamePodiumsModel { PodiumFinishers = podiumFinishers, SamePodiumCount = 1 };
                            sameConstructorsPodiums.Add(newSamePodiumsModel);
                        }
                        else
                        {
                            IncrementSamePodiumCount(sameConstructorsPodiums, podiumFinishers);
                        }
                    }
                }
            });

            return sameConstructorsPodiums;
        }
    }
}
