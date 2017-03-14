using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Wpf_Net_4.Hola
{
    class User
    {
        public long UserKey { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public long UserRoleKey { get; set; }

        public const string USER_KEY = "UserKey";
        public const string USER_NAME = "UserKey";
        public const string PASSWORD = "UserKey";
        public const string USER_ROLE_KEY = "UserKey";

        public User(DataRow record)
        {
            UserKey = record.Field<long>(USER_KEY);
            UserName = record.Field<string>(USER_NAME);
            Password = record.Field<string>(PASSWORD);
            UserRoleKey = record.Field<long>(USER_ROLE_KEY);
        }

        public static IEnumerable<User> Users(DataTable records)
        {
            foreach (DataRow record in records.Rows)
            {
                yield return new User(record);
            }
        }
    }
}
