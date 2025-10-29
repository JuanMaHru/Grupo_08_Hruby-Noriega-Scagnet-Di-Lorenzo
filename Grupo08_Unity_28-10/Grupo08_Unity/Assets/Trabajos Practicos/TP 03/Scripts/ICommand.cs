namespace TP03.Undo
{
    public interface ICommand
    {
        void Do();
        void Undo();
        string Name { get; }
    }
}
