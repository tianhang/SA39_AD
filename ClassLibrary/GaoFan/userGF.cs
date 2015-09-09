using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Entities;

namespace ClassLibrary.GaoFan
{
    
    public class userGF
    {
        SSISDBEntities ctx = new SSISDBEntities();
        public User getUserByEmailGF(string email)
        {
            //
            user x = ctx.users.FirstOrDefault(o => o.email == email);
            User u = new User();
            u.UserID = x.userId;
            u.UserName = x.name;
            u.PhoneNo = x.phoneNo;
            u.Email = x.email;
            u.Address = x.address;
            u.DepartmentId = x.departmentId;
            u.RoleId = x.roleId;
            u.Password = x.password;
            if (x.roleId == "d104")
            {
                u.StartDate = Convert.ToDateTime(x.startDate);
                u.EndDate = Convert.ToDateTime(x.endDate);
            }
            else
            {
                u.StartDate = DateTime.Now;
                u.EndDate = DateTime.Now;
            }
            u.DepartmentName = x.department.name;
            u.RoleName = x.role.name;
            u.Status = x.status;
            return u;
            //gaofan
        }

        public User getCurrentRepresentativeGF(string partId)
        {
            user x = ctx.users.FirstOrDefault(o => o.roleId == "d103" && o.departmentId == partId);
            User u = new User();
            u.UserID = x.userId;
            u.UserName = x.name;
            u.PhoneNo = x.phoneNo;
            u.Email = x.email;
            u.Address = x.address;
            u.DepartmentId = x.departmentId;
            u.RoleId = x.roleId;
            u.Password = x.password;
            if (x.roleId == "d104")
            {
                u.StartDate = Convert.ToDateTime(x.startDate);
                u.EndDate = Convert.ToDateTime(x.endDate);
            }
            else
            {
                u.StartDate = DateTime.Now;
                u.EndDate = DateTime.Now;
            }
            u.DepartmentName = x.department.name;
            u.RoleName = x.role.name;
            return u;
        }

        public List<User> getEmployeeByDepartmentIdGF(string partId)
        {
            var uu = ctx.users.Where(o => o.roleId == "d101" && o.departmentId == partId);
            List<User> ulist = new List<User>();
            foreach(user x in uu.ToList())
            {
                User u = new User();
                u.UserID = x.userId;
                u.UserName = x.name;
                u.PhoneNo = x.phoneNo;
                u.Email = x.email;
                u.Address = x.address;
                u.DepartmentId = x.departmentId;
                u.RoleId = x.roleId;
                u.Password = x.password;
                if (x.roleId == "d104")
                {
                    u.StartDate = Convert.ToDateTime(x.startDate);
                    u.EndDate = Convert.ToDateTime(x.endDate);
                }
                else
                {
                    u.StartDate = DateTime.Now;
                    u.EndDate = DateTime.Now;
                }
                u.DepartmentName = x.department.name;
                u.RoleName = x.role.name;
                ulist.Add(u);
            }
           
            return ulist;
        }
        public List<User> getAllUserByDepartmentIdGF(string departmentId)
        {
            var item = from a in ctx.users
                       where a.departmentId == departmentId
                       select new
                       {
                           UserID = a.userId,
                           UserName = a.name,
                           PhoneNo = a.phoneNo,
                           Email = a.email,
                           Address = a.address,
                           DepartmentId = a.departmentId,
                           RoleId = a.roleId,
                           Password = a.password
                       };

            List<User> list = new List<User>();
            foreach (var x in item)
            {
                User u = new User();
                u.UserID = x.UserID;
                u.UserName = x.UserName;
                u.PhoneNo = x.PhoneNo;
                u.Email = x.Email;
                u.Address = x.Address;
                u.DepartmentId = x.DepartmentId;
                u.RoleId = x.RoleId;
                u.Password = x.Password;
                list.Add(u);
            }
            return list;
            //gaofan
        }

        public void updateUserRoleIdByNameGF(User u)
        {
            user x = ctx.users.FirstOrDefault(o => o.name == u.UserName);
            x.roleId = "d103"; 
            ctx.SaveChanges();
            //gaofan
        }

        public void updateRoleBack(User e)
        {
            user x = ctx.users.FirstOrDefault(o => o.name == e.UserName);
            x.roleId = "d101";
            ctx.SaveChanges();
            //gaofan
        }

        public User getCurrentDelegateGF(string departmentId)
        {
            user x = ctx.users.FirstOrDefault(o => o.roleId == "d104" && o.departmentId == departmentId);
            if (x != null)
            {
                User u = new User();
                u.UserID = x.userId;
                u.UserName = x.name;
                u.PhoneNo = x.phoneNo;
                u.Email = x.email;
                u.Address = x.address;
                u.DepartmentId = x.departmentId;
                u.RoleId = x.roleId;
                u.Password = x.password;
                if (x.roleId == "d104")
                {
                    u.StartDate = Convert.ToDateTime(x.startDate);
                    u.EndDate = Convert.ToDateTime(x.endDate);
                }
                else
                {
                    u.StartDate = DateTime.Now;
                    u.EndDate = DateTime.Now;
                }
                u.DepartmentName = x.department.name;
                u.RoleName = x.role.name;
                return u;
            }
            else {
                return null;
            }
        }

        public void updateUserGF(User u) {
            user us = ctx.users.FirstOrDefault(o => o.userId == u.UserID);
            us.roleId = u.RoleId;
            us.startDate = u.StartDate;
            us.endDate = u.EndDate;
            ctx.SaveChanges();
        }

        public User getManagerGF() {
            user x = ctx.users.FirstOrDefault(o => o.roleId == "d104");
            User u = new User();
            if (x != null)
            {

                u.UserID = x.userId;
                u.UserName = x.name;
                u.PhoneNo = x.phoneNo;
                u.Email = x.email;
                u.Address = x.address;
                u.DepartmentId = x.departmentId;
                u.RoleId = x.roleId;
                u.Password = x.password;
                if (x.roleId == "d104")
                {
                    u.StartDate = Convert.ToDateTime(x.startDate);
                    u.EndDate = Convert.ToDateTime(x.endDate);
                }
                else
                {
                    u.StartDate = DateTime.Now;
                    u.EndDate = DateTime.Now;
                }
                u.DepartmentName = x.department.name;
                u.RoleName = x.role.name;
                return u;
            }
            else
            {
                user m = ctx.users.FirstOrDefault(o => o.roleId == "d103");
                u.UserID = m.userId;
                u.UserName = m.name;
                u.PhoneNo = m.phoneNo;
                u.Email = m.email;
                u.Address = m.address;
                u.DepartmentId = m.departmentId;
                u.RoleId = m.roleId;
                u.Password = m.password;
                if (x.roleId == "d104")
                {
                    u.StartDate = Convert.ToDateTime(x.startDate);
                    u.EndDate = Convert.ToDateTime(x.endDate);
                }
                else
                {
                    u.StartDate = DateTime.Now;
                    u.EndDate = DateTime.Now;
                }
                u.DepartmentName = m.department.name;
                u.RoleName = m.role.name;
                return u;
            }
        }
    }
}
