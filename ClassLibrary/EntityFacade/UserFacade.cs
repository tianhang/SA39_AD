using ClassLibrary.Entities;
using ClassLibrary.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.EntityFacade
{
    public class UserFacade
    {
        SSISDBEntities ctx;
        ErrorLog errorobj;
        public UserFacade()
        {
            ctx = new SSISDBEntities();
            errorobj = new ErrorLog();
        }

        public List<User> getUsers(string departmentId)
        {
            List<User> userCollection = new List<User>();

            try
            {
                var users = from u in ctx.users
                            where
                              u.departmentId == departmentId
                              where
                               !(new string[] {"d102", "d104" }).Contains(u.roleId)
                                 && u.status == "Active"
                            select new
                            {
                                uid = u.userId,
                                name = u.name,
                                email = u.email,
                                start = u.startDate,
                                end = u.endDate,
                                roleId = u.roleId,
                                roleName = u.role.name
                            };

                foreach (var u in users)
                {
                    User user = new User();
                    user.UserID = u.uid;
                    user.UserName = u.name;
                    user.Email = u.email;
                    if (u.roleName == "departmentDeputy")
                    {
                        user.StartDate = (DateTime)u.start.Value;
                        user.EndDate = (DateTime)u.end.Value;
                    }
                    user.RoleId = u.roleId;
                    user.RoleName = u.roleName;
                    userCollection.Add(user);
                }
            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("UserFacade-getUsersWithRole():::" + exception.ToString());
                userCollection = new List<User>();
            }
            return userCollection;
        }

        public void updateUserRole(string userId, string roleId, bool resetDeputy)
        {
            try
            {
                user us = (from u in ctx.users
                          where u.userId == userId
                          select u).FirstOrDefault();

                us.roleId = roleId;
                if(resetDeputy == true)
                {
                    us.startDate = null;
                    us.endDate = null;
                }

                ctx.SaveChanges();
                
            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("UserFacade-updateUserRole():::" + exception.ToString());
            }
        }

        public void getUserRoles()
        {

        }

        public List<User> getUsersWithRole(string roleName, string departmentId)
        {
            List<User> userCollection = new List<User>();

            try
            {
                var users = from u in ctx.users
                            where
                              u.roleId ==
                               ((from r in ctx.roles
                                 where
                                   r.name == roleName
                                   && u.department.departmentId == departmentId
                                 select new
                                 {
                                     r.roleId
                                 }).FirstOrDefault().roleId)

                                 && u.status == "Active"
                            select new
                            {
                                uid = u.userId,
                                name = u.name,
                                email = u.email,
                                start = u.startDate,
                                end = u.endDate,
                                roleId = u.roleId,
                                roleName = u.role.name
                            };



                foreach (var u in users)
                {
                    User user = new User();
                    user.UserID = u.uid;
                    user.UserName = u.name;
                    user.Email = u.email;
                    if (u.roleName == "departmentDeputy")
                    {
                        user.StartDate = (DateTime)u.start.Value;
                        user.EndDate = (DateTime)u.end.Value;
                    }
                    user.RoleId = u.roleId;
                    user.RoleName = u.roleName;
                    userCollection.Add(user);
                }
            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("UserFacade-getUsersWithRole():::" + exception.ToString());
                userCollection = new List<User>();
            }
            return userCollection;
        }

        public List<User> getUsersWithRole(string roleName)
        {
            List<User> userCollection = new List<User>();

            try
            {
                var users = from u in ctx.users
                         where
                           u.roleId ==
                            ((from r in ctx.roles
                              where
                                r.name == roleName
                              select new
                              {
                                  r.roleId
                              }).FirstOrDefault().roleId)

                              && u.status == "Active"
                         select new
                         {
                             uid = u.userId,
                             name = u.name,
                             email = u.email,
                             start = u.startDate,
                             end = u.endDate,
                         };

                foreach(var u in users)
                {
                    User user = new User();
                    user.UserID = u.uid;
                    user.UserName = u.name;
                    user.Email = u.email;
                    if(roleName=="departmentDeputy")
                    {
                        user.StartDate = (DateTime)u.start.Value;
                        user.EndDate = (DateTime)u.end.Value;
                    }
                    userCollection.Add(user);
                }
            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("UserFacade-getUsersWithRole():::" + exception.ToString());
                userCollection = new List<User>();
            }
            return userCollection;
        }

        public string getHODorDeputy(string departmentId)
        {
            return departmentId;
        }

        public role getUserRoles(string roleName)
        {
            role userRole;
            try
            {
                userRole =  (from r in ctx.roles
                           where r.name == roleName
                           select r).FirstOrDefault();
            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("UserFacade-getUserRoles():::" + exception.ToString());
                userRole = null;
            }

            return userRole;
        }

        public string updateDeputy(string userId, DateTime startDate, DateTime endDate)
        {
            return userId;
        }

        public void createDeputy(string userId, DateTime startDate, DateTime endDate, string roleId)
        {
            try
            {
            
            user usr = (from u in ctx.users
                      where u.userId == userId
                      select u).FirstOrDefault();

            usr.roleId = roleId;
            usr.startDate = startDate;
            usr.endDate = endDate;

            ctx.SaveChanges();
            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("UserFacade-createDeputy():::" + exception.ToString());
            }

        }


        #region Lingna's Part

        public user getUser_Lingna(string userId)
        {
            var u = (from o in ctx.users where o.userId == userId select o).First();
            return u;
        }

        #endregion


        public List<user> getUser_PyaePyae(string departmentId)
        {
            var userData = from x in ctx.users
                           where x.departmentId == departmentId
                           select x;
            return userData.ToList();
        }


    }
}
