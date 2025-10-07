using UnityEngine;

namespace TP03.Objectives
{
    public class ObjectiveBootstrap : MonoBehaviour
    {
        [SerializeField] private ObjectivePanel panel;
        [SerializeField] private Objective[] objectives;

        private void Start()
        {
            if (panel != null)
                panel.Initialize(objectives);
        }
    }
}
