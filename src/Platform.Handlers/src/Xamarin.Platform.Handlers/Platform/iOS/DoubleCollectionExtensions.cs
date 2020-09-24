using System;
using Xamarin.Forms;

namespace Xamarin.Platform
{
    public static class DoubleCollectionExtensions
    {
        public static nfloat[] ToArray(this DoubleCollection doubleCollection)
        {
            if (doubleCollection == null || doubleCollection.Count == 0)
                return new nfloat[0];
            else
            {

                nfloat[] array = new nfloat[doubleCollection.Count];

                for (int i = 0; i < doubleCollection.Count; i++)
                    array[i] = (nfloat)doubleCollection[i];

                return array;
            }
        }
    }
}