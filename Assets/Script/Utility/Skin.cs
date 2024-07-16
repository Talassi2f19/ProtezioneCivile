using System;
using UnityEditor;
using UnityEngine;

namespace Script.Utility
{
    public class Skin : MonoBehaviour
    {
        [SerializeField]private Animator animator;
        [SerializeField]private AnimatorOverrideController coc;
        [SerializeField]private AnimatorOverrideController cri;
        [SerializeField]private AnimatorOverrideController ggev;
        [SerializeField]private AnimatorOverrideController giornalista;
        [SerializeField]private AnimatorOverrideController medico;
        [SerializeField]private AnimatorOverrideController pc;
        [SerializeField]private AnimatorOverrideController pompiere;
        [SerializeField]private AnimatorOverrideController segreteria;
        [SerializeField]private AnimatorOverrideController sindaco;
        [SerializeField]private AnimatorOverrideController tlc;
        [SerializeField]private AnimatorOverrideController vigile;
        [SerializeField]private AnimatorOverrideController normale;
        
        
        public Ruoli r;
        [ContextMenu("prova")]
        public void aggiorna()
        {
                SetSkin(r);
           
        }
        
        public void SetSkin(Ruoli ruolo)
        {
            AnimatorOverrideController tmp;
            switch (ruolo)
            {
                case Ruoli.Coc:
                    tmp = coc;
                    break;
                case Ruoli.RefCri:
                case Ruoli.VolCri:
                    tmp = cri;
                    break;
                case Ruoli.RefGgev:
                case Ruoli.VolGgev:
                    tmp = ggev;
                    break;
                case Ruoli.RefPC:
                case Ruoli.VolPC:
                    tmp = pc;
                    break;
                case Ruoli.RefPolizia:
                case Ruoli.VolPolizia:
                    tmp = vigile;
                    break;
                case Ruoli.VolFuoco:
                case Ruoli.RefFuoco:
                    tmp = pompiere;
                    break;
                case Ruoli.Sindaco:
                    tmp = sindaco;
                    break;
                case Ruoli.Medico:
                    tmp = medico;
                    break;
                // case Ruoli.Giornalista:
                //     animator.runtimeAnimatorController = giornalista;
                //     break;
                case Ruoli.Segreteria:
                    tmp = segreteria;
                    break;
                case Ruoli.RefTlc:
                    tmp = tlc;
                    break;
                default:
                    tmp = normale;
                    break;
            }

            animator.runtimeAnimatorController = tmp;
        }
    }
}
