using SO.Variables;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;

namespace Entities.NPC
{
    public class NPC_Respawn : MonoBehaviour
    {
        #region Inspector variables ---------------------------------------------------	
        [SerializeField] GameObject _npcPrefap;
        [Header("Probability options")]
        [SerializeField] int _maxTimeBetweenSpawns;
        [SerializeField] bool _startOnAwake = true;
        [SerializeField] SO_IntVariable _npcOnScene;
        [SerializeField] int _maxNumberOfNPCOnScreen = 15;        
        #endregion

        #region NO_Inspector variables ------------------------------------------------
        List<Transform> _spawnPoints = new List<Transform>();
        #endregion

        #region methods ---------------------------------------------------------------
        private void Awake()
        {
            foreach (Transform child in this.transform)
            {
                _spawnPoints.Add(child);
            }
        }

        private void Start()
        {
            if (_startOnAwake) StartCoroutine(nameof(SpawnRandomly));
        }

        IEnumerator SpawnRandomly()
        {
            int probab = Random.Range(1, _maxTimeBetweenSpawns);
            yield return new WaitForSeconds(probab);            
            InstantiateNPC();
            StartCoroutine(nameof(SpawnRandomly));
        }

        void InstantiateNPC()
        {
            if(_npcOnScene.currentValue < _maxNumberOfNPCOnScreen)
                Instantiate(_npcPrefap, _spawnPoints[Random.Range(0, _spawnPoints.Count)].position, Quaternion.identity);
        }

        [Button]
        public void StartSpawning()
        {
            StartCoroutine(nameof(SpawnRandomly));
        }
        [Button]
        public void StopSpawning()
        {
            StopCoroutine(nameof(SpawnRandomly));
        }
        [Button]
        public void SpawnNPC()
        {
            InstantiateNPC();
        }
        #endregion        
    }
}