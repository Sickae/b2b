namespace B2B.Logic.BusinessLogic.Base.Command
{
    public class CommandResult : ICommandResult
    {
        public bool Success { get; init; }
        public string ErrorMessage { get; init; }
    }

    public interface ICommandResult
    {
        bool Success { get; init; }
        string ErrorMessage { get; init; }
    }
}
