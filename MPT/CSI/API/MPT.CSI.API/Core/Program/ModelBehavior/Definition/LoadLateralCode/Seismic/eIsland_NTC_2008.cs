// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eIsland_NTC_2008.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic
{
    /// <summary>
    /// The island considered for NTC 2008 seismic lateral code forces.
    /// </summary>
    public enum eIsland_NTC_2008
    {
        /// <summary>
        /// Alicudi.
        /// </summary>
        Alicudi = 1,

        /// <summary>
        /// Arcipelago Toscanoe.
        /// </summary>
        ArcipelagoToscano = 2,

        /// <summary>
        /// Filcudi.
        /// </summary>
        Filcudi = 3,

        /// <summary>
        /// Isole Egadie.
        /// </summary>
        IsoleEgadi = 4,

        /// <summary>
        /// Lampedusa.
        /// </summary>
        Lampedusa = 5,

        /// <summary>
        /// Linosa.
        /// </summary>
        Linosa = 6,

        /// <summary>
        /// Lipari.
        /// </summary>
        Lipari = 7,

        /// <summary>
        /// Palmarola.
        /// </summary>
        Palmarola = 8,

        /// <summary>
        /// Panarea.
        /// </summary>
        Panarea = 9,

        /// <summary>
        /// Pantelleria.
        /// </summary>
        Pantelleria = 10,

        /// <summary>
        /// Ponza.
        /// </summary>
        Ponza = 11,

        /// <summary>
        /// Salina.
        /// </summary>
        Salina = 12,

        /// <summary>
        /// Santo Stefano.
        /// </summary>
        SantoStefano = 13,

        /// <summary>
        /// Sardegna.
        /// </summary>
        Sardegna = 14,

        /// <summary>
        /// Stromboli.
        /// </summary>
        Stromboli = 15,

        /// <summary>
        /// Tremiti.
        /// </summary>
        Tremiti = 16,

        /// <summary>
        /// Ustica.
        /// </summary>
        Ustica = 17,

        /// <summary>
        /// Ventotene.
        /// </summary>
        Ventotene = 18,

        /// <summary>
        /// Vulcano.
        /// </summary>
        Vulcano = 19,

        /// <summary>
        /// Zannone .
        /// </summary>
        Zannone = 20,
    }
}
#endif