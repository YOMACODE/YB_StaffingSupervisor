using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace YB_AssessmentManagement.Models
{
    public class AadhaarXML
    {
        public string AadhaarNumber { get; set; }
        public string SecretCode { get; set; }
        public IFormFile AadhaarXMLFile { get; set; }
    }

    [XmlRoot(ElementName = "OfflinePaperlessKyc")]
    public class OfflinePaperlessKyc
    {
        [XmlAttribute(AttributeName = "referenceId")]
        public string referenceId { get; set; }
        [XmlElement(ElementName = "UidData")]
        public UidData UidData { get; set; }
        [XmlElement(ElementName = "Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public Signature Signature { get; set; }
    }

    [XmlRoot(ElementName = "UidData")]
    public class UidData
    {
        [XmlElement(ElementName = "Poi")]
        public Poi Poi { get; set; }
        [XmlElement(ElementName = "Poa")]
        public Poa Poa { get; set; }
        [XmlElement(ElementName = "Pht")]
        public Pht Pht { get; set; }
    }

    [XmlRoot(ElementName = "Poi")]
    public class Poi
    {
        [XmlAttribute(AttributeName = "dob")]
        public string dob { get; set; }
        [XmlAttribute(AttributeName = "e")]
        public string e { get; set; }
        [XmlAttribute(AttributeName = "gender")]
        public string gender { get; set; }
        [XmlAttribute(AttributeName = "m")]
        public string m { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string name { get; set; }
    }

    [XmlRoot(ElementName = "Poa")]
    public class Poa
    {
        [XmlAttribute(AttributeName = "careof")]
        public string careof { get; set; }
        [XmlAttribute(AttributeName = "country")]
        public string country { get; set; }
        [XmlAttribute(AttributeName = "dist")]
        public string dist { get; set; }
        [XmlAttribute(AttributeName = "house")]
        public string house { get; set; }
        [XmlAttribute(AttributeName = "landmark")]
        public string landmark { get; set; }
        [XmlAttribute(AttributeName = "loc")]
        public string loc { get; set; }
        [XmlAttribute(AttributeName = "pc")]
        public string pc { get; set; }
        [XmlAttribute(AttributeName = "po")]
        public string po { get; set; }
        [XmlAttribute(AttributeName = "state")]
        public string state { get; set; }
        [XmlAttribute(AttributeName = "street")]
        public string street { get; set; }
        [XmlAttribute(AttributeName = "subdist")]
        public string subdist { get; set; }
        [XmlAttribute(AttributeName = "vtc")]
        public string vtc { get; set; }
    }

    [XmlRoot(ElementName = "Pht")]
    public class Pht
    {
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "Signature")]
    public class Signature
    {
        //[XmlAttribute(AttributeName = "xmlns")]
        //public string xmlns { get; set; }
        [XmlElement(ElementName = "SignedInfo")]
        public SignedInfo SignedInfo { get; set; }
        [XmlElement(ElementName = "SignatureValue")]
        public SignatureValue SignatureValue { get; set; }
    }
    [XmlRoot(ElementName = "SignatureValue")]
    public class SignatureValue
    {
        [XmlText]
        public string Text { get; set; }
    }
    [XmlRoot(ElementName = "SignedInfo")]
    public class SignedInfo
    {
        [XmlElement(ElementName = "CanonicalizationMethod")]
        public CanonicalizationMethod CanonicalizationMethod { get; set; }
        [XmlElement(ElementName = "SignatureMethod")]
        public SignatureMethod SignatureMethod { get; set; }
        [XmlElement(ElementName = "Reference")]
        public Reference Reference { get; set; }
    }
    [XmlRoot(ElementName = "CanonicalizationMethod")]
    public class CanonicalizationMethod
    {
        [XmlAttribute(AttributeName = "Algorithm")]
        public string Algorithm { get; set; }
    }
    [XmlRoot(ElementName = "SignatureMethod")]
    public class SignatureMethod
    {
        [XmlAttribute(AttributeName = "Algorithm")]
        public string Algorithm { get; set; }
    }
    [XmlRoot(ElementName = "Reference")]
    public class Reference
    {
        [XmlAttribute(AttributeName = "URI")]
        public string URI { get; set; }
        [XmlElement(ElementName = "Transforms")]
        public Transforms Transforms { get; set; }
        [XmlElement(ElementName = "DigestMethod")]
        public DigestMethod DigestMethod { get; set; }
        [XmlElement(ElementName = "DigestValue")]
        public DigestValue DigestValue { get; set; }
    }
    [XmlRoot(ElementName = "Transforms")]
    public class Transforms
    {
        [XmlElement(ElementName = "Transform")]
        public Transform Transform { get; set; }
    }
    [XmlRoot(ElementName = "Transform")]
    public class Transform
    {
        [XmlAttribute(AttributeName = "Algorithm")]
        public string Algorithm { get; set; }
    }
    [XmlRoot(ElementName = "DigestMethod")]
    public class DigestMethod
    {
        [XmlAttribute(AttributeName = "Algorithm")]
        public string Algorithm { get; set; }
    }
    [XmlRoot(ElementName = "DigestValue")]
    public class DigestValue
    {
        [XmlText]
        public string Text { get; set; }
    }
}