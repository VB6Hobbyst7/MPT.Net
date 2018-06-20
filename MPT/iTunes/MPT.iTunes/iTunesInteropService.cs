using System.Runtime.InteropServices;
using iTunesLib;

// Source: https://backtosquarezero.blogspot.com/2012/12/updating-your-itunes-library-with-bpm.html
// TODO: Consider putting this in a dedicated place on GitHub and sharing link on site, as current links no longer work.
namespace MPT.iTunes
{
    public static class iTunesInteropService
    {
        public static void UpdateTrackInfo()
        {
            iTunesApp myITunesApp = null;
            try
            {
                myITunesApp = new iTunesApp();

                var mainLibrary = myITunesApp.LibraryPlaylist;
                foreach (var track in mainLibrary.Tracks)
                {
                    var filetrack = track as IITFileOrCDTrack;
                    filetrack?.UpdateInfoFromFile();
                }
            }
            finally
            {
                if (myITunesApp != null)
                    Marshal.ReleaseComObject(myITunesApp);
            }
        }
    }
}
