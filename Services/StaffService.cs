namespace CoreDemoJSON.Services
{

    using System.Collections.Generic;
    using CoreDemoJSON.Models;
    using System.Linq;
    using Newtonsoft.Json;

    public class StaffService: IStaffService
    {

        const string StaffFileName = "staff.json";

        private List<StaffMember> _staff = new List<StaffMember>();


        public StaffService()
        {

            // Read From Staff file and into string
            string strResultJSON = string.Empty;

            if (System.IO.File.Exists(StaffFileName))
            {
                strResultJSON = System.IO.File.ReadAllText(StaffFileName);
            }

            // Map Staff file string to a list of StaffMember objects
            List<StaffMember> result = JsonConvert.DeserializeObject<List<StaffMember>>(strResultJSON);

            if (result != null)
                {
                    _staff = JsonConvert.DeserializeObject<List<StaffMember>>(strResultJSON);
                }
        }


        public StaffMember FindStaffById(int id)
        {

            StaffMember staff  = _staff.Where(p => p.Id == id).FirstOrDefault();
            
            return staff;            
        }


        public List<StaffMember> GetStaff()
        {
            return _staff;
        }


        public bool DeleteStaff(int id)
        {

            // delete ID > 1 and returns no records deleted
            // int no = _staff.RemoveAll(x => x.Id == id);

            // if (no == 1)
            //     {
            //         UpdateFile();
            //         return true;
            //     }
            // else
            //     {
            //         return false;        
            //     }

            StaffMember staffToDelete = _staff.FirstOrDefault(e => e.Id ==id);

            if (staffToDelete != null)
            {
                _staff.Remove(staffToDelete);
                UpdateFile();
                return true;
            }

            return false;
        }


        public void SaveNewStaff(StaffMember staffModel)
        {

            // Increment Id
            int id =0;
            if (_staff ==null || _staff.Count==0)
            {
                _staff = new List<StaffMember>();
            }
            else
            {
                int countt = _staff.Count-1;
                id = _staff[countt].Id;
            }

            id++;

            
            // Create Staff object
            StaffMember staff = new StaffMember();
            
            staff.Id = id;
            staff.FirstName = staffModel.FirstName;
            staff.LastName = staffModel.LastName;

            // Add Staff object to _staff
            _staff.Add(staff); 

            // Build json data string of all staff and write to file
            string jsonData = JsonConvert.SerializeObject(_staff);
            System.IO.File.WriteAllText(StaffFileName, jsonData);
   
        } 


        public void UpdateStaff(StaffMember staffModel)
        {
            // update FirstName and LastName for ID
            foreach (StaffMember staff in _staff.Where(u => u.Id==staffModel.Id))
            {
                staff.FirstName= staffModel.FirstName;
                staff.LastName = staffModel.LastName;
            }

            UpdateFile(); 
        }


        private void UpdateFile()
        {
            // Build json data string of all staff and write to file
            string jsonData = JsonConvert.SerializeObject(_staff);
            System.IO.File.WriteAllText(StaffFileName, jsonData);            
        }

    }
}