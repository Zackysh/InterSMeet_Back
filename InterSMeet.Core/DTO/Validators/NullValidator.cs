using System.Reflection;


namespace InterSMeet.Core.DTO.Validators
{
    public class NullValidator
    {
        public static bool IsNullOrEmpty(object myObject)
        {
            if (myObject == null) return true;
            foreach (PropertyInfo pi in myObject.GetType().GetProperties())
            {
                if (pi.PropertyType == typeof(string))
                {
                    // if string isn't empty
                    if (!string.IsNullOrEmpty(pi.GetValue(myObject) as string))
                        return false;
                }
                else if(pi.PropertyType == typeof(DateTime) || pi.PropertyType == typeof(DateTime?))
                {
                    try { if ((DateTime)pi.GetValue(myObject)! != DateTime.MinValue) return false; }
                    catch { }
                }
                else if (pi.GetValue(myObject) != null) return false;

            }
            return true;
        }
    }
}
