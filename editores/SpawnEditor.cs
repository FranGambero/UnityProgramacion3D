using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Editores {
    [CustomEditor(typeof(SpawnManager))]
    public class SpawnEditor : Editor {

        int units = 0;
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            GUILayout.Space(20f);
            GUILayout.Label("Custom Editor Elements", EditorStyles.boldLabel);

            units = EditorGUILayout.IntField("Units to move: ", units);

            if(GUILayout.Button("Move all spawns Y units up")) {
                SpawnManager.Instance.moveYSpawnPoints(units);
            }

            if (GUILayout.Button("Move all spawns Y units down")) {
                SpawnManager.Instance.moveYSpawnPoints(-units);
            }
        }
    }
}
