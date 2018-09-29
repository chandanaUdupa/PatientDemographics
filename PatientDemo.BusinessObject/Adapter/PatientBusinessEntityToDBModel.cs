using PatientDemo.BusinessObject.BusinessEntities;
using PatientDemo.BusinessObject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDemo.BusinessObject.Adapter
{
    public class PatientBusinessEntityToDBModel
    {
        /// <summary>
        /// To convert the PatientRecord (model object)
        /// to Patient (db object)
        /// </summary>
        /// <param name="patientRecord"></param>
        /// <param name="id"></param>
        /// <returns>Patient</returns>
        public static Patient ConvertPatientRecordToPatient(PatientRecord patientRecord, int? id = null)
        {
            Patient patient = new Patient();
            patient.Surname = patientRecord.Surname;
            patient.Date_of_Birth = patientRecord.Date_of_Birth;
            patient.Forenames = patientRecord.Forenames;
            patient.Gender = patientRecord.Gender;
            if (id.HasValue)
                patient.Id = id.Value;
            if (patientRecord.TelephoneNumbers != null)
            {
                ICollection<PatientElectronicAddress> patientElectronicAddresses = new List<PatientElectronicAddress>();

                foreach (PatientElectronicAddressCustom item in patientRecord.TelephoneNumbers)
                {
                    PatientElectronicAddress patientElectronicAddress = new PatientElectronicAddress();
                    patientElectronicAddress.ElecType = item.ElectronicAddressType;
                    patientElectronicAddress.Telephone_numbers = item.Telephone_number;
                    if (id.HasValue)
                        patientElectronicAddress.PatientId = id.Value;
                    patientElectronicAddresses.Add(patientElectronicAddress);
                }

                patient.PatientElectronicAddresses = new List<PatientElectronicAddress>();
                patient.PatientElectronicAddresses = patientElectronicAddresses;
            }
            return patient;
        }

        /// <summary>
        /// To extract PatientElectronicAddress info
        /// from Patient record
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public static List<PatientElectronicAddress> ConvertPatientContactCustomToPatientContactDB(PatientRecord patient)
        {
            List<PatientElectronicAddress> patientElectronicAddresses = new List<PatientElectronicAddress>();
            foreach (PatientElectronicAddressCustom item in patient.TelephoneNumbers)
            {
                PatientElectronicAddress patientElectronicAddress = new PatientElectronicAddress();
                patientElectronicAddress.ElecType = item.ElectronicAddressType;
                patientElectronicAddress.Telephone_numbers = item.Telephone_number;
                patientElectronicAddress.PatientId = patient.Id;
                patientElectronicAddresses.Add(patientElectronicAddress);
            }
            return patientElectronicAddresses;
        }

    }
}
