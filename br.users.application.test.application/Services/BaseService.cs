using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace br.users.application.test.application.Services
{
    public abstract class BaseService
    {
        protected bool StringToBool(string value)
        {
            if (value.ToUpper().Equals("TRUE"))
            {
                return true;
            }

            return false;
        }

        protected bool ObjectToBool(object value)
        {
            return StringToBool(value.ToString());
        }

        public List<T> EnumerableToList<T>(IEnumerable<T> enumerable)
        {
            return enumerable.ToList();
        }

        public string ObjectToString(object value)
        {
            return value != null ? value.ToString() : string.Empty;
        }

        public DateTime FormatDateTimeEndDate(DateTime Date)
        {
            return new DateTime(Date.Year, Date.Month, Date.Day, 23, 59, 59);
        }

        public DateTime FormatDateTimeBeginDate(DateTime Date)
        {
            return new DateTime(Date.Year, Date.Month, Date.Day, 00, 00, 00);
        }
    }
}
