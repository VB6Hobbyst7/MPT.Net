// ***********************************************************************
// Assembly         : MPT.GIS
// Author           : Mark Thomas
// Created          : 10-04-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-03-2017
// ***********************************************************************
// <copyright file="Location.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.GIS
{
    /// <summary>
    /// Represents a location.
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Gets the name of the location.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the alternative name.
        /// </summary>
        /// <value>The name of the other.</value>
        public string OtherName { get; private set; }

        /// <summary>
        /// Gets the elevation.
        /// </summary>
        /// <value>The elevation.</value>
        public int? Elevation { get; private set; }

        /// <summary>
        /// The coordinate
        /// </summary>
        private readonly Coordinate _coordinate = new Coordinate();

        /// <summary>
        /// Gets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public double Latitude => _coordinate.Latitude;

        /// <summary>
        /// Gets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
        public double Longitude => _coordinate.Longitude;

        /// <summary>
        /// Initializes a new instance of the <see cref="Location" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="coordinate">The latitude/longitude, and possibly elevation of the location.</param>
        /// <param name="otherName">Alternative name.</param>
        public Location(string name,
            Coordinate coordinate,
            string otherName = "")
        {
            if (coordinate == null)
            {
                Initialize(name, 0, 0, otherName, 0);
            }
            else
            {
                int altitude = 0;
                if (coordinate.Altitude.HasValue) altitude = (int)coordinate.Altitude;
                Initialize(name, coordinate.Latitude, coordinate.Longitude, otherName, altitude);
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Location" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="otherName">Alternative name.</param>
        /// <param name="elevation">The elevation.</param>
        public Location(string name, 
            double latitude, 
            double longitude, 
            string otherName = "", 
            int elevation = 0)
        {
            Initialize(name, latitude, longitude, otherName, elevation);
        }

        /// <summary>
        /// Initializes the specified values.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="otherName">Alternative name.</param>
        /// <param name="elevation">The elevation.</param>
        private void Initialize(string name,
            double latitude,
            double longitude,
            string otherName,
            int elevation)
        {
            Name = name;
            OtherName = otherName;
            Elevation = elevation;
            _coordinate.Latitude = latitude;
            _coordinate.Longitude = longitude;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return base.ToString();
            }
            if (string.IsNullOrEmpty(OtherName))
            {
                return Name;
            }
            return Name + " (" + OtherName + ")";
        }
    }
}
