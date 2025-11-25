using UnityEngine;
using UnityEngine.UI;
using TP03;            

namespace TP03.Undo
{
    public class UndoManager : MonoBehaviour
    {
        [Header("UI (optional)")]
        [SerializeField] private Button undoButton;
        [SerializeField] private Text lastActionText;

        private MyStack<ICommand> _history;

        private void Awake()
        {
            _history = new MyStack<ICommand>();
            if (undoButton != null) undoButton.onClick.AddListener(UndoLast);
            RefreshUI();
        }

        public void Execute(ICommand command)
        {
            command.Do();
            _history.Push(command);
            RefreshUI();
        }

        public void UndoLast()
        {
            if (_history.TryPop(out var cmd))
            {
                cmd.Undo();
            }
            RefreshUI();
        }

        private void RefreshUI()
        {
            if (lastActionText == null) return;

            if (_history.TryPeek(out var top))
                lastActionText.text = "Last Action: " + top.Name;
            else
                lastActionText.text = "Last Action: (none)";
        }
    }
}
