using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.ResourceManagement
{
    public struct AssetInfo
    {
        public string Asset;
        public string Bundle;
        public override bool Equals(object obj)
        {
            var right = (AssetInfo)obj;
            return Asset.Equals(right.Asset) && Bundle.Equals(right.Bundle);
        }

        public override int GetHashCode()
        {
            return 31 * Asset.GetHashCode() + Bundle.GetHashCode();
        }

        public static AssetInfo EmptyAssetInfo = new AssetInfo();

        public static AssetInfoComparer AssetInfoComparerInstance = new AssetInfoComparer();
    }

    public class AssetInfoComparer : IEqualityComparer<AssetInfo>
    {
        public bool Equals(AssetInfo x, AssetInfo y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(AssetInfo obj)
        {
            return obj.GetHashCode();
        }
    }

}
