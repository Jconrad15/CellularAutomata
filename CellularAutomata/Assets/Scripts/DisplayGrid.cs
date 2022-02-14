using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CellularAutomata
{
    public class DisplayGrid : MonoBehaviour
    {
        [SerializeField]
        GridController gridController;

        // Start is called before the first frame update
        void Start()
        {
            if (gridController == null)
            {
                Debug.LogError("Forgot to assign GridController in editor.");
            }



        }





    }
}