using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml;

namespace PatientDemo.Models
{
    public class PatientRecord
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Forenames { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Surname { get; set; }

        public System.DateTime Date_of_Birth { get; set; }

        [Required]
        public string Gender { get; set; }

        public PatientElectronicAddressCollection TelephoneNumbers { get; set; }
    }

    public class PatientElectronicAddressCollection : System.Collections.CollectionBase
    {
        public Int32 Add(PatientElectronicAddressCustom pea)
        {
            return List.Add(pea);
        }

        public PatientElectronicAddressCustom this[int index]
        {
            get { return (PatientElectronicAddressCustom)List[index]; }
        }

        public void Remove(Int32 index)
        {
            if (index > Count - 1 || index < 0)
                throw new IndexOutOfRangeException();
            else
                List.RemoveAt(index);
        }
    }

    public class PatientElectronicAddressCustom
    {
        public string ElectronicAddressType { get; set; }
        public string Telephone_number { get; set; }
    }

    // helper class to ignore namespaces when de-serializing
    public class NamespaceIgnorantXmlTextReader : XmlTextReader
    {
        public NamespaceIgnorantXmlTextReader(System.IO.TextReader reader) : base(reader) { }

        public override string NamespaceURI
        {
            get { return ""; }
        }
    }
}
