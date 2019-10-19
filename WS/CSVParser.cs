using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Globalization;
using System.Security.Cryptography;

namespace WS
{
    class CSVParser
    {
        public static void Parser(string path)
        {
            var allLines = File.ReadLines(path);            
            foreach (var item in allLines)
            {
                var arrValue = item.Split(',');
                if (arrValue.Length == 8)
                {
                    User user = new User();

                    user.RoleID = arrValue[0] == "Administrator" ? 1 : 2;
                    user.Email = arrValue[1];
                    user.Password = GetMd5Hash(arrValue[2]);
                    //user.Password = arrValue[2];
                    user.FirstName = arrValue[3];
                    user.LastName = arrValue[4];

                    session1DataSetTableAdapters.OfficesTableAdapter adapter = new session1DataSetTableAdapters.OfficesTableAdapter();
                    var officeData = adapter.GetData();
                    int id = (from i in officeData
                             where i.Title == arrValue[5]
                             select i.ID).First();

                    user.OfficeID = id;
                    user.Birthdate = DateTime.ParseExact(arrValue[6], "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    user.Active = arrValue[7] == "1" ? true : false;

                    session1DataSetTableAdapters.UsersTableAdapter adapterUser = new session1DataSetTableAdapters.UsersTableAdapter();
                    
                    var userData = adapterUser.GetData();
                    int UserID = 0;
                    try
                    {
                        UserID = (from i in userData
                                  orderby i.ID descending
                                  select i.ID).First() + 1;
                    }
                    catch{ }                    

                    adapterUser.Insert(UserID, user.RoleID, user.Email,user.Password, user.FirstName, user.LastName, user.OfficeID, user.Birthdate, user.Active);                                        
                }
                else
                {
                    throw new Exception("CSVParser::Parser::invalidqtyparam");
                }                                
            }
        }
        public static string GetMd5Hash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hash);
        }
    }
}
