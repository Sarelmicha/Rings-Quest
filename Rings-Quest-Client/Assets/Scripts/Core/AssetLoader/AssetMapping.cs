using UnityEngine;
using System.Collections.Generic;

namespace Happyflow.Core.AssetLoader
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(fileName = "AssetMapping", menuName = "AssetMapping")]
    public class AssetMapping : ScriptableObject
    {
        [SerializeField] private List<AssetInfo> m_AssetMappings;
        
        private Dictionary<string, string> AssetAddressDictionary
        {
            get
            {
                var dict = new Dictionary<string, string>();
                foreach (var asset in m_AssetMappings)
                {
                    dict[asset.Name] = asset.Address;
                }

                return dict;
            }
        }

        /// <summary>
        /// Map between all assets name and its addresses.
        /// </summary>
        private readonly Dictionary<string, string> m_AssetAddressDictionary;
        
        /// <summary>
        /// Get the address of the asset according to the asset name.
        /// </summary>
        /// <param name="assetName">If exists return the address of the asset, otherwise return null.</param>
        /// <returns></returns>
        public string GetAssetAddress(string assetName)
        {
            if (!AssetAddressDictionary.TryGetValue(assetName, out string assetAddress))
            {
                Debug.Log($"The name {assetName} is not found in the {nameof(AssetAddressDictionary)}");
                return null;
            }

            return assetAddress;
        }
    }
}