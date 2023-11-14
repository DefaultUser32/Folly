using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassManager : MonoBehaviour
{
    [SerializeField] List<GameObject> shards;
    public List<bool> locationsAreFilled;


    private void Start()
    {
        foreach (GameObject shard in shards)
        {
            GlassShard shardManager = shard.GetComponent<GlassShard>();
            shard.transform.localPosition = new Vector3(Random.Range(shardManager.minStartPos.x, shardManager.maxStartPos.x), Random.Range(shardManager.minStartPos.y, shardManager.maxStartPos.y), 0);
        }
    }
    private void Update()
    {
        foreach (bool location in locationsAreFilled) {
            if (!location) return;
        }
        FindObjectOfType<PlayerSceneManager>().UnloadGlassManager();
    }
}
