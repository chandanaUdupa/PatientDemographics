using PatientDemo.BusinessObject.Adapter;
using PatientDemo.BusinessObject.BusinessEntities;
using PatientDemo.BusinessObject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDemo.BusinessObject
{
    public class PatientManager
    {
        /// <summary>
        /// To call PatientDAL's Get methods
        /// and process the return value to PatientRecord
        /// </summary>
        /// <param name="id"></param>
        /// <returns>PatientRecord</returns>
        public PatientRecord GetPatientDetails(int id)
        {
            PatientElectronicAddressCollection patientElecAddressList = new PatientElectronicAddressCollection();
            PatientRecord patientFound = PatientDAL.GetPatientCoreDetails(id);

            foreach (var item in PatientDAL.GetPatientContactDetails(id))
            {
                PatientElectronicAddressCustom patientElectronicAddress = new PatientElectronicAddressCustom();
                patientElectronicAddress.ElectronicAddressType = item.ElecType;
                patientElectronicAddress.Telephone_number = item.Telephone_numbers;
                patientElecAddressList.Add(patientElectronicAddress);
            }

            patientFound.TelephoneNumbers = new PatientElectronicAddressCollection();
            patientFound.TelephoneNumbers = patientElecAddressList;

            return patientFound;
        }

        /// <summary>
        /// To call PatientDAL's Set methods
        /// and return success value
        /// </summary>
        /// <param name="patient"></param>
        /// <returns>int</returns>
        public int SetPatientDetails(PatientRecord patient)
        {
            return PatientDAL.SetPatientDetails(PatientBusinessEntityToDBModel.ConvertPatientRecordToPatient(patient));
        }

        /// <summary>
        /// To call PatientDAL's Save methods
        /// to save existing patient's modified details
        /// or to add Telephone number to a Patient record
        /// and return success value
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="id"></param>        
        /// <returns>int</returns>
        public int SavePatientCDetails(int id, PatientRecord patient)
        {
            return PatientDAL.SavePatientCDetails(id, patient);
        }
    }
}
