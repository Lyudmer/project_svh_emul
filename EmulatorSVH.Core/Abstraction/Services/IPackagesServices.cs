﻿using ClientSVH.Core.Models;
using System.Xml.Linq;

namespace ClientSVH.Core.Abstraction.Services
{
    public interface IPackagesServices
    {
    
        Task<int> LoadFile(Guid UserId, string xFile);
      
        Task<int> SendToServer(int Pid);
        Task<bool> SendDelPkgToServer(int Pid);
        Task<HistoryPkg> HistoriPkgByPid(int Pid);
        Task<Package> GetPkgId(int Pid);
        Task<List<Document>> GetDocsPkg(int Pid);
        Task DeletePkg(int Pid);
    }
}