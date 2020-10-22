using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AutoChessPlayerLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IPlayerService
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);


        #region Units
        [OperationContract]
        List<BaseUnitPackage> RequestRandomUnitsFromPool(int playerLevel, int count);

        [OperationContract]
        void MoveUnit(string playerUsername, FigurePackage unit);

        [OperationContract]
        void ReturnUnitToPool(BaseUnitPackage unit);
        #endregion

        #region Players
        [OperationContract]
        void RegisterPlayer(PlayerPackage myself);

        [OperationContract]
        List<FigurePackage> GetOpponentsUnits(string myUsername);

        [OperationContract]
        void TakeDamage(string username, int hp);
        #endregion

    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "AutoChessPlayerLibrary.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
