using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Entities.NPC
{
    public class NPC_Respawn : MonoBehaviour
    {
        #region Inspector variables ---------------------------------------------------
        public List<Transform> respawnPoints;
        public GameObject npc_Prefap;
        public SO.Variables.SO_IntVariable numberOfTotal_NPC;
        public bool randomSpawn = true;
        #endregion

        #region NO_Inspector variables ------------------------------------------------
        #endregion

        #region methods ---------------------------------------------------------------
        private void Awake()
        {
            foreach (Transform i in this.transform)
            {
                respawnPoints.Add(i);
            }
        }

        private void Start()
        {
            if (randomSpawn)
            {
                StartCoroutine(nameof(SpawnAtRandomTimes));
            }
        }

		[Button]
        public void StartSpawning()
        {
            StartCoroutine(nameof(SpawnAtRandomTimes));
        }
		[Button]
		public void StopSpawning()
		{

		}

        IEnumerator SpawnAtRandomTimes()
        {
            int time = Random.Range(0, 10);
            yield return new WaitForSeconds(time);
            if (numberOfTotal_NPC.currentValue < 15)
                RespawnNPC();
            StartCoroutine(nameof(SpawnAtRandomTimes));
        }

        [NaughtyAttributes.Button]
        void RespawnNPC()
        {
            numberOfTotal_NPC.ChangeCurrentValueBy(1);
            int randomSpawnPoint = Random.Range(0, respawnPoints.Count);
            Instantiate(npc_Prefap, respawnPoints[randomSpawnPoint].position, respawnPoints[randomSpawnPoint].rotation);
        }
        #endregion
    }
}