﻿using F1Statistics.Library.DataAccess.Interfaces;
using F1Statistics.Library.Models.Responses;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.DataAccess
{
    public class StandingsDataAccess : IStandingsDataAccess
    {
        public List<DriverStandingsDataResponse> GetDriverStandingsFrom(int year)
        {
            var client = new RestClient($"http://ergast.com/api/f1/{year}/driverStandings.json?limit=1000");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<MRDataResponse>(response.Content);

            return result.MRData.StandingsTable.StandingsLists[0].DriverStandings;
        }

        public List<ConstructorStandingsDataResponse> GetConstructorStandingsFrom(int year)
        {
            var client = new RestClient($"http://ergast.com/api/f1/{year}/constructorStandings.json?limit=1000");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<MRDataResponse>(response.Content);

            return result.MRData.StandingsTable.StandingsLists[0].ConstructorStandings;
        }
    }
}