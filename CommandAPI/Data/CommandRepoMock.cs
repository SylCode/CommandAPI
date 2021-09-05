using CommandAPI.Models;
using System.Collections.Generic;

namespace CommandAPI.Data
{
    public class CommandRepoMock : ICommanderRepo
    {
        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return new Command[] {
                new Command { Id = 0, HowTo = "Test", Line = "ipconfig -all", Platform = "Windows" },
                new Command { Id = 1, HowTo = "Test1", Line = "ipconfig -all", Platform = "Windows"},
                new Command { Id = 2, HowTo = "Test2", Line = "ipconfig -all", Platform = "Windows"},
                new Command { Id = 3, HowTo = "Test3", Line = "ipconfig -all", Platform = "Windows"},
                new Command { Id = 4, HowTo = "Test4", Line = "ipconfig -all", Platform = "Windows"}
            };
        }

        public Command GetCommandById(int id)
        {
            return new Command { Id = 0, HowTo = "Test", Line = "ipconfig -all", Platform = "Windows" };
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}