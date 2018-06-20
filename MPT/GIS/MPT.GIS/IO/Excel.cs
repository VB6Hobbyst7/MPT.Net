// ***********************************************************************
// Assembly         : MPT.GIS
// Author           : Mark Thomas
// Created          : 10-04-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2017
// ***********************************************************************
// <copyright file="Excel.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using NMath = System.Math;

using MPT.Excel;

namespace MPT.GIS.IO
{
    /// <summary>
    /// Reads and writes data to/from Excel files.
    /// </summary>
    public static class Excel
    {
        /// <summary>
        /// Opens the Excel file.
        /// </summary>
        /// <param name="filePath">The file path to the Excel file.</param>
        /// <returns>ExcelFile.</returns>
        public static ExcelFile OpenFile(string filePath)
        {
            return new ExcelFile(filePath);
        }
        

        /// <summary>
        /// Reads all of the formations and returns a list of objects with data.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>List&lt;FormationMatcher&gt;.</returns>
        public static List<FormationMatcher> ReadFormations(string filePath)
        {
            List<FormationMatcher> formations;
            using (ExcelFile file = OpenFile(filePath))
            {
                formations = ReadFormations(file);
            }
            return formations;
        }

        /// <summary>
        /// Reads all of the formations and returns a list of objects with data.
        /// </summary>
        /// <param name="excel">The excel application.</param>
        /// <returns>List&lt;DBLocation&gt;.</returns>
        private static List<FormationMatcher> ReadFormations(ExcelFile excel)
            {
                List<FormationMatcher> locations = new List<FormationMatcher>();
                List<string> areas = excel.RangeValuesBelowHeader("AreaChild");
                List<string> names = excel.RangeValuesBelowHeader("Formation");
                int numberOfRows = names.Count;
                List<string> latitudes = excel.RangeValuesBelowHeader("Latitude", includeNull: true, numberOfRows: numberOfRows);
                List<string> longitudes = excel.RangeValuesBelowHeader("Longitude", includeNull: true, numberOfRows: numberOfRows);
                List<string> otherNames = excel.RangeValuesBelowHeader("SubFormation", includeNull: true, numberOfRows: numberOfRows);

                for (int i = 0, length = names.Count; i < length; i++)
                {
                    double longitude;
                    if (!double.TryParse(longitudes[i], out longitude))
                    {
                        longitude = 0;
                    }
                    double latitude;
                    if (!double.TryParse(latitudes[i], out latitude))
                    {
                        latitude = 0;
                    }
                    if (!(NMath.Abs(longitude) < 1) || 
                        !(NMath.Abs(latitude) < 1)) continue;
                    FormationMatcher location = new FormationMatcher(areas[i], names[i], otherNames[i]);
                    locations.Add(location);
                }
                return locations;
            }



        // TODO: Handle sub-formations        
        /// <summary>
        /// Reads the www.peakbagger.com formations from the Excel file.
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

        /// <summary>
        /// Reads the www.peakbagger.com formations from the Excel file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="maxLatitude">The maximum latitude from which locations are to be read.</param>
        /// <param name="minLatitude">The minimum latitude from which locations are to be read.</param>
        /// <param name="maxLongitude">The maximum longitude from which locations are to be read.</param>
        /// <param name="minLongitude">The minimum longitude from which locations are to be read.</param>
        /// <returns>List&lt;PeakbaggerLocation&gt;.</returns>
        public static List<Formation> ReadPeakbaggerFormations(
            string filePath, 
            double maxLatitude = 0,
            double minLatitude = 0,
            double maxLongitude = 0,
            double minLongitude = 0)
        {
            List<Formation> formations = new List<Formation>();
            using (ExcelFile excel = OpenFile(filePath))
            {
                List<string> names = excel.RangeValuesBelowHeader("peakName");
                List<string> latitudes = excel.RangeValuesBelowHeader("peakLatitude");
                List<string> longitudes = excel.RangeValuesBelowHeader("peakLongitude");
                int numberOfRows = names.Count;
                List<string> elevations = excel.RangeValuesBelowHeader("elevationFt", includeNull: true,
                    numberOfRows: numberOfRows);
                List<string> otherNames = excel.RangeValuesBelowHeader("peakNameAlt", includeNull: true,
                    numberOfRows: numberOfRows);


                for (int i = 0, length = latitudes.Count; i < length; i++)
                {
                    double longitude;
                    if (!double.TryParse(longitudes[i], out longitude))
                    {
                        continue;
                    }
                    double latitude;
                    if (!double.TryParse(latitudes[i], out latitude))
                    {
                        continue;
                    }
                    if ((!(minLongitude <= longitude) || !(longitude <= maxLongitude)) ||
                        (!(minLatitude <= latitude) || !(latitude <= maxLatitude))) continue;

                    int elevation;
                    int.TryParse(elevations[i], out elevation);
                    Formation formation = new Formation(
                        names[i], 
                        latitude, 
                        longitude, 
                        otherNames[i], 
                        elevation);
                    formations.Add(formation);
                }
            }
            return formations;
        }



        /// <summary>
        /// Displays the error.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void DisplayError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
        }
    }
}
