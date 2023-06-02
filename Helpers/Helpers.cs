using System.Threading.Tasks;

namespace Helpers;

internal static class Helpers
{
    public static void Pause(int milliSecondsToSleep)
    {
        Thread.Sleep(milliSecondsToSleep);
    }
}