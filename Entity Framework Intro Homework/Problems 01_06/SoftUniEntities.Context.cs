﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Problems_01_06
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SoftUniEntities : DbContext
    {
        public SoftUniEntities()
            : base("name=SoftUniEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Town> Towns { get; set; }
    
        public virtual ObjectResult<GetProjectsByEmployee_Result> GetProjectsByEmployee(string firstname, string lastEmail)
        {
            var firstnameParameter = firstname != null ?
                new ObjectParameter("firstname", firstname) :
                new ObjectParameter("firstname", typeof(string));
    
            var lastEmailParameter = lastEmail != null ?
                new ObjectParameter("lastEmail", lastEmail) :
                new ObjectParameter("lastEmail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetProjectsByEmployee_Result>("GetProjectsByEmployee", firstnameParameter, lastEmailParameter);
        }
    
        public virtual ObjectResult<GetProjects_Result> GetProjects(string firstname, string lastEmail)
        {
            var firstnameParameter = firstname != null ?
                new ObjectParameter("firstname", firstname) :
                new ObjectParameter("firstname", typeof(string));
    
            var lastEmailParameter = lastEmail != null ?
                new ObjectParameter("lastEmail", lastEmail) :
                new ObjectParameter("lastEmail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetProjects_Result>("GetProjects", firstnameParameter, lastEmailParameter);
        }
    
        public virtual ObjectResult<GetProjectsByEmployees_Result> GetProjectsByEmployees(string firstname, string lastEmail)
        {
            var firstnameParameter = firstname != null ?
                new ObjectParameter("firstname", firstname) :
                new ObjectParameter("firstname", typeof(string));
    
            var lastEmailParameter = lastEmail != null ?
                new ObjectParameter("lastEmail", lastEmail) :
                new ObjectParameter("lastEmail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetProjectsByEmployees_Result>("GetProjectsByEmployees", firstnameParameter, lastEmailParameter);
        }
    
        public virtual ObjectResult<usp_GetProjectsByEmployees_Result> usp_GetProjectsByEmployees(string firstname, string lastEmail)
        {
            var firstnameParameter = firstname != null ?
                new ObjectParameter("firstname", firstname) :
                new ObjectParameter("firstname", typeof(string));
    
            var lastEmailParameter = lastEmail != null ?
                new ObjectParameter("lastEmail", lastEmail) :
                new ObjectParameter("lastEmail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_GetProjectsByEmployees_Result>("usp_GetProjectsByEmployees", firstnameParameter, lastEmailParameter);
        }
    }
}
