#if BUILD_ETABS2015 || BUILD_ETABS2016
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design
{
    public enum eDesignActionType
    {
        NonComposite = 1,
        ShortTermComposite = 2,
        LongTermComposite = 3,
        Staged = 4,
        Other = 5
    }
}
#endif
