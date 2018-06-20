// ***********************************************************************
// Assembly         : MPT.GIS
// Author           : Mark Thomas
// Created          : 10-05-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-02-2017
// ***********************************************************************
// <copyright file="Merger.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.IO;
using MPT.GIS.IO;

namespace MPT.GIS
{
    /// <summary>
    /// Aggregates regions and formations, merging the peakbagger and base sets of formations.
    /// </summary>
    public static class Aggregator
    {

        /// <summary>
        /// Compiles regions with locations, merging the peakbagger and base sets of formations from Excel files.
        /// </summary>
        /// <param name="filePathRegionsKml">The file path to the KML file containing the regions.</param>
        /// <param name="filePathExcel">The file path to the Excel file containing the base formation sets.</param>
        /// <param name="filePathPeakbaggerExcel">The file path to the Excel peakbagger file.</param>
        /// <param name="filePathPeakbaggerCsv">The file path to the peakbagger CSV file.
        /// If specified, this will be read instead of the Excel file.</param>
        /// <returns>List&lt;Region&gt;.</returns>
        public static Region CollateLocationsFromExcel(string filePathRegionsKml, 
            string filePathExcel, 
            string filePathPeakbaggerExcel,
            string filePathPeakbaggerCsv = "")
        {
            // Get regions
            Region regions = Kml.ReadRegions(filePathRegionsKml);
            
            // Add database locations
            List<FormationMatcher> locations = GetFormationsFromExcel(filePathExcel, saveToCsv: true);
            regions.AddFormationsByRegionName(locations);

            // Get peakbagger locations
            bool potentialLocationsAreSaved = !string.IsNullOrWhiteSpace(filePathPeakbaggerCsv);
            List<Formation> locationsPeakbagger = potentialLocationsAreSaved 
                ? GetPeakbaggerFormationsFromCsvFiltered(filePathPeakbaggerCsv) 
                : GetPeakbaggerFormationsFromExcel(filePathPeakbaggerExcel, new Extents(regions.Extents), saveToCsv: true);
            regions.AddFormationsByCoordinates(locationsPeakbagger);

            // Add locations to the appropriate region
            foreach (Region region in regions)
            {
                region.MergeFormations();
                region.CondensePotentialMatches();
            }

            return regions;
        }

        /// <summary>
        /// Compiles regions with locations, merging the peakbagger and base sets of formations from *.csv files.
        /// </summary>
        /// <param name="filePathRegionsKml">The file path to the KML file containing the regions.</param>
        /// <param name="filePathCsv">The file path to the *.csv file containing the base formation sets.</param>
        /// <param name="filePathPeakbaggerCsvSource">The file path to the master peakbagger *.csv file.</param>
        /// <param name="filePathPeakbaggerCsv">The file path to the filtered peakbagger *.csv file.</param>
        /// <returns>Regions.</returns>
        public static Region CollateLocationsFromCsv(string filePathRegionsKml,
            string filePathCsv,
            string filePathPeakbaggerCsvSource,
            string filePathPeakbaggerCsv = "")
        {
            // Get regions
            Region regions = Kml.ReadRegions(filePathRegionsKml);

            // Add database locations
            List<FormationMatcher> locations = GetFormationsFromCsv(filePathCsv);
            regions.AddFormationsByRegionName(locations);

            // Get peakbagger locations
            bool potentialLocationsAreSaved = !string.IsNullOrWhiteSpace(filePathPeakbaggerCsv);
            List<Formation> locationsPeakbagger = potentialLocationsAreSaved 
                ? GetPeakbaggerFormationsFromCsvFiltered(filePathPeakbaggerCsv) 
                : GetPeakbaggerFormationsFromCsvOriginal(filePathPeakbaggerCsvSource, new Extents(regions.Extents), saveToCsv: true);
            regions.AddFormationsByCoordinates(locationsPeakbagger);

            // Add locations to the appropriate region
            foreach (Region region in regions)
            {
                region.MergeFormations();
                region.CondensePotentialMatches();
            }

            return regions;
        }


        /// <summary>
        /// Gets the locations from excel.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="saveToCsv">if set to <c>true</c> [save to CSV].</param>
        /// <returns>List&lt;FormationMatcher&gt;.</returns>
        public static List<FormationMatcher> GetFormationsFromExcel(
            string filePath, 
            bool saveToCsv = false)
        {
            List<FormationMatcher> locations = IO.Excel.ReadFormations(filePath);
            if (saveToCsv) Aggregator.saveToCsv(filePath, locations);
            return locations;
        }

        /// <summary>
        /// Gets the formations from CSV.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>List&lt;FormationMatcher&gt;.</returns>
        public static List<FormationMatcher> GetFormationsFromCsv(string filePath)
        {
            List<FormationMatcher> locations;
            Csv.Read(filePath, out locations);
            return locations;
        }

        /// <summary>
        /// Gets the peakbagger formations from excel.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="regionsExtents">The regions extents.</param>
        /// <param name="saveToCsv">if set to <c>true</c> [save to CSV].</param>
        /// <returns>List&lt;Formation&gt;.</returns>
        public static List<Formation> GetPeakbaggerFormationsFromExcel(
            string filePath,
            Extents regionsExtents,
            bool saveToCsv = false)
        {
            List<Formation> locationsPeakbagger = IO.Excel.ReadPeakbaggerFormations(filePath, regionsExtents);
            if (saveToCsv) Aggregator.saveToCsv(filePath, locationsPeakbagger);
            return locationsPeakbagger;
        }

        /// <summary>
        /// Gets the peakbagger formations from CSV original.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="regionsExtents">The regions extents.</param>
        /// <param name="saveToCsv">if set to <c>true</c> [save to CSV].</param>
        /// <returns>List&lt;Formation&gt;.</returns>
        public static List<Formation> GetPeakbaggerFormationsFromCsvOriginal(
            string filePath,
            Extents regionsExtents,
            bool saveToCsv = false)
        {
            List<Formation> locationsPeakbagger = Csv.ReadPeakbaggerFormations(filePath, regionsExtents);
            if (saveToCsv) Aggregator.saveToCsv(filePath, locationsPeakbagger);
            return locationsPeakbagger;
        }

        /// <summary>
        /// Gets the peakbagger formations from CSV filtered.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>List&lt;Formation&gt;.</returns>
        public static List<Formation> GetPeakbaggerFormationsFromCsvFiltered(
            string filePath)
        {
            List<Formation> locationsPeakbagger;
            Csv.Read(filePath, out locationsPeakbagger);
            return locationsPeakbagger;
        }



        /// <summary>
        /// Saves matching formations to CSV.
        /// </summary>
        /// <param name="sourceFilePath">The source file path.</param>
        /// <param name="formations">The formations.</param>
        private static void saveToCsv(
            string sourceFilePath,
            List<FormationMatcher> formations)
        {
            Csv.Write(createFilePathCsv(sourceFilePath), formations);
        }

        /// <summary>
        /// Saves formations to CSV.
        /// </summary>
        /// <param name="sourceFilePath">The source file path.</param>
        /// <param name="formations">The formations.</param>
        private static void saveToCsv(
            string sourceFilePath,
            List<Formation> formations)
        {
            Csv.Write(createFilePathCsv(sourceFilePath), formations);
        }

        /// <summary>
        /// Creates the file path for a *.csv file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>System.String.</returns>
        private static string createFilePathCsv(string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath) + ".csv";
        }
        


        // Google Earth no longer seems to support being opened with a file.
        ///// <summary>
        ///// Opens the in google earth.
        ///// </summary>
        ///// <param name="dbLocation">The database location.</param>
        ///// <param name="pathFile">The path file.</param>
        ///// <param name="pathApplication">The path application.</param>
        //public static void OpenInGoogleEarth(FormationMatcher dbLocation, string pathFile, string pathApplication)
        //{
        //    Kml.WriteToKml(pathFile, dbLocation);

        //    System.Diagnostics.Process process = new System.Diagnostics.Process();
        //    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
        //    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        //    startInfo.FileName = "cmd.exe";
        //    // /C Carries out the command specified by string and then terminates
        //    startInfo.Arguments = "/C \"" + pathApplication + "\" \"" + pathFile + "\"";
        //    process.StartInfo = startInfo;
        //    process.Start();
        //}
    }
}
