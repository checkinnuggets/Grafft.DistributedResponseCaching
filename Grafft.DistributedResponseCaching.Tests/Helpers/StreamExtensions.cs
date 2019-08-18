using System.IO;

namespace Grafft.DistributedResponseCaching.Tests.Helpers
{
    public static class StreamExtensions
    {
        public static byte[] CopyToArray(this Stream i)
        {
            if (i == null)
            {
                return null;
            }

            byte[] oArr;

            using (var o = new MemoryStream())
            {
                i.Position = 0;
                i.CopyTo(o);
                oArr = o.ToArray();
            }

            return oArr;
        }
    }
}
