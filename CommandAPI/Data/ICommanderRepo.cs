using CommandAPI.Models;
using System.Collections.Generic;

namespace CommandAPI.Data
{
    public interface ICommanderRepo
    {
        bool SaveChanges();
        public IEnumerable<Command> GetAllCommands();
        public Command GetCommandById(int id);
        void CreateCommand(Command cmd);
        void UpdateCommand(Command cmd);
        void DeleteCommand(Command cmd);
    }
}
