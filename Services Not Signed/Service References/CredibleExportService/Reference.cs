﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Services_Not_Signed.CredibleExportService {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="https://www.crediblebh.com/", ConfigurationName="CredibleExportService.ExportServiceSoap")]
    public interface ExportServiceSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.crediblebh.com/ExportDataSet", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet ExportDataSet(string connection, string start_date, string end_date, string custom_param1, string custom_param2, string custom_param3);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.crediblebh.com/ExportDataSet", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> ExportDataSetAsync(string connection, string start_date, string end_date, string custom_param1, string custom_param2, string custom_param3);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.crediblebh.com/ExportXML", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string ExportXML(string connection, string start_date, string end_date, string custom_param1, string custom_param2, string custom_param3);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.crediblebh.com/ExportXML", ReplyAction="*")]
        System.Threading.Tasks.Task<string> ExportXMLAsync(string connection, string start_date, string end_date, string custom_param1, string custom_param2, string custom_param3);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ExportServiceSoapChannel : Services_Not_Signed.CredibleExportService.ExportServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ExportServiceSoapClient : System.ServiceModel.ClientBase<Services_Not_Signed.CredibleExportService.ExportServiceSoap>, Services_Not_Signed.CredibleExportService.ExportServiceSoap {
        
        public ExportServiceSoapClient() {
        }
        
        public ExportServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ExportServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ExportServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ExportServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Data.DataSet ExportDataSet(string connection, string start_date, string end_date, string custom_param1, string custom_param2, string custom_param3) {
            return base.Channel.ExportDataSet(connection, start_date, end_date, custom_param1, custom_param2, custom_param3);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> ExportDataSetAsync(string connection, string start_date, string end_date, string custom_param1, string custom_param2, string custom_param3) {
            return base.Channel.ExportDataSetAsync(connection, start_date, end_date, custom_param1, custom_param2, custom_param3);
        }
        
        public string ExportXML(string connection, string start_date, string end_date, string custom_param1, string custom_param2, string custom_param3) {
            return base.Channel.ExportXML(connection, start_date, end_date, custom_param1, custom_param2, custom_param3);
        }
        
        public System.Threading.Tasks.Task<string> ExportXMLAsync(string connection, string start_date, string end_date, string custom_param1, string custom_param2, string custom_param3) {
            return base.Channel.ExportXMLAsync(connection, start_date, end_date, custom_param1, custom_param2, custom_param3);
        }
    }
}
