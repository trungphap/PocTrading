
namespace Models
{
    public interface IShell
    {
        string StatusText { get; set; }
        string FontText { get; set; }
        bool StatusExecutable { get; set; }
        bool IsExecuting { get; set; }
    }
}
