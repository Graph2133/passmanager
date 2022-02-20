using PassManager.Core.Repository;
using PassManager.Core.VirtualDirectory;
using Spectre.Console.Cli;

namespace PassManager.Console.Commands
{
    public abstract class BasePasswordCommand : Command
    {
        protected readonly IPasswordManager passwordManager;

        public BasePasswordCommand(IPasswordManager passwordManager)
        {
            this.passwordManager = passwordManager;
        }
    }
}
