// ***********************************************************************
// Assembly         : MPT.GIS
// Author           : Mark Thomas
// Created          : 12-02-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-02-2017
// ***********************************************************************
// <copyright file="Formation.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.GIS
{
    /// <summary>
    /// Represents a formation.
    /// </summary>
    /// <seealso cref="MPT.GIS.Location" />
    public class Formation : Location
    {
        /// <summary>
        /// Gets the name of the sub-formation of the location.
        /// </summary>
        /// <value>The name of the sub formation.</value>
        public string SubFormationName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Formation" /> class.
        /// </summary>
        /// <param name="name">The name of the formation.</param>
        /// <param name="coordinate">The latitude/longitude, and possibly elevation of the location.</param>
        /// <param name="otherName">Alternative name.</param>
        /// <param name="subformationName">The name of the sub-formation</param>
        public Formation(string name,
            Coordinate coordinate,
            string otherName = "",
            string subformationName = "") : base(name, coordinate, otherName)
        {
            SubFormationName = subformationName;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Formation" /> class.
        /// </summary>
        /// <param name="name">The name of the formation.</param>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="otherName">Alternative name.</param>
        /// <param name="elevation">The elevation.</param>
        /// <param name="subformationName">The name of the sub-formation</param>
        public Formation(string name,
            double latitude,
            double longitude,
            string otherName = "",
            int elevation = 0,
            string subformationName = "") : base(name, latitude, longitude, otherName, elevation)
        {
            SubFormationName = subformationName;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Formation.</returns>
        public Formation Clone()
        {
            int elevaton = 0;
            if (Elevation.HasValue) elevaton = Elevation.Value;
            Formation formation = new Formation(Name, Latitude, Longitude, OtherName, elevaton, SubFormationName);
            return formation;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(Name) || 
                string.IsNullOrEmpty(SubFormationName))
            {
                return base.ToString();
            }

            string nameOfFormation = SubFormationName + " (of " + Name + ")";
            if (!string.IsNullOrEmpty(OtherName)) nameOfFormation += "(" + OtherName + ")";
            return nameOfFormation;
        }
    }

}
