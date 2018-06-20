// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 06-12-2017
//
// Last Modified By : Mark
// Last Modified On : 09-28-2017
// ***********************************************************************
// <copyright file="eIsland.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic
{
    /// <summary>
    /// Italian island used for NTC2008 seismic load patterns.
    /// </summary>
    public enum eIsland_NTC_2008
    {
        /// <summary>
        /// The alicudi
        /// </summary>
        Alicudi,
        /// <summary>
        /// The arcipelago toscano
        /// </summary>
        ArcipelagoToscano,
        /// <summary>
        /// The filcudi
        /// </summary>
        Filcudi,
        /// <summary>
        /// The isole egadi
        /// </summary>
        IsoleEgadi,
        /// <summary>
        /// The lampedusa
        /// </summary>
        Lampedusa,
        /// <summary>
        /// The linosa
        /// </summary>
        Linosa,
        /// <summary>
        /// The lipari
        /// </summary>
        Lipari,
        /// <summary>
        /// The palmarola
        /// </summary>
        Palmarola,
        /// <summary>
        /// The panarea
        /// </summary>
        Panarea,
        /// <summary>
        /// The pantelleria
        /// </summary>
        Pantelleria,
        /// <summary>
        /// The ponza
        /// </summary>
        Ponza,
        /// <summary>
        /// The salina
        /// </summary>
        Salina,
        /// <summary>
        /// The santo stefano
        /// </summary>
        SantoStefano,
        /// <summary>
        /// The sardegna
        /// </summary>
        Sardegna,
        /// <summary>
        /// The stromboli
        /// </summary>
        Stromboli,
        /// <summary>
        /// The tremiti
        /// </summary>
        Tremiti,
        /// <summary>
        /// The ustica
        /// </summary>
        Ustica,
        /// <summary>
        /// The ventotene
        /// </summary>
        Ventotene,
        /// <summary>
        /// The vulcano
        /// </summary>
        Vulcano,
        /// <summary>
        /// The zannone
        /// </summary>
        Zannone
    }
}
#endif
