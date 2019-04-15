using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;

namespace MVCDemoNew.App_Start
{
    public class ResourceManager
    {
        private static ListDictionary GetResourceCache(string cultureName)
        {
            ListDictionary resources = CacheHelper.GetItem(cultureName) as ListDictionary;
            if (null == resources)
            {
                resources = ResourceHelper.GetResources(cultureName);
                CacheHelper.InsertItem(cultureName, resources);
            }
            return resources;
        }

        public static string GetString(string resourceKey)
        {
            string value = GetResourceCache(Thread.CurrentThread.CurrentCulture.Name)[resourceKey] as string;
            if (string.IsNullOrWhiteSpace(value))
            {
                value = GetResourceCache("")[resourceKey] as string;
                if (string.IsNullOrWhiteSpace(value))
                {
                    value = resourceKey;
                }
            }
            return value;
        }
    }

    public class ResourceHelper
    {
        public static ListDictionary GetResources(string cultureName)
        {
            ListDictionary resources = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    SqlCommand command = new SqlCommand("GetResources", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter paramCultureCode = new SqlParameter() { ParameterName = "p_CultureCode", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = cultureName };

                    command.Parameters.Add(paramCultureCode);
                    command.Parameters.Add(DatabaseHelper.ErrorCodeParameter);
                    command.Parameters.Add(DatabaseHelper.ErrorMessageParameter);

                    DataSet dsResources = new DataSet();

                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.SelectCommand = command;
                        adapter.Fill(dsResources);
                    }

                        if (null != dsResources && dsResources.Tables.Count > 0 && dsResources.Tables[0].Rows.Count > 0)
                        {
                            resources = new ListDictionary();
                            foreach (DataRow drResource in dsResources.Tables[0].Rows)
                            {
                                resources.Add(drResource[""].ToString(), drResource[""].ToString());
                            }
                        }
                    
                }
            }
            catch (Exception objException)
            {
            }
            return resources;
        }
    }
}