using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Data
{
    [CreateAssetMenu(fileName = "TileConfig", menuName = "Configs/TileConfig")]
    public class TileConfig : ScriptableObject
    {
        [field: SerializeField] public Tile Unknown { get; private set; }
        [field: SerializeField] public Tile Empty { get; private set; }
        [field: SerializeField] public Tile Mine { get; private set; }
        [field: SerializeField] public Tile Exploded { get; private set; }
        [field: SerializeField] public Tile Flagged { get; private set; }
        [field: SerializeField] public List<Tile> TileNums { get; private set; } = new();
    }
}