// ***********************************************************************
// Assembly         : MPT.GIS
// Author           : Mark Thomas
// Created          : 10-04-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2017
// ***********************************************************************
// <copyright file="Region.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using MPT.Math;
using MPT.Geometry.Intersection;
using MPT.Geometry.Aggregation;

namespace MPT.GIS
{
    /// <summary>
    /// Represents a geographical region.
    /// </summary>
    public class Region : CompositeShape<Coordinate, Boundary, Extents>
    {
        #region Properties
        private readonly List<FormationMatcher> _formations = new List<FormationMatcher>();
        /// <summary>
        /// Gets the formations within the region.
        /// </summary>
        /// <value>The formations.</value>
        public IList<FormationMatcher> Formations => new ReadOnlyCollection<FormationMatcher>(_formations);

        private readonly List<Formation> _formationsBase = new List<Formation>();
        /// <summary>
        /// Gets the formations base.
        /// </summary>
        /// <value>The formations base.</value>
        public IList<Formation> FormationsBase => new ReadOnlyCollection<Formation>(_formationsBase);
        #endregion


        // TODO: Determine how to handle shape derived from internals vs. manually specified
        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="Region"/> class.
        /// </summary>
        /// <param name="regionName">Name of the region.</param>
        /// <param name="regionBoundary">The region boundary.</param>
        public Region(string regionName, 
            IList<Coordinate> regionBoundary) : base(regionBoundary, regionName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Region"/> class.
        /// </summary>
        /// <param name="regionName">Name of the region.</param>
        /// <param name="collection">A collection of regions.</param>
        public Region(string regionName, 
            IEnumerable<Region> collection) : base (collection, regionName)
        {
        }
        #endregion

        #region Add & Consolidate Formations
        // TODO: Consider formations that lie in a parent region but no child regions (depends on if shape can be manually specified or only from internals)

        /// <summary>
        /// Adds the formations to the region if the region names match.
        /// </summary>
        /// <param name="formations">The formations.</param>
        public void AddFormationsByRegionName(IEnumerable<FormationMatcher> formations)
        {
            if (IsBaseShape())
            {
                foreach (FormationMatcher formation in formations)
                {
                    AddFormationByRegionName(formation);
                }
            }
            else
            {
                List<FormationMatcher> formationsList = formations.ToList();
                foreach (Region region in this)
                {
                    region.AddFormationsByRegionName(formationsList);
                }
            }
            
        }

        /// <summary>
        /// Adds the formation to the region if the region name matches.
        /// </summary>
        /// <param name="formation">The location.</param>
        public bool AddFormationByRegionName(FormationMatcher formation)
        {
            if (!IsBaseShape())
            {
                foreach (CompositeShape<Coordinate, Boundary, Extents> shape in this)
                {
                    Region region = shape as Region;
                    bool? result = region?.AddFormationByRegionName(formation);
                    if (result.HasValue && result.Value) return true;
                }    
            }
            
            if (string.CompareOrdinal(formation.RegionName, Name) != 0) return false;

            _formations.Add(formation);
            return true;
        }


        /// <summary>
        /// Adds the formations to the region if the coordinates lie within the region shape.
        /// </summary>
        /// <param name="formations">The formations.</param>
        public void AddFormationsByCoordinates(IEnumerable<Formation> formations)
        {
            if (IsBaseShape())
            {
                foreach (Formation formation in formations)
                {
                    AddFormationByCoordinates(formation);
                }
            }
            else
            {
                List<Formation> formationsList = formations.ToList();
                foreach (CompositeShape<Coordinate, Boundary, Extents> shape in this)
                {
                    Region region = shape as Region;
                    region?.AddFormationsByCoordinates(formationsList);
                }
            }
        }

        /// <summary>
        /// Adds the formation to the region if the coordinates lie within the region shape.
        /// </summary>
        /// <param name="formation">The location.</param>
        public bool AddFormationByCoordinates(Formation formation)
        {
            if (!IsBaseShape())
            {
                foreach (CompositeShape<Coordinate, Boundary, Extents> shape in this)
                {
                    Region region = shape as Region;
                    bool? result = region?.AddFormationByCoordinates(formation);
                    if (result.HasValue && result.Value) return true;
                }
            }
            
            if (!isWithinExtents(formation) || !PointIntersection.IsWithinShape(new Point(formation.Longitude, formation.Latitude), Boundary.Select(coordinate => (Point) coordinate).ToArray())) return false;

            _formationsBase.Add(formation);
            return true;
        }

       


        /// <summary>
        /// Merges the formations by adding any potential matches to the matching <see cref="FormationMatcher"/> objects.
        /// </summary>
        public void MergeFormations()
        {
            if (IsBaseShape())
            {
                foreach (Formation potentialFormation in _formationsBase)
                {
                    if (_formations.Count > 0)
                    {
                        foreach (FormationMatcher formation in _formations)
                        {
                            formation.AddIfPotentialMatchByName(potentialFormation);
                        }
                    }
                    else
                    {
                        FormationMatcher formation =
                            new FormationMatcher(Name,
                                potentialFormation.Name,
                                potentialFormation.SubFormationName);
                        formation.AddIfPotentialMatchByName(potentialFormation);
                        AddFormationByRegionName(formation);
                    }
                }
            }
            else
            {
                foreach (CompositeShape<Coordinate, Boundary, Extents> shape in this)
                {
                    Region region = shape as Region;
                    region?.MergeFormations();
                }
            }
            
        }

        /// <summary>
        /// Condenses the potential matches, if possible.
        /// </summary>
        public void CondensePotentialMatches()
        {
            if (IsBaseShape())
            {
                foreach (FormationMatcher formation in _formations)
                {
                    formation.CondensePotentialMatches();
                }
            }
            else
            {
                foreach (CompositeShape<Coordinate, Boundary, Extents> shape in this)
                {
                    Region region = shape as Region;
                    region?.CondensePotentialMatches();
                }
            }
            
        }
        #endregion

        #region Overwrites

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return string.IsNullOrEmpty(Name) ? base.ToString() : Name;
        }
        #endregion

        #region Modify Regions
        // TODO: Finish
        public Region Split(IEnumerable<Coordinate> polyline)
        {
            throw new NotImplementedException();
            //return regions;
        }

        // TODO: Finish
        public void AlignInternalBoundaries()
        {
            throw new NotImplementedException();
        }
        #endregion


        #region Private: Methods
        /// <summary>
        /// Determines whether the specified location is within extents.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns><c>true</c> if the specified location is within extents; otherwise, <c>false</c>.</returns>
        private bool isWithinExtents(Formation location)
        {
            return _extents.IsWithinExtents(new Coordinate(location.Latitude, location.Longitude));
        }
        #endregion
    }
}

