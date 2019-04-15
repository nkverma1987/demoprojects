using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDemoNew.Models
{
    public class Employee : TableEntity
    {
        private string EmpNo => PartitionKey;
        private string Name => RowKey;
        private string city;

        public string City
        {
            get
            {
                return city;
            }

            set
            {
                city = value;
            }
        }
        string conString = System.Configuration.ConfigurationManager.ConnectionStrings["AzureStorageAccount"].ConnectionString;
        CloudStorageAccount sa;
        public bool Save(Employee employee, out string msg)
        {
            msg = string.Empty;
            bool hasError = false;
            try
            {
                sa = CloudStorageAccount.Parse(conString);
                //CREATE AZURE TABLE
                var tableClient = sa.CreateCloudTableClient();
                var employeeTable = tableClient.GetTableReference("Employee");
                employeeTable.CreateIfNotExists();
                //insert entities
                employeeTable.Execute(TableOperation.Insert(employee));
            }
            catch (Exception ex)
            {
                hasError = true;
                msg = ex.Message;
            }
            return hasError;
        }
        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            sa = CloudStorageAccount.Parse(conString);
            var tableClient = sa.CreateCloudTableClient();
            var employeeTable = tableClient.GetTableReference("Employee");
            employeeTable.CreateIfNotExists();
            //query table
            var scores = employeeTable.CreateQuery<Employee>();
            return scores.ToList();
        }

        public bool Delete(Employee employee, out string msg)
        {
            msg = string.Empty;
            bool hasError = false;
            try
            {
                sa = CloudStorageAccount.Parse(conString);
                var tableClient = sa.CreateCloudTableClient();
                var employeeTable = tableClient.GetTableReference("Employee");
                TableOperation delteOperation = TableOperation.Delete(employee);
                employeeTable.Execute(delteOperation);
            }
            catch (Exception ex)
            {
                hasError = true;
                msg = ex.Message;
            }
            return hasError;
        }

        public bool Update(Employee employee, out string msg)
        {
            msg = string.Empty;
            bool hasError = false;
            try
            {
                sa = CloudStorageAccount.Parse(conString);
                var tableClient = sa.CreateCloudTableClient();
                var employeeTable = tableClient.GetTableReference("Employee");
                TableOperation updateOperation = TableOperation.InsertOrReplace(employee);
                employeeTable.Execute(updateOperation);
            }
            catch (Exception ex)
            {
                hasError = true;
                msg = ex.Message;
            }
            return hasError;
        }
    }
}
