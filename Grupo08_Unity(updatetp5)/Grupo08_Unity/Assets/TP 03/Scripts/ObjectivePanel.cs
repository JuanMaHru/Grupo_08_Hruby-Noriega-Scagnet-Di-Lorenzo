using UnityEngine;
using UnityEngine.UI;
using TP03;                    // para MyQueue<T>
using TP03.Objectives;

namespace TP03.Objectives
{
    public class ObjectivePanel : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Text currentObjectiveText;
        [SerializeField] private Button completeButton;

        private MyQueue<Objective> _queue;

        private void Awake()
        {
            _queue = new MyQueue<Objective>();
            if (completeButton != null) completeButton.onClick.AddListener(CompleteCurrentObjective);
        }

        // Llenar la cola desde el editor o por código
        public void Initialize(Objective[] objectives)
        {
            foreach (var obj in objectives)
                _queue.Enqueue(obj);

            RefreshUI();
        }

        private void CompleteCurrentObjective()
        {
            if (_queue.TryDequeue(out var _))
            {
                RefreshUI();
            }
        }

        private void RefreshUI()
        {
            if (_queue.TryPeek(out var current))
            {
                currentObjectiveText.text = "Current Objective:\n" + current.description;
                completeButton.interactable = true;
            }
            else
            {
                currentObjectiveText.text = "All objectives completed!";
                completeButton.interactable = false;
            }
        }
    }
}
