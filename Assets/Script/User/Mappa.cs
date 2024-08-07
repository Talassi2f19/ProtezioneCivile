using System;
using UnityEngine;

namespace Script.User
{
    public class Mappa : MonoBehaviour
    {
        [SerializeField] private Canvas mainCanvas;
        [SerializeField] private GameObject objGrande;
        [SerializeField] private PlayerLocal playerLocal;
        [SerializeField] private GameObject darkBack;

        private void Start()
        {
            Exit();
        }


        public void Enter()
        {
            mainCanvas.enabled = false;
            objGrande.SetActive(true);
            darkBack.SetActive(true);
            playerLocal.canMove = false;
        }

        public void Exit()
        {
            mainCanvas.enabled = true;
            objGrande.SetActive(false);
            darkBack.SetActive(false);
            playerLocal.canMove = true;
        }



    }
}
