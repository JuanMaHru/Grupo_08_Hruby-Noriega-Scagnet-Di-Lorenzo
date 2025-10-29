using UnityEngine;

namespace TP03.Undo
{
    public class MoveWithUndoExample : MonoBehaviour
    {
        [SerializeField] private UndoManager undoManager;
        [SerializeField] private Transform target;   // Objeto a mover
        [SerializeField] private float step = 1f;

        private void Update()
        {
            if (undoManager == null || target == null) return;

            // Acciones con flechas/WASD
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                undoManager.Execute(new MoveCommand(target, new Vector3(step, 0, 0), "Move +X"));

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                undoManager.Execute(new MoveCommand(target, new Vector3(-step, 0, 0), "Move -X"));

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                undoManager.Execute(new MoveCommand(target, new Vector3(0, 0, step), "Move +Z"));

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                undoManager.Execute(new MoveCommand(target, new Vector3(0, 0, -step), "Move -Z"));

            // Undo con tecla U (adem�s del bot�n de UI)
            if (Input.GetKeyDown(KeyCode.U))
                undoManager.UndoLast();
        }
    }
}
