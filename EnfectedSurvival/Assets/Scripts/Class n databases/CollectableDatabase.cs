using UnityEngine;

[CreateAssetMenu(fileName = "CollectableDatabase", menuName = "collectable/collectable select database")]
public class CollectableDatabase : ScriptableObject
{
    public GameObject[] collectables;
    public int collectableCount { get { return collectables.Length; } }

    public GameObject GetCollectable(int index)
    {
        return collectables[index];
    }
}