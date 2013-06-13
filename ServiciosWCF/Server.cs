using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Runtime.Serialization;

namespace ServiciosWCF
{
    [DataContract]
    public class PlayerOnline
    {
        [DataMember]
        private String name;
        [DataMember]
        private int level;
        [DataMember]
        private Boolean ready;
        [DataMember]
        private Boolean finish;

        public PlayerOnline(String name,int level)
        {
            this.name = name;
            this.level = level;
        }
    }

    [ServiceContract]
    public interface IServices
    {
        [OperationContract]
        void AddPlayer(String nickname, int level);

        [OperationContract]
        List<PlayerOnline> ShowOnlinePlayers();
        
        [OperationContract]
        List<PlayerOnline> ShowOnlinePlayersByLevel(int level);
        
        [OperationContract]
        void SendVSRequest(int nPlayer);

        [OperationContract]
        void GetVSRequest(int nPlayer);
   
    }

}
