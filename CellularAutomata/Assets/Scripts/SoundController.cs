using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CellularAutomata
{
    public class SoundController : MonoBehaviour
    {
        [SerializeField]
        private AudioClip cellClick;

        private AudioSource audioSource;

        void OnEnable()
        {
            audioSource = gameObject.GetComponent<AudioSource>();
        }

        public void OnCellClicked()
        {
            audioSource.PlayOneShot(cellClick);
        }
    }
}