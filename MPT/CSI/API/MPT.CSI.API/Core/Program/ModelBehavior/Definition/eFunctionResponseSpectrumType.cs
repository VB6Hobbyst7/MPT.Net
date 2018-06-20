// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-13-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eFunctionResponseSpectrumType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Types of Response Spectrum functions available in the application.
    /// </summary>
    public enum eFunctionResponseSpectrumType
    {
        /// <summary>
        /// Function is read from an external file.
        /// </summary>
        FromFile = 0,

        /// <summary>
        /// User-defined function.
        /// </summary>
        User = 1,

        /// <summary>
        /// UBC 94 function.
        /// </summary>
        UBC_94 = 2,

        /// <summary>
        /// UBC 97 function.
        /// </summary>
        UBC_97 = 3,

        /// <summary>
        /// BOCA 96 function.
        /// </summary>
        BOCA_96 = 4,

        /// <summary>
        /// NBCC 95 function.
        /// </summary>
        NBCC_95 = 5,

        /// <summary>
        /// IBC 2003 function.
        /// </summary>
        IBC_2003 = 6,

        /// <summary>
        /// NEHRP 97 function.
        /// </summary>
        NEHRP_97 = 7,

        /// <summary>
        /// Eurocode 8-1998 function.
        /// </summary>
        Eurocode_8_1998 = 8,

        /// <summary>
        /// NZS4203 1992 function.
        /// </summary>
        NZS_4203_1992 = 9,

        /// <summary>
        /// Chinese 2010 function.
        /// </summary>
        Chinese_2010 = 10,

        /// <summary>
        /// Italian Ordinanza 3274 function.
        /// </summary>
        Italian_Ordinanza_3274 = 11,

        /// <summary>
        /// IS1893:2002 function.
        /// </summary>
        IS_1893_2002 = 12,

        /// <summary>
        /// AASHTO LRFD 2006 function.
        /// </summary>
        AASHTO_LRFD_200 = 13,

        /// <summary>
        /// NCHRP Project 20-07 function.
        /// </summary>
        NCHRP_Project_20_07 = 14,

        /// <summary>
        /// IBC 2006 function.
        /// </summary>
        IBC_2006 = 15,

        /// <summary>
        /// NBCC 2005 function.
        /// </summary>
        NBCC_2005 = 16,

        /// <summary>
        /// Eurocode 8-2004 function.
        /// </summary>
        Eurocode_8_2004 = 17,

        /// <summary>
        /// AS 1170.4-2007 function.
        /// </summary>
        AS_1170_4_2007 = 18,

        /// <summary>
        /// NZS 1170.5-2004 function.
        /// </summary>
        NZS_1170_5_2004 = 19,

        /// <summary>
        /// AASHTO 2007 function.
        /// </summary>
        AASHTO_2007 = 20,

        /// <summary>
        /// Chinese JTG/T B02-2013 function.
        /// </summary>
        Chinese_JTG_T_B02_2013 = 21,

        /// <summary>
        /// Chinese GB 50111-2006 function.
        /// </summary>
        Chinese_GB_50111_2006 = 22,

        /// <summary>
        /// IBC 2009 function.
        /// </summary>
        IBC_2009 = 23,

        /// <summary>
        /// NBCC 2010 function.
        /// </summary>
        NBCC_2010 = 24,

        /// <summary>
        /// NTC 2008 function.
        /// </summary>
        NTC_2008 = 25,

        /// <summary>
        /// AASHTO 2012 function.
        /// </summary>
        AASHTO_2012 = 26,

        /// <summary>
        /// IBC 2012 function.
        /// </summary>
        IBC_2012 = 27,

        /// <summary>
        /// TSC 2007 function.
        /// </summary>
        TSC_2007 = 28,

        /// <summary>
        /// SI 413(1995) function.
        /// </summary>
        SI_413_1995 = 29,

        /// <summary>
        /// Argentina INPRES-CIRSOC 103 function.
        /// </summary>
        Argentina_INPRES_CIRSOC_103 = 30,

        /// <summary>
        /// Chile Norma NCh433+DS61 function.
        /// </summary>
        Chile_Norma_NCh433_DS61 = 31,

        /// <summary>
        /// Chile Norma NCh2369-2003 function.
        /// </summary>
        Chile_Norma_NCh2369_2003 = 32,

        /// <summary>
        /// Colombia NSR-10 function.
        /// </summary>
        Colombia_NSR_10 = 33,

        /// <summary>
        /// Ecuador NEC-11 Capitulo 2 function.
        /// </summary>
        Ecuador_NEC_11_Capitulo_2 = 34,

        /// <summary>
        /// Guatemala AGIES NSE 2-10 function.
        /// </summary>
        Guatemala_AGIES_NSE_2_10 = 35,

        /// <summary>
        /// Mexico NTC 2004 function.
        /// </summary>
        Mexico_NTC_2004 = 36,

        /// <summary>
        /// Peru Norma E.030 function.
        /// </summary>
        Peru_Norma_E_030 = 37,

        /// <summary>
        /// Dominican Republic R-001 function.
        /// </summary>
        Dominican_Republic_R_001 = 38,

        /// <summary>
        /// Venezuela COVENIN 1756-2:2001 function.
        /// </summary>
        Venezuela_COVENIN_1756_2_2001 = 39,

        /// <summary>
        /// KBC 2009 function.
        /// </summary>
        KBC_2009 = 40,

        /// <summary>
        /// Mexico CFE-93 function.
        /// </summary>
        Mexico_CFE_93 = 41,

        /// <summary>
        /// Peru NTE E.030 2014 function.
        /// </summary>
        Peru_NTE_E_030_2014 = 42,

        /// <summary>
        /// Mexico CFE-2008 function.
        /// </summary>
        Mexico_CFE_2008 = 43,

        /// <summary>
        /// Ecuado Norma NEC-SE-DS 2015 function.
        /// </summary>
        Ecuado_Norma_NEC_SE_DS_2015 = 44,

        /// <summary>
        /// Costa Rica Seismic Code 2010 function.
        /// </summary>
        Costa_Rica_Seismic_Code_2010 = 45,

        /// <summary>
        /// SP 14.13330.2014 function.
        /// </summary>
        SP_14_13330_2014 = 46,

        /// <summary>
        /// Chinese CJJ 166-2011 function.
        /// </summary>
        Chinese_CJJ_166_2011 = 47

    }
}

#endif