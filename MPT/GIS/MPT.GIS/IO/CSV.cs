// ***********************************************************************
// Assembly         : MPT.GIS
// Author           : Mark Thomas
// Created          : 12-02-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-02-2017
// ***********************************************************************
// <copyright file="Csv.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace MPT.GIS.IO
{
    /// <summary>
    /// Reads and writes data to/from CSV files.
    /// </summary>
    public static class Csv
    {
        /// <summary>
        /// Writes formations to a CSV file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="formations">The formations.</param>
        public static void Write(string filePath,
            List<FormationMatcher> formations)
        {
            using (TextWriter writer = File.CreateText(filePath))
            {
                var csv = new CsvWriter(writer);
                csv.WriteRecords(formations);
            }
        }

        /// <summary>
        /// Reads formations from a CSV file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="formations">The formations.</param>
        public static void Read(string filePath,
            out List<FormationMatcher> formations)
        {
            using (TextReader reader = File.OpenText(filePath))
            {
                var csv = new CsvReader(reader);
                csv.Configuration.PrepareHeaderForMatch = header =>
                    CultureInfo.CurrentCulture.TextInfo.ToTitleCase(header);
                var results = csv.GetRecords<FormationMatcher>();
                formations = new List<FormationMatcher>(results);
            }
        }


        /// <summary>
        /// Writes formations to a CSV file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="formations">The formations.</param>
        public static void Write(string filePath,
            List<Formation> formations)
        {

            using (TextWriter writer = File.CreateText(filePath))
            {
                var csv = new CsvWriter(writer);
                csv.WriteRecords(formations);
            }
        }

        /// <summary>
        /// Reads formations from a CSV file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="formations">The formations.</param>
        public static void Read(
            string filePath,
            out List<Formation> formations)
        {
            using (TextReader reader = File.OpenText(filePath))
            {
                var csv = new CsvReader(reader);
                csv.Configuration.PrepareHeaderForMatch = header =>
                    CultureInfo.CurrentCulture.TextInfo.ToTitleCase(header);
                var results = csv.GetRecords<Formation>();
                formations = new List<Formation>(results);
            }
        }



        /// <summary>
        /// Reads the www.peakbagger.com formations from the CSV file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="regionsExtents">The regions extents from which locations are to be read.</param>
        /// <returns>List&lt;Formation&gt;.</returns>
        public static List<Formation> ReadPeakbaggerFormations(
            string filePath,
            Extents regionsExtents)
        {
            return ReadPeakbaggerFormations(filePath,
                                        regionsExtents.MaxLatitude,
                                        regionsExtents.MinLongitude,
                                        regionsExtents.MaxLongitude,
                                        regionsExtents.MinLongitude);
        }

        // TODO: Read subformation
        /// <summary>
        /// Reads the www.peakbagger.com formations from the CSV file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="maxLatitude">The maximum latitude from which locations are to be read.</param>
        /// <param name="minLatitude">The minimum latitude from which locations are to be read.</param>
        /// <param name="maxLongitude">The maximum longitude from which locations are to be read.</param>
        /// <param name="minLongitude">The minimum longitude from which locations are to be read.</param>
        /// <returns>List&lt;Location&gt;.</returns>
        public static List<Formation> ReadPeakbaggerFormations(
            string filePath,
            double maxLatitude = 0,
            double minLatitude = 0,
            double maxLongitude = 0,
            double minLongitude = 0)
        {
            List<Formation> locations = new List<Formation>();
            using (TextReader reader = File.OpenText(filePath))
            {
                var csv = new CsvReader(reader);
                var results = csv.GetRecords<PeakbaggerLocation>().ToList();

                foreach (PeakbaggerLocation locationRaw in results)
                {
                    if (locationRaw.Longitude == null ||
                        locationRaw.Latitude == null)
                    {
                        continue;
                    }
                    double longitude = (double)locationRaw.Longitude;
                    double latitude = (double)locationRaw.Latitude;
                    if ((!(minLongitude <= longitude) || !(longitude <= maxLongitude)) ||
                        (!(minLatitude <= latitude) || !(latitude <= maxLatitude))) continue;
                    int elevation = 0;
                    if (locationRaw.Elevation != null)
                    {
                        elevation = (int)locationRaw.Elevation;
                    }
                    Formation location = new Formation(
                        locationRaw.Name,
                        latitude, longitude,
                        locationRaw.OtherName,
                        elevation);
                    locations.Add(location);
                }
            }
            return locations;
        }



        //private sealed class PeakbaggerLocationRawMap : ClassMap<PeakbaggerLocation>
        //{
        //    public PeakbaggerLocationRawMap()
        //    {
        //        Map(m => m.Index).Name("Index");
        //        Map(m => m.Name).ConvertUsing(row => row.GetField("Name"));
        //    }
        //}
    }
}
