using LibApp.Models;
using System.Collections.Generic;

namespace LibApp.Interfaces
{
    public interface IMembershipTypeRepository
    {
        IEnumerable<MembershipType> GetMembershipTypes();
        MembershipType GetMembershipTypeById(int membershipTypeId);
        void AddMembershipType(MembershipType membershipType);
        void UpdateMembershipType(MembershipType membershipType);
        void DeleteMembershipType(int membershipTypeId);

        void Save();
    }
}
