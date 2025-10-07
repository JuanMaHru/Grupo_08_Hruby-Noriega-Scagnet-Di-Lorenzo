using UnityEngine;

namespace TP03.Undo
{
    public class MoveCommand : ICommand
    {
        private Transform _target;
        private Vector3 _delta;
        private string _name;
        public string Name => _name;

        public MoveCommand(Transform target, Vector3 delta, string name = "Move")
        {
            _target = target;
            _delta = delta;
            _name = name;
        }

        public void Do()
        {
            if (_target == null) return;
            _target.position += _delta;
        }

        public void Undo()
        {
            if (_target == null) return;
            _target.position -= _delta;
        }
    }
}
