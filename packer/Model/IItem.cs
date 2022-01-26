namespace Packer.Model
{
    public interface IItem
    {
        double Cost { get; }
        int Id { get; }
        bool IsSelected { get; }
        double Profit { get; }
        double Weight { get; }

        void Select();
    }
}