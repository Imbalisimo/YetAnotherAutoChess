﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YetAnotherAutoChess.PlayerServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CompositeType", Namespace="http://schemas.datacontract.org/2004/07/AutoChessPlayerLibrary")]
    [System.SerializableAttribute()]
    public partial class CompositeType : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool BoolValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StringValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool BoolValue {
            get {
                return this.BoolValueField;
            }
            set {
                if ((this.BoolValueField.Equals(value) != true)) {
                    this.BoolValueField = value;
                    this.RaisePropertyChanged("BoolValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StringValue {
            get {
                return this.StringValueField;
            }
            set {
                if ((object.ReferenceEquals(this.StringValueField, value) != true)) {
                    this.StringValueField = value;
                    this.RaisePropertyChanged("StringValue");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BaseUnitPackage", Namespace="http://schemas.datacontract.org/2004/07/AutoChessPlayerLibrary")]
    [System.SerializableAttribute()]
    public partial class BaseUnitPackage : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CostField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Cost {
            get {
                return this.CostField;
            }
            set {
                if ((this.CostField.Equals(value) != true)) {
                    this.CostField = value;
                    this.RaisePropertyChanged("Cost");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FigurePackage", Namespace="http://schemas.datacontract.org/2004/07/AutoChessPlayerLibrary")]
    [System.SerializableAttribute()]
    public partial class FigurePackage : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NewColumnField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NewRowField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int OriginalColumnField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int OriginalRowField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PieceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool PieceToggledField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StarField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int NewColumn {
            get {
                return this.NewColumnField;
            }
            set {
                if ((this.NewColumnField.Equals(value) != true)) {
                    this.NewColumnField = value;
                    this.RaisePropertyChanged("NewColumn");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int NewRow {
            get {
                return this.NewRowField;
            }
            set {
                if ((this.NewRowField.Equals(value) != true)) {
                    this.NewRowField = value;
                    this.RaisePropertyChanged("NewRow");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int OriginalColumn {
            get {
                return this.OriginalColumnField;
            }
            set {
                if ((this.OriginalColumnField.Equals(value) != true)) {
                    this.OriginalColumnField = value;
                    this.RaisePropertyChanged("OriginalColumn");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int OriginalRow {
            get {
                return this.OriginalRowField;
            }
            set {
                if ((this.OriginalRowField.Equals(value) != true)) {
                    this.OriginalRowField = value;
                    this.RaisePropertyChanged("OriginalRow");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Piece {
            get {
                return this.PieceField;
            }
            set {
                if ((object.ReferenceEquals(this.PieceField, value) != true)) {
                    this.PieceField = value;
                    this.RaisePropertyChanged("Piece");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool PieceToggled {
            get {
                return this.PieceToggledField;
            }
            set {
                if ((this.PieceToggledField.Equals(value) != true)) {
                    this.PieceToggledField = value;
                    this.RaisePropertyChanged("PieceToggled");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Star {
            get {
                return this.StarField;
            }
            set {
                if ((object.ReferenceEquals(this.StarField, value) != true)) {
                    this.StarField = value;
                    this.RaisePropertyChanged("Star");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PlayerPackage", Namespace="http://schemas.datacontract.org/2004/07/AutoChessPlayerLibrary")]
    [System.SerializableAttribute()]
    public partial class PlayerPackage : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private YetAnotherAutoChess.PlayerServiceReference.FigurePackage[] FiguresField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int HpField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IP_adressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int MoneyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PortField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UsernameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public YetAnotherAutoChess.PlayerServiceReference.FigurePackage[] Figures {
            get {
                return this.FiguresField;
            }
            set {
                if ((object.ReferenceEquals(this.FiguresField, value) != true)) {
                    this.FiguresField = value;
                    this.RaisePropertyChanged("Figures");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Hp {
            get {
                return this.HpField;
            }
            set {
                if ((this.HpField.Equals(value) != true)) {
                    this.HpField = value;
                    this.RaisePropertyChanged("Hp");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string IP_adress {
            get {
                return this.IP_adressField;
            }
            set {
                if ((object.ReferenceEquals(this.IP_adressField, value) != true)) {
                    this.IP_adressField = value;
                    this.RaisePropertyChanged("IP_adress");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Money {
            get {
                return this.MoneyField;
            }
            set {
                if ((this.MoneyField.Equals(value) != true)) {
                    this.MoneyField = value;
                    this.RaisePropertyChanged("Money");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password {
            get {
                return this.PasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordField, value) != true)) {
                    this.PasswordField = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Port {
            get {
                return this.PortField;
            }
            set {
                if ((object.ReferenceEquals(this.PortField, value) != true)) {
                    this.PortField = value;
                    this.RaisePropertyChanged("Port");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Username {
            get {
                return this.UsernameField;
            }
            set {
                if ((object.ReferenceEquals(this.UsernameField, value) != true)) {
                    this.UsernameField = value;
                    this.RaisePropertyChanged("Username");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="PlayerServiceReference.IPlayerService")]
    public interface IPlayerService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPlayerService/GetData", ReplyAction="http://tempuri.org/IPlayerService/GetDataResponse")]
        string GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPlayerService/GetData", ReplyAction="http://tempuri.org/IPlayerService/GetDataResponse")]
        System.Threading.Tasks.Task<string> GetDataAsync(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPlayerService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IPlayerService/GetDataUsingDataContractResponse")]
        YetAnotherAutoChess.PlayerServiceReference.CompositeType GetDataUsingDataContract(YetAnotherAutoChess.PlayerServiceReference.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPlayerService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IPlayerService/GetDataUsingDataContractResponse")]
        System.Threading.Tasks.Task<YetAnotherAutoChess.PlayerServiceReference.CompositeType> GetDataUsingDataContractAsync(YetAnotherAutoChess.PlayerServiceReference.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPlayerService/RequestRandomUnitsFromPool", ReplyAction="http://tempuri.org/IPlayerService/RequestRandomUnitsFromPoolResponse")]
        YetAnotherAutoChess.PlayerServiceReference.BaseUnitPackage[] RequestRandomUnitsFromPool(int playerLevel, int count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPlayerService/RequestRandomUnitsFromPool", ReplyAction="http://tempuri.org/IPlayerService/RequestRandomUnitsFromPoolResponse")]
        System.Threading.Tasks.Task<YetAnotherAutoChess.PlayerServiceReference.BaseUnitPackage[]> RequestRandomUnitsFromPoolAsync(int playerLevel, int count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPlayerService/MoveUnit", ReplyAction="http://tempuri.org/IPlayerService/MoveUnitResponse")]
        void MoveUnit(string playerUsername, YetAnotherAutoChess.PlayerServiceReference.FigurePackage unit);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPlayerService/MoveUnit", ReplyAction="http://tempuri.org/IPlayerService/MoveUnitResponse")]
        System.Threading.Tasks.Task MoveUnitAsync(string playerUsername, YetAnotherAutoChess.PlayerServiceReference.FigurePackage unit);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPlayerService/ReturnUnitToPool", ReplyAction="http://tempuri.org/IPlayerService/ReturnUnitToPoolResponse")]
        void ReturnUnitToPool(YetAnotherAutoChess.PlayerServiceReference.BaseUnitPackage unit);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPlayerService/ReturnUnitToPool", ReplyAction="http://tempuri.org/IPlayerService/ReturnUnitToPoolResponse")]
        System.Threading.Tasks.Task ReturnUnitToPoolAsync(YetAnotherAutoChess.PlayerServiceReference.BaseUnitPackage unit);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPlayerService/RegisterPlayer", ReplyAction="http://tempuri.org/IPlayerService/RegisterPlayerResponse")]
        void RegisterPlayer(YetAnotherAutoChess.PlayerServiceReference.PlayerPackage myself);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPlayerService/RegisterPlayer", ReplyAction="http://tempuri.org/IPlayerService/RegisterPlayerResponse")]
        System.Threading.Tasks.Task RegisterPlayerAsync(YetAnotherAutoChess.PlayerServiceReference.PlayerPackage myself);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPlayerService/GetOpponentsUnits", ReplyAction="http://tempuri.org/IPlayerService/GetOpponentsUnitsResponse")]
        YetAnotherAutoChess.PlayerServiceReference.FigurePackage[] GetOpponentsUnits(string myUsername);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPlayerService/GetOpponentsUnits", ReplyAction="http://tempuri.org/IPlayerService/GetOpponentsUnitsResponse")]
        System.Threading.Tasks.Task<YetAnotherAutoChess.PlayerServiceReference.FigurePackage[]> GetOpponentsUnitsAsync(string myUsername);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPlayerService/TakeDamage", ReplyAction="http://tempuri.org/IPlayerService/TakeDamageResponse")]
        void TakeDamage(string username, int hp);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPlayerService/TakeDamage", ReplyAction="http://tempuri.org/IPlayerService/TakeDamageResponse")]
        System.Threading.Tasks.Task TakeDamageAsync(string username, int hp);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPlayerServiceChannel : YetAnotherAutoChess.PlayerServiceReference.IPlayerService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PlayerServiceClient : System.ServiceModel.ClientBase<YetAnotherAutoChess.PlayerServiceReference.IPlayerService>, YetAnotherAutoChess.PlayerServiceReference.IPlayerService {
        
        public PlayerServiceClient() {
        }
        
        public PlayerServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PlayerServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PlayerServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PlayerServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetData(int value) {
            return base.Channel.GetData(value);
        }
        
        public System.Threading.Tasks.Task<string> GetDataAsync(int value) {
            return base.Channel.GetDataAsync(value);
        }
        
        public YetAnotherAutoChess.PlayerServiceReference.CompositeType GetDataUsingDataContract(YetAnotherAutoChess.PlayerServiceReference.CompositeType composite) {
            return base.Channel.GetDataUsingDataContract(composite);
        }
        
        public System.Threading.Tasks.Task<YetAnotherAutoChess.PlayerServiceReference.CompositeType> GetDataUsingDataContractAsync(YetAnotherAutoChess.PlayerServiceReference.CompositeType composite) {
            return base.Channel.GetDataUsingDataContractAsync(composite);
        }
        
        public YetAnotherAutoChess.PlayerServiceReference.BaseUnitPackage[] RequestRandomUnitsFromPool(int playerLevel, int count) {
            return base.Channel.RequestRandomUnitsFromPool(playerLevel, count);
        }
        
        public System.Threading.Tasks.Task<YetAnotherAutoChess.PlayerServiceReference.BaseUnitPackage[]> RequestRandomUnitsFromPoolAsync(int playerLevel, int count) {
            return base.Channel.RequestRandomUnitsFromPoolAsync(playerLevel, count);
        }
        
        public void MoveUnit(string playerUsername, YetAnotherAutoChess.PlayerServiceReference.FigurePackage unit) {
            base.Channel.MoveUnit(playerUsername, unit);
        }
        
        public System.Threading.Tasks.Task MoveUnitAsync(string playerUsername, YetAnotherAutoChess.PlayerServiceReference.FigurePackage unit) {
            return base.Channel.MoveUnitAsync(playerUsername, unit);
        }
        
        public void ReturnUnitToPool(YetAnotherAutoChess.PlayerServiceReference.BaseUnitPackage unit) {
            base.Channel.ReturnUnitToPool(unit);
        }
        
        public System.Threading.Tasks.Task ReturnUnitToPoolAsync(YetAnotherAutoChess.PlayerServiceReference.BaseUnitPackage unit) {
            return base.Channel.ReturnUnitToPoolAsync(unit);
        }
        
        public void RegisterPlayer(YetAnotherAutoChess.PlayerServiceReference.PlayerPackage myself) {
            base.Channel.RegisterPlayer(myself);
        }
        
        public System.Threading.Tasks.Task RegisterPlayerAsync(YetAnotherAutoChess.PlayerServiceReference.PlayerPackage myself) {
            return base.Channel.RegisterPlayerAsync(myself);
        }
        
        public YetAnotherAutoChess.PlayerServiceReference.FigurePackage[] GetOpponentsUnits(string myUsername) {
            return base.Channel.GetOpponentsUnits(myUsername);
        }
        
        public System.Threading.Tasks.Task<YetAnotherAutoChess.PlayerServiceReference.FigurePackage[]> GetOpponentsUnitsAsync(string myUsername) {
            return base.Channel.GetOpponentsUnitsAsync(myUsername);
        }
        
        public void TakeDamage(string username, int hp) {
            base.Channel.TakeDamage(username, hp);
        }
        
        public System.Threading.Tasks.Task TakeDamageAsync(string username, int hp) {
            return base.Channel.TakeDamageAsync(username, hp);
        }
    }
}
