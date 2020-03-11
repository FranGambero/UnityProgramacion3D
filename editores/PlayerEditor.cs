using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Tools {
    public class PlayerEditor : EditorWindow {
        
        [MenuItem("Game/PlayerEditor")]
        public static void showWindow() {
            // Show existing window instance. If one doesnt exist, make one!
            EditorWindow window = EditorWindow.GetWindow(typeof(PlayerEditor));
            window.minSize = new Vector2(400, 350);
            window.titleContent = new GUIContent("Creador de personajes");
        }

        private void OnGUI() {
            showBaseSettings();
            showAdminSettings();
            showPlayerJSON();
        }

        #region BASE
        private void showBaseSettings() {
            showHeader("Base Settings");
            showpPlayerFields();
            showSpecialSettings();
        }

        private void showpPlayerFields() {
            myPlayerId = EditorGUILayout.TextField("Player ID ", myPlayerId);
            myPlayerHealth = EditorGUILayout.IntField("Health ", myPlayerHealth);
            myPlayerArmor = EditorGUILayout.IntField("Armor ", myPlayerArmor);
            myCharClass = (CharacterClass) EditorGUILayout.EnumPopup("Class ", myCharClass);

        }
        #endregion

        #region PLAYER VARIABLES

        string myPlayerId = "You can find me in AllPlayers.txt";

        int myPlayerHealth = 100;
        int myPlayerArmor = 50;

        CharacterClass myCharClass;
        CharacterStats myCharStats;

        int myStrength, myDextery, myInteligence;

        float myOverPowered = 1;
        bool myIsInmortal = false;

        #endregion

        #region EDITOR VARIABLES

        bool myCustomStats = false;
        bool myAdminMode = false;

        string myKeyID = "\"ID\"";
        string myKeyHealth = "\"Health\"";
        string myKeyArmor = "\"Armor\"";
        string myKeyClass = "\"Class\"";
        string myKeyStats = "\"Stats\"";


        private void showCustomStatsField() {
            myStrength = EditorGUILayout.IntField("Strength: ", myStrength);
            myDextery = EditorGUILayout.IntField("Dextery: ", myDextery);
            myInteligence = EditorGUILayout.IntField("Inteligence: ", myInteligence);

        }
        private void resetCustomStats() {
            myStrength = 0;
            myDextery = 0;
            myInteligence = 0;
        }

        private void showCustomStats() {
            myCustomStats = EditorGUILayout.Toggle("Edit stats: ", myCustomStats);

            if (myCustomStats) {
                showCustomStatsField();
            } else {
                resetCustomStats();
            }
        }

        private void showSpecialSettings() {
            showCustomStats();
        }

        #endregion

        #region ADMIN

        private void showAdminSettings() {
            showHeader("Admin settings");
            showadminFields();
        }

        private void showadminFields() {
            myAdminMode = EditorGUILayout.BeginToggleGroup("Enabled: ", myAdminMode);
            myOverPowered = EditorGUILayout.Slider("Overpowered: ", myOverPowered, 0, 3);
            myIsInmortal = EditorGUILayout.Toggle("Make Inmortal: ", myIsInmortal);

            EditorGUILayout.EndToggleGroup();
        }
        #endregion

        #region JSON

        private void showPlayerJSON() {
            showHeader("Player JSON");

            EditorGUILayout.TextArea(generateJSON(), textAreaStyle(), GUILayout.Height(50));
        }

        private string generateJSON() {
            string playerJSON = "{" + myKeyID + ":" + myPlayerId + "\"" + "," +
                                     myKeyHealth + ":" + "\"" + myPlayerHealth + "\"" + "," +
                                     myKeyArmor + ":" + "\"" + myPlayerArmor + "\"" + "," +
                                     myKeyClass + ":" + "\"" + myCharClass.ToString() + "\"}";

            return playerJSON;
        }

        #endregion

        #region UTILS
        private void showHeader(string name) {
            GUILayout.Space(10);
            GUILayout.Label(name, headerStyle());
            GUILayout.Space(10);
        }

        private GUIStyle headerStyle() {
            GUIStyle style = new GUIStyle();
            style.fontStyle = FontStyle.Bold;
            style.alignment = TextAnchor.MiddleCenter;

            return style;
        }

        private GUIStyle textAreaStyle() {
            GUIStyle style = EditorStyles.textArea;
            style.wordWrap = true;

            return style;
        }
        #endregion
    }
}
