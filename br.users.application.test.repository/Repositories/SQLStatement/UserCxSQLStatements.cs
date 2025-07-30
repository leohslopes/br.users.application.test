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
                                                    "password_user AS UserPassword, " +
                                                    "picture_user AS UserPicture, "+
                                                    "official_number_user AS UserOfficialNumber, " +
                                                    "date_alter AS DateAlter " +
                                                    "FROM users_cx";

        public static readonly string InsertUserData = @"INSERT INTO users_cx(name_user, " +
                                                       "email_user, " +
                                                       "age_user, " +
                                                       "gender_user," +
                                                       "password_user," +
                                                       "picture_user," +
                                                       "official_number_user," +
                                                       "search_field, " +
                                                       "date_alter) " +
                                                       "VALUES (@P_NAME_USER, " +
                                                       "@P_EMAIL_USER, " +
                                                       "@P_AGE_USER, " +
                                                       "@P_GENDER_USER, " +
                                                       "@P_PASSWORD_USER," +
                                                       "NULL, " +
                                                       "@P_OFFICIAL_NUMBER_USER, " +
                                                       "@P_SEARCH_FIELD, " +
                                                       "NOW())";

        public static readonly string UpdateUserData = @"UPDATE users_cx SET "+
                                                       "name_user = @P_NAME_USER, " +
                                                       "email_user = @P_EMAIL_USER, " +
                                                       "age_user = @P_AGE_USER, " +
                                                       "gender_user = @P_GENDER_USER, " +
                                                       "password_user = @P_PASSWORD_USER, " +
                                                       "picture_user = @P_PICTURE_USER, " +
                                                       "official_number_user = @P_OFFICIAL_NUMBER_USER, " +
                                                       "search_field = @P_SEARCH_FIELD, " +
                                                       "date_alter = NOW() " +
                                                       "WHERE id_user = @P_USER_ID";

        public static readonly string DeleteUserData = "DELETE FROM users_cx WHERE id_user = @P_USER_ID";

        public static readonly string GetUsersWithFilters = "SELECT id_user AS UserID," +
                                                            "name_user AS UserName, " +
                                                            "email_user AS UserEmail, " +
                                                            "age_user AS UserAge, " +
                                                            "gender_user AS UserGender, " +
                                                            "password_user AS UserPassword, " +
                                                            "picture_user AS UserPicture, " +
                                                            "official_number_user AS UserOfficialNumber, " +
                                                            "date_alter AS DateAlter " +
                                                            "FROM users_cx " +
                                                            "WHERE (@P_SEARCH_FIELD IS NULL OR UPPER(search_field) LIKE @P_SEARCH_FIELD) " +
                                                            "AND (@P_EMAIL_USER IS NULL OR UPPER(email_user) = @P_EMAIL_USER) " +
                                                            "AND (@P_HAS_IMG IS NULL OR (@P_HAS_IMG = FALSE AND picture_user IS NOT NULL) OR (@P_HAS_IMG = TRUE AND picture_user IS NULL)) " +
                                                            "AND (@P_RECS_USER IS NULL OR (@P_RECS_USER = TRUE AND DATE(date_alter) = CURDATE())) " +
                                                            "AND (@P_GENDER_USER IS NULL OR UPPER(gender_user) = @P_GENDER_USER)";

        public static readonly string GetUserEmailExists = "SELECT COUNT(*) FROM users_cx WHERE UPPER(email_user) = @P_EMAIL_USER";

        public static readonly string GetUserOfficialNumberExists = "SELECT COUNT(*) FROM users_cx WHERE official_number_user = @P_OFFICIAL_NUMBER_USER";

        public static readonly string GetTotalUsersByMonths = "WITH meses AS (SELECT DATE_FORMAT(DATE_SUB(CURDATE(), INTERVAL n MONTH), '%Y-%m') AS mes_referencia, " +
                                                              "MONTH(DATE_SUB(CURDATE(), INTERVAL n MONTH)) AS num_mes, "+
                                                              "YEAR(DATE_SUB(CURDATE(), INTERVAL n MONTH)) AS ano " +
                                                              "FROM (SELECT 0 AS n UNION ALL SELECT 1 UNION ALL SELECT 2 UNION ALL SELECT 3 " +
                                                              "UNION ALL SELECT 4 UNION ALL SELECT 5 UNION ALL SELECT 6 UNION ALL SELECT 7 " +
                                                              "UNION ALL SELECT 8 UNION ALL SELECT 9 UNION ALL SELECT 10 UNION ALL SELECT 11) AS x) " +
                                                              "SELECT CONCAT(LPAD(m.num_mes, 2, '0'), '/', m.ano) AS Years, " +
                                                              "CASE m.num_mes " +
                                                              "WHEN 1 THEN 'Janeiro' " +
                                                              "WHEN 2 THEN 'Fevereiro' " +
                                                              "WHEN 3 THEN 'Março' " +
                                                              "WHEN 4 THEN 'Abril' " +
                                                              "WHEN 5 THEN 'Maio' " +
                                                              "WHEN 6 THEN 'Junho' " +
                                                              "WHEN 7 THEN 'Julho' " +
                                                              "WHEN 8 THEN 'Agosto' " +
                                                              "WHEN 9 THEN 'Setembro' " +
                                                              "WHEN 10 THEN 'Outubro' " +
                                                              "WHEN 11 THEN 'Novembro' " +
                                                              "WHEN 12 THEN 'Dezembro' " +
                                                              "END AS MonthName, " +
                                                              "COUNT(u.id_user) AS CountUsers " +
                                                              "FROM meses m " +
                                                              "LEFT JOIN users_cx u " +
                                                              "ON DATE_FORMAT(u.date_alter, '%Y-%m') = m.mes_referencia " +
                                                              "GROUP BY m.ano, m.num_mes " +
                                                              "ORDER BY m.ano, m.num_mes";
    }
}
