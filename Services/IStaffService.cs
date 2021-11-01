namespace CoreDemoJSON.Services
{
    using System.Collections.Generic;
    using CoreDemoJSON.Models;
    

    public interface IStaffService
    {
        List<StaffMember> GetStaff();
    
        StaffMember FindStaffById(int id);

        void SaveNewStaff(StaffMember staffModel);

        void UpdateStaff(StaffMember staffModel); 

        bool DeleteStaff(int id);     
    }
}