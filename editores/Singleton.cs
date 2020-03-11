using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils {
    /// <summary>
    /// Class used to simulate singleton behaviour
    /// </summary>
    /// <typeparam name="T">Class to be singleted</typeparam>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
        protected static T myInstance;

        /// <summary>
        /// Get Instance
        /// </summary>
        public static T Instance {
            get
            {
                // If no reference found, search it
                if(myInstance == null) {
                    myInstance = (T)FindObjectOfType(typeof(T));
                }

                return myInstance;
            }
        }
    }
}
