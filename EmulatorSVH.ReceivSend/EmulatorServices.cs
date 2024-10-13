
using System.Xml.Linq;
using ServerSVH.Application.Common;


using System.Xml.XPath;


using EmulatorSVH.ReceivSend.Consumer;
using EmulatorSVH.ReceivSend.Producer;
using System.Text;
using System.Xml.Xsl;
using System.Xml;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using static System.Runtime.InteropServices.JavaScript.JSType;
using EmulatorSVH.Application.Interface;


namespace EmulatorSVH.ReceivSend
{
    public class EmulatorServices(IRabbitMQConsumer rabbitMQConsumer,
             IMessagePublisher messagePublisher
             ) : IEmulatorServices
    {
        private readonly IRabbitMQConsumer _rabbitMQConsumer = rabbitMQConsumer;
        private readonly IMessagePublisher _messagePublisher = messagePublisher;
        public int LoadMessage()
        {
            int stPkg = 0;

            try
            {
                // получить сообщение с документом

                var resMessArch = _rabbitMQConsumer.LoadMessage("SendEmulArch");
                ResLoadMess resPkg = new();
                XDocument xPkg = new();
                // создать пакет и запустить workflow
                if (resMessArch != null)
                {
                    resPkg = ArchDocFromMessage(resMessArch);

                    if (resPkg != null)
                        _messagePublisher.SendMessage(CreateResultXml(resPkg).ToString(), "EmulSendDoc");

                }
                var resMessArmti = _rabbitMQConsumer.LoadMessage("SendEmulArmti");
                if (resMessArmti != null)
                {
                    _messagePublisher.SendMessage(ArmtiDocFromMessage(resMessArmti).ToString(), "EmulArmtiDoc");
                }
            }
            catch (Exception)
            {
                //string mess = ex.Message;

            }
            return stPkg;
        }
        private static XDocument ArmtiDocFromMessage(string resMessArmti)
        {
            string filexslt = "Workflow\\FTS\\ConfirmWHDocRegFromArmti.xsl";
            string resTransl = string.Empty;
            var inXmlPkg = XDocument.Load(resMessArmti);
            using (var stringReader = new StringReader(filexslt))
            {
                using XmlReader xsltReader = XmlReader.Create(stringReader);
                var transformer = new XslCompiledTransform();
                transformer.Load(xsltReader);
                XsltArgumentList args = new();
                args.AddParam("docid", "", Guid.NewGuid().ToString());
                args.AddParam("Now", "", DateTime.Now.ToString());
                using XmlReader oldDocumentReader = inXmlPkg.CreateReader();

                StringBuilder resultStr = new(1024 * 1024 * 10);
                XmlWriter resultWriter = XmlWriter.Create(resultStr);
                transformer.Transform(oldDocumentReader, args, resultWriter);
                resultWriter?.Close();
                resTransl = resultStr.ToString();
            }

            XDocument xRes = XDocument.Load(resTransl);
            return xRes;
        }
        private static XDocument CreateResultXml(ResLoadMess resPkg)
        {
            string resXml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            resXml += "<ArchResult DocumentModeID=\"1005016E\" uuid=" + resPkg.UUID + ">";
            resXml += "<DocumentID>" + resPkg.DocId + "</DocumentID>";
            resXml += "<Response><ResultInformation><ResultCode>0000</ResultCode>";
            resXml += "<EADInfo><ArchDeclID>" + Guid.NewGuid().ToString() + "</ArchDeclID>";
            resXml += "<ArchID>" + Guid.NewGuid().ToString() + "</ArchID><ArchDocID>" + Guid.NewGuid().ToString() + "</ArchDocID>";
            resXml += "<ArchDocStatus>LOAD</ArchDocStatus><DocCode>" + resPkg.CodeDoc + "</DocCode>";
            resXml += "<DocNum>" + resPkg.NumDoc + "</DocNum><DocDate>" + resPkg.DateDoc + "</DocDate>";
            resXml += "</EADInfo></ResultInformation></Response></ArchResult>";
            XDocument xRes = XDocument.Load(resXml);
            return xRes;
        }

        private static ResLoadMess ArchDocFromMessage(string Mess)
        {
            ResLoadMess resPkg = new();
            try
            {
                XDocument xMess = XDocument.Load(Mess);
                var xPkg = xMess.Element("Package")?.Elements("*");
                if (xPkg is not null)
                {
                    var propsPkg = xMess.Elements("Package")?.Elements("package-properties");


                    var uuidPkg = propsPkg?.Elements("name").Where(s => s.Attribute("uuid")?.Value is not null).ToString();
                    if (uuidPkg is not null) resPkg.UUID = uuidPkg;

                    var uuidDoc = xPkg.Where(d => d.Attribute("docid")?.Value is not null).ToString();
                    if (uuidDoc is not null) resPkg.DocId = uuidDoc;

                    var xDoc = xPkg.Where(d => d.Attribute("arch")?.Value == "docCode");
                    if (xDoc is not null)
                    {
                        XDocument xArch = new();
                        xArch.Add(xDoc);
                        var dCodeAtt = xArch.XPathSelectElement("//*@[local-name()='docCode']")?.ToString();
                        if (dCodeAtt is not null) resPkg.CodeDoc = dCodeAtt;
                        var dNum = xArch.XPathSelectElement("//*[local-name()='RegistrationDocument']/*[local-name()='PrDocumentNumber']")?.ToString();
                        var dNumAtt = xArch.XPathSelectElement("//*@[local-name()='InvDocNumber']")?.ToString();
                        if (dNum is not null) resPkg.NumDoc = dNum;
                        if (dNumAtt is not null) resPkg.NumDoc = dNumAtt;

                        var dDat = xArch.XPathSelectElement("//*[local-name()='RegistrationDocument']/*[local-name()='PrDocumentDate']")?.ToString();
                        var dDatAtt = xArch.XPathSelectElement("//*@[local-name()='InvDocDate']")?.ToString();
                        if (dDat is not null) resPkg.DateDoc = dDat;
                        if (dDatAtt is not null) resPkg.DateDoc = dDatAtt;

                    }

                }

            }
            catch (Exception)
            {

            }

            return resPkg;
        }

    }
}


