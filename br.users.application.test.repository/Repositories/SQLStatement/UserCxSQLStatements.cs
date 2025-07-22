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

        public static readonly string UpdateUserData = @"UPDATE users_cx SET "+
                                                       "name_user = @P_NAME_USER, " +
                                                       "email_user = @P_EMAIL_USER, " +
                                                       "age_user = @P_AGE_USER, " +
                                                       "gender_user = @P_GENDER_USER, " +
                                                       "password_user = @P_PASSWORD_USER " +
                                                       "WHERE id_user = @P_USER_ID";

        public static readonly string DeleteUserData = "DELETE FROM users_cx WHERE id_user = @P_USER_ID";
    }
}
