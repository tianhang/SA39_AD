using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Web;
using ClassLibrary.Entities;


namespace ClassLibrary.EntityFacade
{
    public class LoginFacade
    {
        SSISDBEntities entity;
        User userEntity;
        public LoginFacade()
        {
            entity = new SSISDBEntities();
        }

        public bool checkLogIn(string email, string password)
        {
            var userData = (from x in entity.users where x.email == email & x.password == password select x).Count();
            int userCount = userData;
            if (userCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public user getUserData(string email, string password)
        {
            var userData = from x in entity.users where x.email == email & x.password == password select x;
            return userData.SingleOrDefault();
        }

        public department getDepartmentData(string departmentid)
        {
            var departmentData = from x in entity.departments where x.departmentId == departmentid select x;
            return departmentData.SingleOrDefault();
        }

        public List<department> getDepartmentDataList()
        {
            var departmentData = from x in entity.departments select x;
            return departmentData.ToList();
        }

        public role getRoleData(string roleId)
        {
            var roleData = from x in entity.roles where x.roleId == roleId select x;
            return roleData.SingleOrDefault();
        }

        public User fillUserEntity(string email, string password)
        {
            userEntity = new User();
            if (checkLogIn(email, password))
            {
                //Call to every methods to collect data needed
                user userData = getUserData(email, password);

                //Fill to customize entity from data                
                userEntity.UserID = userData.userId;
                userEntity.UserName = userData.name;
                //userEntity.PhoneNo = userData.phoneNo;
                userEntity.Email = userData.email;
                userEntity.Address = userData.address;
                userEntity.DepartmentId = userData.departmentId;
                userEntity.RoleId = userData.roleId;
                userEntity.Password = userData.password;
                role roleData = getRoleData(userData.roleId);
                userEntity.RoleName = roleData.name;
                //get department data
                department departmentData = getDepartmentData(userData.departmentId);
                userEntity.DepartmentName = departmentData.name;
            }            
            return userEntity;
        }
    }
}
