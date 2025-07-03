using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppDiplomaDemo.HttpCommands;
using WpfAppDiplomaDemo.JsonModels;

namespace WpfAppDiplomaDemo.Command
{
    public class ChangingSystem
    {
        private int startNumber = 0;
        private int endNumber = 10;
        private int currentNumber = 0;
        private Guid idCurrentUser;
        private Dictionary<int, Guid> userIds = new Dictionary<int, Guid>();
        private List<ConnectionInfo> connectionInfos;

        public ChangingSystem(Guid idUser) 
        {
            idCurrentUser = idUser;
        }
        private async Task UpdateStartEndNumber()
        {
            startNumber = endNumber;
            endNumber += 10;

            await SetIds();
        }
        public async Task<Guid> SetIds()
        {
            List<Guid> ids = new List<Guid>();
            List<ConnectionInfo> connectionInfos = await Commands.GetConnectionInfos(idCurrentUser, startNumber, endNumber);
            if (connectionInfos.Count == 0)
            {
                ids = await Commands.GetUserIds(startNumber, endNumber);
            }
            else
            {
                ids = connectionInfos.Select(c => c.IdSecondUser).ToList();
            }
            int numberOfId = currentNumber;
            foreach (var id in ids)
            {
                userIds[numberOfId] = id;
                numberOfId++;
            }
            return await GetId();
        }

        public async Task<Guid> GoNext()
        {
            if (userIds.Count == 0) return Guid.Empty;
            currentNumber++;
            if (currentNumber > endNumber)
            {
                await UpdateStartEndNumber();
            }
            return await GetId();
        }

        public async Task<Guid> GetId()
        {
            return userIds[currentNumber];
        }
    }
}
