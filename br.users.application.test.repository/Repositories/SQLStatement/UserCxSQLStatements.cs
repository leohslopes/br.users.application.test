using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace br.users.application.test.repository.Repositories.SQLStatement
{
    public class UserCxSQLStatements
    {
        public static readonly string GetAllUsers = @"SELECT " +
                                                    "id_user AS UserID, " +
                                                    "name_user AS UserName, " +
                                                    "email_user AS UserEmail, " +
                                                    "age_user AS UserAge, " +
                                                    "gender_user AS UserGender, " +
                                                    "password_user AS UserPassword " +
                                                    "FROM users_cx";

        public static readonly string InsertUserData = @"INSERT INTO users_cx(name_user, " +
                                                       "email_user, " +
                                                       "age_user, " +
                                                       "gender_user," +
                                                       "password_user) " +
                                                       "VALUES (@P_NAME_USER, " +
                                                       "@P_EMAIL_USER, " +
                                                       "@P_AGE_USER, " +
                                                       "@P_GENDER_USER, " +
                                                       "@P_PASSWORD_USER)";
    }
}
