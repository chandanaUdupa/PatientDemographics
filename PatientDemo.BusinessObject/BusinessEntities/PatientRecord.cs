using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PatientDemo.BusinessObject.BusinessEntities
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

    [CollectionDataContract]
    [KnownType(typeof(PatientDemo.BusinessObject.BusinessEntities.PatientElectronicAddressCustom))]
    [XmlRoot(ElementName = "Telephone Numbers")]
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

        public void Remove(PatientElectronicAddressCustom pea)
        {
                List.Remove(pea);
        }
    }

    [KnownType(typeof(PatientDemo.BusinessObject.BusinessEntities.PatientElectronicAddressCustom))]
    [XmlRoot(ElementName = "Phone Number")]
    public class PatientElectronicAddressCustom
    {
        public string ElectronicAddressType { get; set; }
        public string Telephone_number { get; set; }
    }
}
