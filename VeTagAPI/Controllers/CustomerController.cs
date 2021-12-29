using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VeTagAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VeTagAPI.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        SqlConnection con = new SqlConnection("Server=DESKTOP-2BEA8I1\\SQLEXPRESS01;Database=vetag;Trusted_Connection=true");     
            // Data Source = DESKTOP - 2BEA8I1\\SQLEXPRESS01;Integrated Security = True; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
               // GET: api/<CustomerController>
               [HttpGet]
        public async Task<IEnumerable<Customer>> Get()
        {
            IEnumerable<Customer> customers = new List<Customer>();
            SqlDataAdapter da = new SqlDataAdapter("select * from Customer order by Created_Date desc", con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            customers = ds.Tables[0].AsEnumerable()
               .Select(dataRow => new Customer
               {
                   CustomerId = dataRow.Field<string>("CustomerId"),

                   AllowCalls = dataRow.Field<bool>("AllowCalls"),
                   FullName = dataRow.Field<string>("FullName"),
                   EmailId = dataRow.Field<string>("EmailId"),
                   EmergencyContactNumber = dataRow.Field<string>("EmergencyContactNumber"),
                   MobileNumber = dataRow.Field<string>("MobileNumber"),
                   ReferenceID = dataRow.Field<string>("ReferenceID"),
                   VehicleNumber = dataRow.Field<string>("VehicleNumber"),
                   IsRegisterByCustomer = dataRow.Field<bool>("IsRegisterByCustomer"),
                   ContactOptions = dataRow.Field<string>("ContactOptions"),

               }).ToList();
            //return new string[] { "value1", "value2" };

            return customers;
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public Customer Get(string id)
        {
            Customer customer = null;
            if (!string.IsNullOrEmpty(id))
            {
                string cmdText = "select * from Customer Where CustomerId = '" + id + "'";
                SqlDataAdapter da = new SqlDataAdapter(cmdText, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                customer = ds.Tables[0].AsEnumerable()
               .Select(dataRow => new Customer
               {
                   CustomerId = dataRow.Field<string>("CustomerId"),
                   AllowCalls = dataRow.Field<bool>("AllowCalls"),
                   FullName = dataRow.Field<string>("FullName"),
                   EmailId = dataRow.Field<string>("EmailId"),
                   EmergencyContactNumber = dataRow.Field<string>("EmergencyContactNumber"),
                   MobileNumber = dataRow.Field<string>("MobileNumber"),
                   ReferenceID = dataRow.Field<string>("ReferenceID"),
                   VehicleNumber = dataRow.Field<string>("VehicleNumber"),
                   IsRegisterByCustomer = dataRow.Field<bool>("IsRegisterByCustomer"),
                   ContactOptions = dataRow.Field<string>("ContactOptions"),
               }).FirstOrDefault();
            }

            return customer;

        }

        // POST api/<CustomerController>
        [HttpPost]
        public async Task<Customer> Post([FromBody] Customer customer)
        {
            Customer new_customer = new Customer();
            try
            {
                if (customer != null)
                {
                    string cmdtxt = @"Insert into [dbo].[customer](CustomerId, AllowCalls,FullName,
                                EmailId, EmergencyContactNumber, MobileNumber,ReferenceID, VehicleNumber, IsRegisterByCustomer, ContactOptions) 
                                Values (@CustomerId, @AllowCalls,@FullName,
                                @EmailId, @EmergencyContactNumber, @MobileNumber,@ReferenceID, @VehicleNumber, @IsRegisterByCustomer, @ContactOptions)";
                    Guid custid =  Guid.NewGuid();
                    SqlCommand cmd = new SqlCommand(cmdtxt, con);
                    cmd.Parameters.AddWithValue("@CustomerId", custid);                    
                    cmd.Parameters.AddWithValue("@AllowCalls", (customer.AllowCalls ? customer.AllowCalls :false));
                    cmd.Parameters.AddWithValue("@FullName", (string.IsNullOrEmpty(customer.FullName) ? "" : customer.FullName));
                    cmd.Parameters.AddWithValue("@EmailId", (string.IsNullOrEmpty(customer.EmailId) ? "" : customer.EmailId));
                    cmd.Parameters.AddWithValue("@MobileNumber", (string.IsNullOrEmpty(customer.MobileNumber) ? "" : customer.MobileNumber));
                    cmd.Parameters.AddWithValue("@EmergencyContactNumber", (string.IsNullOrEmpty(customer.EmergencyContactNumber) ? "" : customer.EmergencyContactNumber));
                    cmd.Parameters.AddWithValue("@ReferenceID", (string.IsNullOrEmpty(customer.ReferenceID) ? "" : customer.ReferenceID));
                    cmd.Parameters.AddWithValue("@VehicleNumber", (string.IsNullOrEmpty(customer.VehicleNumber) ? "" : customer.VehicleNumber));
                    cmd.Parameters.AddWithValue("@IsRegisterByCustomer", (customer.IsRegisterByCustomer ? customer.IsRegisterByCustomer : false));
                    cmd.Parameters.AddWithValue("@ContactOptions", (string.IsNullOrEmpty(customer.ContactOptions) ? "" : customer.ContactOptions));
                    con.Open();
                  int count =  cmd.ExecuteNonQuery();
                    if(count > 0)
                    {
                      IEnumerable<Customer> custs = await this.Get();
                        new_customer = custs.FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
               
            }     
            finally
            {
                con.Close();
            }

            return new_customer;
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] Customer customer)
        {
            try
            {
                if (customer != null && !string.IsNullOrEmpty(id))
                {
                    Customer cust = this.Get(id);
                    string _allowcalls = "", _fullName = "", _emailId = "", _mobileNumber = "", _emergencyContactNumber = "", _referenceID = "", _vehicleNumber = "", _isRegisterByCustomer = "", _contactOptions = "";

                    if (cust != null)
                    {
                       
                        SqlCommand cmd = new SqlCommand();
                      
                        if (customer.AllowCalls)
                        {
                            cmd.Parameters.AddWithValue("@AllowCalls", (customer.AllowCalls));
                            _allowcalls = "AllowCalls = @AllowCalls,";
                        }

                        if (!string.IsNullOrEmpty(customer.FullName))
                        {
                            cmd.Parameters.AddWithValue("@FullName", customer.FullName);
                            _fullName = "FullName = @FullName,";
                        }

                        if (!string.IsNullOrEmpty(customer.EmailId))
                        {
                            cmd.Parameters.AddWithValue("@EmailId", customer.EmailId);
                            _emailId = "EmailId = @EmailId,";
                        }

                        if (!string.IsNullOrEmpty(customer.MobileNumber))
                        {
                            cmd.Parameters.AddWithValue("@MobileNumber", customer.MobileNumber);
                            _mobileNumber = "MobileNumber = @MobileNumber,";
                        }

                        if (!string.IsNullOrEmpty(customer.EmergencyContactNumber))
                        {
                            cmd.Parameters.AddWithValue("@EmergencyContactNumber", customer.EmergencyContactNumber);
                            _emergencyContactNumber = "EmergencyContactNumber = @EmergencyContactNumber,";
                        }

                        if (!string.IsNullOrEmpty(customer.ReferenceID))
                        {
                            cmd.Parameters.AddWithValue("@ReferenceID", customer.ReferenceID);
                            _referenceID = "ReferenceID = @ReferenceID,";
                        }

                        if (!string.IsNullOrEmpty(customer.VehicleNumber))
                        {
                            cmd.Parameters.AddWithValue("@VehicleNumber", customer.VehicleNumber);
                            _vehicleNumber  = "VehicleNumber = @VehicleNumber,";
                        }

                        if (customer.IsRegisterByCustomer)
                        {
                            cmd.Parameters.AddWithValue("@IsRegisterByCustomer", customer.IsRegisterByCustomer);
                            _isRegisterByCustomer = "IsRegisterByCustomer = @IsRegisterByCustomer,";
                        }


                        if (!string.IsNullOrEmpty(customer.ContactOptions))
                        {
                            cmd.Parameters.AddWithValue("@ContactOptions", customer.ContactOptions);
                            _contactOptions = "ContactOptions = @ContactOptions,";
                        }

                        string updateCmd = $"Update [dbo].[customer] set { _allowcalls } {_fullName} {_emailId} {_mobileNumber} {_emergencyContactNumber} {_referenceID} {_vehicleNumber} {_isRegisterByCustomer} {_contactOptions} where CustomerId = '{ cust.CustomerId}'";
                        cmd.Connection = con;
                        cmd.CommandText = updateCmd.Replace(",  where", " where");

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
