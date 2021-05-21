
namespace Models
{
    public interface IShell
    {
        string StatusText { get; set; }
        bool StatusExecutable { get; set; }
    }
}
