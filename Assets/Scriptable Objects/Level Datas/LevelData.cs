using System.Collections.Generic;
using UnityEngine;

namespace Scriptable_Objects.Level_Datas
{
    [CreateAssetMenu]
    public class LevelData : ScriptableObject
    {
        public Vector2 player;
        public List<Vector2> boxes;
    }
}