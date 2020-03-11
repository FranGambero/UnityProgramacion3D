using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Editores {
    public class SpawnManager : Singleton<SpawnManager> {
        public List<Transform> spawnPoints;

        [Header("Used for debug")]
        public List<Color> spawnColors;

        public void moveYSpawnPoints(float yAmount) {
            foreach(Transform t in spawnPoints) {
                t.position += new Vector3(0, yAmount);
            }
        }

        // OJO CUIDAO, SOLO SE ECUTA EN EL EDITOR Y ES PARA HACER DEBYGGG
        private void OnDrawGizmos() {

            if(spawnPoints != null && spawnColors != null) {
                for (int i = 0; i < spawnPoints.Count; i++) {
                    Color color = (i < spawnColors.Count ? spawnColors[i] : spawnColors[0]);

                    Gizmos.color = color;
                    Gizmos.DrawSphere(spawnPoints[i].position, 1f);
                }
            }
        }
    }
}
